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
            if (StringValidationHelper.IsAlphanumeric(schema.Name) == false)
                throw new Exception(string.Format("Incorrect name for schema '{0}'. It should be alphanumeric starting with alphabet.", schema.Name));
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
                    ConstraintsHelper.Process(constraint,ref property);
                }

                property.DataType = DataTypeHelper.FigureDataType(tableColumn);
                
                schema.Properties.Add(property);
            }
            input.Schemata.Add(schema);
        }
    }
}
