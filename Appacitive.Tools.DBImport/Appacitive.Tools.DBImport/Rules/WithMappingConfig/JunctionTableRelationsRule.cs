using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Appacitive.Tools.DBImport.Model;

namespace Appacitive.Tools.DBImport
{
    public class JunctionTableRelationsRule : IRule
    {
        public void Apply(Database database, MappingConfig mappingConfig, int tableIndex, ref AppacitiveInput input)
        {
            var table = database.Tables[tableIndex];
            var tableConfig =
                    mappingConfig.TableMappings.FirstOrDefault(t => t.TableName.Equals(database.Tables[tableIndex].Name, StringComparison.InvariantCultureIgnoreCase));

            if (tableConfig != null && !tableConfig.IsJunctionTable) return;

            var juncColA = table.Columns.First(col => col.Name.Equals(tableConfig.JunctionsSideAColumn));
            var juncColB = table.Columns.First(col => col.Name.Equals(tableConfig.JunctionsSideBColumn));

            var juncColAfKeyIndex = juncColA.Indexes.First(i => i.Type.Equals("foriegn")) as ForeignIndex;
            var juncColBFKeyIndex = juncColB.Indexes.First(i => i.Type.Equals("foriegn")) as ForeignIndex;

            var relation = new Relation();

            relation.Name = tableConfig.JunctionTableRelationName ??
                            string.Format("{0}_{1}", juncColAfKeyIndex.ReferenceTableName,
                                          juncColBFKeyIndex.ReferenceTableName);

            //  Validate relation name.
            if (StringValidationHelper.IsAlphanumeric(relation.Name) == false)
                throw new Exception(string.Format("Incorrect name for relation '{0}'. It should be alphanumeric starting with alphabet.", relation.Name));

            relation.Description = tableConfig.JunctionTableRelationDescription ?? string.Format("Many-to-many Relation for junction table '{0}'", table.Name);

            relation.EndPointA = new EndPoint();
            relation.EndPointB = new EndPoint();
            relation.EndPointA.Label = tableConfig.JunctionALabel ?? juncColAfKeyIndex.ReferenceColumnName;

            relation.EndPointB.Label = tableConfig.JunctionBLabel ?? juncColBFKeyIndex.ReferenceColumnName;

            relation.EndPointA.Multiplicity = tableConfig.JunctionaSideAMultiplicity == 0
                                                  ? -1
                                                  : tableConfig.JunctionaSideAMultiplicity;

            relation.EndPointB.Multiplicity = tableConfig.JunctionaSideBMultiplicity == 0
                                                  ? -1
                                                  : tableConfig.JunctionaSideBMultiplicity;


            var tableA = database.Tables.First(t => t.Name.Equals(juncColAfKeyIndex.ReferenceTableName));
            if(tableA==null)
                throw new Exception(string.Format("No table found by name '{0}' for relation '{1}'.", juncColAfKeyIndex.ReferenceTableName,relation.Name));
            var tableAConf = mappingConfig.TableMappings.Find(conf => conf.TableName.Equals(tableA.Name));
            
            if (tableAConf == null)
                relation.EndPointA.SchemaName = tableA.Name;
            else
                relation.EndPointA.SchemaName = tableAConf.KeepNameAsIs ? tableA.Name : tableAConf.AppacitiveName;


            var tableB = database.Tables.First(t => t.Name.Equals(juncColBFKeyIndex.ReferenceTableName));
            if (tableB == null)
                throw new Exception(string.Format("No table found by name '{0}' for relation '{1}'.", juncColAfKeyIndex.ReferenceTableName, relation.Name));
            var tableBConf = mappingConfig.TableMappings.Find(conf => conf.TableName.Equals(tableB.Name));
            
            if (tableBConf == null)
                relation.EndPointB.SchemaName = tableB.Name;
            else
                relation.EndPointB.SchemaName = tableBConf.KeepNameAsIs ? tableB.Name : tableBConf.AppacitiveName;
            
            relation.Properties = new List<Property>();
            relation.Properties.AddRange(tableConfig.AddPropertiesToSchema);

            //  Process remaining columns in junction table
            //  TODO: Move property mapping logic to separate rule
            foreach (var column in table.Columns)
            {
                if (column.Name.Equals(tableConfig.JunctionsSideAColumn) || column.Name.Equals(tableConfig.JunctionsSideBColumn))
                    return;
                var property = new Property();
                var propertyConfig = tableConfig.PropertyMappings.First(pc => pc.ColumnName.Equals(column.Name));
                if (propertyConfig == null)
                {
                    property.Name = column.Name;
                    property.Description = string.Format("Property for {0}", property.Name);
                }
                else
                {
                    property.Name = propertyConfig.KeepNameAsIs
                                        ? column.Name
                                        : propertyConfig.AppacitivePropertyName;
                    property.Description = string.IsNullOrEmpty(property.Description)
                                               ? string.Format("Property for {0}", property.Name)
                                               : propertyConfig.Description;
                }

                //  Validate relation property name.
                if (StringValidationHelper.IsAlphanumeric(property.Name) == false)
                    throw new Exception(string.Format("Incorrect name for property '{0}' in relation '{1}'. It should be alphanumeric starting with alphabet.",property.Name, relation.Name));

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
