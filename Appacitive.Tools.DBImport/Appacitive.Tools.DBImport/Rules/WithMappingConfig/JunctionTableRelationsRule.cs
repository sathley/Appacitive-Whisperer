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

            if (!tableConfig.IsJunctionTable)   return;

            var juncColA = table.Columns.First(col => col.Name.Equals(tableConfig.JunctionsSideAColumn));
            var juncColB = table.Columns.First(col => col.Name.Equals(tableConfig.JunctionsSideBColumn));

            var juncColAfKeyIndex = juncColA.Indexes.First(i => i.Type.Equals("foriegn")) as ForeignIndex;
            var juncColBFKeyIndex = juncColB.Indexes.First(i => i.Type.Equals("foriegn")) as ForeignIndex;

            var relation = new Relation();
            relation.Name = string.IsNullOrEmpty(tableConfig.JunctionTableRelationName)
                                ? string.Format("{0}_{1}", juncColAfKeyIndex.ReferenceTableName,
                                                juncColBFKeyIndex.ReferenceTableName)
                                : tableConfig.JunctionTableRelationName;

            relation.Description = string.IsNullOrEmpty(tableConfig.JunctionTableRelationDescription)
                                       ? string.Format("Many to many Relation for junction table '{0}'", table.Name)
                                       : tableConfig.JunctionTableRelationDescription;

            relation.EndPointA=new EndPoint();
            relation.EndPointB=new EndPoint();
            relation.EndPointA.Label = string.IsNullOrEmpty(tableConfig.JunctionALabel)
                                           ? juncColAfKeyIndex.ReferenceColumnName
                                           : tableConfig.JunctionALabel;

            relation.EndPointB.Label = string.IsNullOrEmpty(tableConfig.JunctionBLabel)
                                           ? juncColBFKeyIndex.ReferenceColumnName
                                           : tableConfig.JunctionBLabel;

            relation.EndPointA.Multiplicity = tableConfig.JunctionaSideAMultiplicity == 0
                                                  ? -1
                                                  : tableConfig.JunctionaSideAMultiplicity;

            relation.EndPointB.Multiplicity = tableConfig.JunctionaSideBMultiplicity == 0
                                                  ? -1
                                                  : tableConfig.JunctionaSideBMultiplicity;


            var tableA = database.Tables.First(t => t.Name.Equals(juncColAfKeyIndex.ReferenceTableName));
            var tableAConf = mappingConfig.TableMappings.Find(conf => conf.TableName.Equals(tableA.Name));
            if (tableAConf == null)
                relation.EndPointA.SchemaName = tableA.Name;
            else
            {
                relation.EndPointA.SchemaName = tableAConf.KeepNameAsIs ? tableA.Name : tableAConf.AppacitiveName;
            }

            var tableB = database.Tables.First(t => t.Name.Equals(juncColBFKeyIndex.ReferenceTableName));
            var tableBConf = mappingConfig.TableMappings.Find(conf => conf.TableName.Equals(tableB.Name));
            if (tableBConf == null)
                relation.EndPointB.SchemaName = tableB.Name;
            else
            {
                relation.EndPointB.SchemaName = tableBConf.KeepNameAsIs ? tableB.Name : tableBConf.AppacitiveName;
            }
            relation.Properties=new List<Property>();
            relation.Properties.AddRange(tableConfig.AddPropertiesToSchema);

            //  Process remaining columns in junction table
            //  TODO: Move property mapping logic to separate rule
            foreach (var column in table.Columns)
            {
                if(column.Name.Equals(tableConfig.JunctionsSideAColumn) || column.Name.Equals(tableConfig.JunctionsSideBColumn))
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
                //  Figure out data type
                if (column.Type == DbDataType.NVarChar || column.Type == DbDataType.NChar
                    || column.Type == DbDataType.VarChar || column.Type == DbDataType.Char)
                    property.DataType = "string";
                if (column.Type == DbDataType.Bit)
                    property.DataType = "bool";
                if (column.Type == DbDataType.SmallInt || column.Type == DbDataType.BigInt || column.Type == DbDataType.Int || column.Type == DbDataType.TinyInt)
                    property.DataType = "integer";
                if (column.Type == DbDataType.Timestamp || column.Type == DbDataType.SmallDateTime || column.Type == DbDataType.DateTime || column.Type == DbDataType.DateTime2)
                    property.DataType = "datetime";
                if (column.Type == DbDataType.Date)
                    property.DataType = "date";
                if (column.Type == DbDataType.Time)
                    property.DataType = "time";
                if (column.Type == DbDataType.Geography)
                    property.DataType = "geography";
                if (column.Type == DbDataType.Text || column.Type == DbDataType.NText)
                    property.DataType = "text";
                if (column.Type == DbDataType.Float || column.Type == DbDataType.Decimal)
                    property.DataType = "decimal";
                if (column.Type == DbDataType.VarBinary || column.Type == DbDataType.Binary || column.Type == DbDataType.Image)
                    property.DataType = "blob";

                if (string.IsNullOrEmpty(property.DataType))
                    property.DataType = "string";
                foreach (var constraint in column.Constraints)
                {
                    switch (constraint.Type)
                    {
                        case "check":
                            var checkConstraint = constraint as CheckConstraint;
                            if (checkConstraint != null && checkConstraint.MinValue != null)
                                property.Range.MinValue = checkConstraint.MinValue;
                            if (checkConstraint != null && checkConstraint.MaxValue != null)
                                property.Range.MaxValue = checkConstraint.MaxValue;
                            if (checkConstraint != null)
                            {
                                property.MinLength = checkConstraint.MinLength;
                                property.MaxLength = checkConstraint.MaxLength;
                                if (string.IsNullOrEmpty(checkConstraint.Regex) == false)
                                    property.RegexValidator = checkConstraint.Regex;
                            }
                            break;
                        case "default":
                            var defaultConstraint = constraint as DefaultConstraint;
                            if (defaultConstraint != null && string.IsNullOrEmpty(defaultConstraint.DefaultValue) == false)
                            {
                                property.HasDefaultValue = true;
                                property.DefaultValue = defaultConstraint.DefaultValue;
                            }
                            break;
                        case "notnull":
                            var notNullConstraint = constraint as NotNullConstraint;
                            property.IsMandatory = true;
                            break;
                    }
                }
                relation.Properties.Add(property);
            }
            input.Relations.Add(relation);
        }
    }
}
