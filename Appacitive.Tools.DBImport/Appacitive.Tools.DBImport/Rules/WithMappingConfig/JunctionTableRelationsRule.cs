using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Appacitive.Tools.DBImport.Model;

namespace Appacitive.Tools.DBImport
{
    public class JunctionTableRelationsRule : IRule
    {
        public void Apply(Database database, List<TableMapping> tableMappings, int tableIndex, ref AppacitiveInput input)
        {
            var currentTable = database.Tables[tableIndex];

            TableMapping tableConfigForCurrentTable = null;
            if (tableMappings != null && tableMappings.Count != 0)
                tableConfigForCurrentTable = tableMappings.FirstOrDefault(t => t.TableName.Equals(database.Tables[tableIndex].Name, StringComparison.InvariantCultureIgnoreCase));

            //  Return in absense of table mapping for this table.
            if (tableConfigForCurrentTable == null)
                return;

            //  Return if this table is not marked as junction table.
            if (tableConfigForCurrentTable.IsJunctionTable == false)
                return;

            var juncColA = currentTable.Columns.First(col => col.Name.Equals(tableConfigForCurrentTable.JunctionsSideAColumn, StringComparison.InvariantCultureIgnoreCase));
            var juncColB = currentTable.Columns.First(col => col.Name.Equals(tableConfigForCurrentTable.JunctionsSideBColumn, StringComparison.InvariantCultureIgnoreCase));

            var juncColAfKeyIndex = juncColA.Indexes.First(i => i.Type.Equals("foreign")) as ForeignIndex;
            var juncColBfKeyIndex = juncColB.Indexes.First(i => i.Type.Equals("foreign")) as ForeignIndex;

            var relation = new Relation();

            relation.Name = tableConfigForCurrentTable.JunctionTableRelationName ?? string.Format("{0}_{1}", juncColAfKeyIndex.ReferenceTableName, juncColBfKeyIndex.ReferenceTableName);

            //  Validate relation name.
            if (relation.Name.IsValidName() == false)
                throw new Exception(string.Format("Incorrect name for relation '{0}'. It should be alphanumeric starting with alphabet.", relation.Name));

            relation.Description = tableConfigForCurrentTable.JunctionTableRelationDescription ?? string.Empty;

            relation.EndPointA = new EndPoint();
            relation.EndPointB = new EndPoint();

            relation.EndPointA.Label = tableConfigForCurrentTable.JunctionALabel ?? juncColAfKeyIndex.ReferenceColumnName;

            if (relation.EndPointA.Label.IsValidName() == false)
                throw new Exception(string.Format("Incorrect name for label '{0}' in relation '{1}'. It should be alphanumeric starting with alphabet.", relation.EndPointA.Label, relation.Name));

            relation.EndPointB.Label = tableConfigForCurrentTable.JunctionBLabel ?? juncColBfKeyIndex.ReferenceColumnName;

            if (relation.EndPointB.Label.IsValidName() == false)
                throw new Exception(string.Format("Incorrect name for label '{0}' in relation '{1}'. It should be alphanumeric starting with alphabet.", relation.EndPointB.Label, relation.Name));

            relation.EndPointA.Multiplicity = tableConfigForCurrentTable.JunctionaSideAMultiplicity == 0
                                                  ? -1
                                                  : tableConfigForCurrentTable.JunctionaSideAMultiplicity;

            relation.EndPointB.Multiplicity = tableConfigForCurrentTable.JunctionaSideBMultiplicity == 0
                                                  ? -1
                                                  : tableConfigForCurrentTable.JunctionaSideBMultiplicity;


            var tableA = database.Tables.First(t => t.Name.Equals(juncColAfKeyIndex.ReferenceTableName, StringComparison.InvariantCultureIgnoreCase));
            if (tableA == null)
                throw new Exception(string.Format("No table found by name '{0}' for relation '{1}'.", juncColAfKeyIndex.ReferenceTableName, relation.Name));
            var tableAConf = tableMappings.Find(conf => conf.TableName.Equals(tableA.Name, StringComparison.InvariantCultureIgnoreCase));

            if (tableAConf == null)
                relation.EndPointA.SchemaName = tableA.Name;
            else
                relation.EndPointA.SchemaName = tableAConf.KeepNameAsIs ? tableA.Name : tableAConf.AppacitiveName;


            var tableB = database.Tables.First(t => t.Name.Equals(juncColBfKeyIndex.ReferenceTableName));
            if (tableB == null)
                throw new Exception(string.Format("No table found by name '{0}' for relation '{1}'.", juncColAfKeyIndex.ReferenceTableName, relation.Name));
            var tableBConf = tableMappings.Find(conf => conf.TableName.Equals(tableB.Name));

            if (tableBConf == null)
                relation.EndPointB.SchemaName = tableB.Name;
            else
                relation.EndPointB.SchemaName = tableBConf.KeepNameAsIs ? tableB.Name : tableBConf.AppacitiveName;

            relation.Properties = new List<Property>();

            //  Add additional properties supplied and validate thier names
            foreach (var property in tableConfigForCurrentTable.AddPropertiesToSchema)
            {
                if (property.Name.IsValidName() == false)
                    throw new Exception(string.Format("Incorrect name for property '{0}' in relation '{1}'. It should be alphanumeric starting with alphabet.", property.Name, relation.Name));
            }
            relation.Properties.AddRange(tableConfigForCurrentTable.AddPropertiesToSchema);

            //  Process remaining columns in junction table
            //  TODO: Move property mapping logic to separate rule
            foreach (var column in currentTable.Columns)
            {
                //  Ignore junction columns
                if (column.Name.Equals(tableConfigForCurrentTable.JunctionsSideAColumn) || column.Name.Equals(tableConfigForCurrentTable.JunctionsSideBColumn))
                    return;

                var property = new Property();
                var propertyConfig = tableConfigForCurrentTable.PropertyMappings.FirstOrDefault(pc => pc.ColumnName.Equals(column.Name,StringComparison.InvariantCultureIgnoreCase));
                if (propertyConfig == null)
                {
                    property.Name = column.Name;
                    property.Description = string.Format("Property for {0}", property.Name);
                }
                else
                {
                    property.Name = propertyConfig.KeepNameAsIs? column.Name: propertyConfig.AppacitivePropertyName;
                    property.Description =  propertyConfig.Description ?? string.Empty;
                }

                //  Validate relation property name.
                if (property.Name.IsValidName() == false)
                    throw new Exception(string.Format("Incorrect name for property '{0}' in relation '{1}'. It should be alphanumeric starting with alphabet.", property.Name, relation.Name));

                //  Figure out Appacitive datatype
                property.DataType = DataTypeHelper.FigureDataType(column);

                //  Add Appacitive business constraints
                foreach (var constraint in column.Constraints)
                {
                    ConstraintsHelper.Process(constraint, ref property);
                }
                relation.Properties.Add(property);
            }
            input.Relations.Add(relation);
        }
    }
}
