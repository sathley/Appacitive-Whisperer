using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Appacitive.Tools.DBImport.Model;

namespace Appacitive.Tools.DBImport
{
    public class RegularSchemaRule : IRule
    {
        public void Apply(Database database, MappingConfig mappingConfig, int tableIndex, ref AppacitiveInput input)
        {
            var table = database.Tables[tableIndex];
            var tableConfig =
                    mappingConfig.TableMappings.FirstOrDefault(t => t.TableName.Equals(database.Tables[tableIndex].Name, StringComparison.InvariantCultureIgnoreCase));

            if (tableConfig.IsJunctionTable || tableConfig.MakeCannedList)
                return;
            var schema = new Schema
                             {
                                 Name = tableConfig.KeepNameAsIs ? table.Name : tableConfig.AppacitiveName,
                                 Description =
                                     tableConfig.Description ?? string.Format("Schema for {0}", table.Name),
                                 Properties = new List<Property>()
                             };
            schema.Properties.AddRange(tableConfig.AddPropertiesToSchema);
            //  Process columns
            foreach (var tableColumn in table.Columns)
            {
                var property = new Property();
                var propertyConfig = tableConfig.PropertyMappings.First(pc => pc.ColumnName.Equals(tableColumn.Name));
                if (propertyConfig == null)
                {
                    property.Name = tableColumn.Name;
                    property.Description = string.Format("Property for {0}", property.Name);
                }
                else
                {
                    property.Name = propertyConfig.KeepNameAsIs
                                        ? tableColumn.Name
                                        : propertyConfig.AppacitivePropertyName;
                    property.Description = string.IsNullOrEmpty(property.Description)
                                               ? string.Format("Property for {0}", property.Name)
                                               : propertyConfig.Description;
                } 
                
                foreach (var constraint in tableColumn.Constraints)
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
                //  Figure out data type
                if (tableColumn.Type == DbDataType.NVarChar || tableColumn.Type == DbDataType.NChar
                    || tableColumn.Type == DbDataType.VarChar || tableColumn.Type == DbDataType.Char)
                    property.DataType = "string";
                if (tableColumn.Type == DbDataType.Bit)
                    property.DataType = "bool";
                if (tableColumn.Type == DbDataType.SmallInt || tableColumn.Type == DbDataType.BigInt || tableColumn.Type == DbDataType.Int || tableColumn.Type == DbDataType.TinyInt)
                    property.DataType = "long";
                if (tableColumn.Type == DbDataType.Timestamp || tableColumn.Type == DbDataType.SmallDateTime || tableColumn.Type == DbDataType.DateTime || tableColumn.Type == DbDataType.DateTime2)
                    property.DataType = "datetime";
                if (tableColumn.Type == DbDataType.Date)
                    property.DataType = "date";
                if (tableColumn.Type == DbDataType.Time)
                    property.DataType = "time";
                if (tableColumn.Type == DbDataType.Geography)
                    property.DataType = "geography";
                if (tableColumn.Type == DbDataType.Text || tableColumn.Type == DbDataType.NText)
                    property.DataType = "text";
                if (tableColumn.Type == DbDataType.Float || tableColumn.Type == DbDataType.Decimal)
                    property.DataType = "decimal";
                if (tableColumn.Type == DbDataType.VarBinary || tableColumn.Type == DbDataType.Binary || tableColumn.Type == DbDataType.Image)
                    property.DataType = "blob";

                if (string.IsNullOrEmpty(property.DataType))
                    property.DataType = "string";
                schema.Properties.Add(property);
            }
            input.Schemata.Add(schema);
        }
    }
}
