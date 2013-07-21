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
            TableMapping tableConfig=null;
            if (mappingConfig != null && mappingConfig.TableMappings != null)
                tableConfig =
                    mappingConfig.TableMappings.FirstOrDefault(t => t.TableName.Equals(database.Tables[tableIndex].Name, StringComparison.InvariantCultureIgnoreCase));
            

            if(tableConfig==null)
            {
                new RegularSchemaRuleWithNoConfig().Apply(database,mappingConfig,tableIndex,ref input);
                return;
            }

            if (tableConfig.IsJunctionTable || tableConfig.MakeCannedList)
                return;
            var schema = new Schema
                             {
                                 Name = tableConfig.KeepNameAsIs ? table.Name : tableConfig.AppacitiveName,
                                 Description =
                                     tableConfig.Description ?? string.Format("Schema for {0}.", table.Name),
                                 Properties = new List<Property>()
                             };

            //  Validate schema name.
            if (StringValidationHelper.IsAlphanumeric(schema.Name) == false)
                throw new Exception(string.Format("Incorrect name for schema '{0}'. It should be alphanumeric starting with alphabet.", schema.Name));

            //  Add additional properties specified in the config.
            schema.Properties.AddRange(tableConfig.AddPropertiesToSchema);

            //  Process columns.
            foreach (var tableColumn in table.Columns)
            {
                var property = new Property();
                var propertyConfig = tableConfig.PropertyMappings.First(pc => pc.ColumnName.Equals(tableColumn.Name));
                if (propertyConfig == null)
                {
                    property.Name = tableColumn.Name;
                    property.Description = string.Format("Property for {0}.", property.Name);
                }
                else
                {
                    property.Name = propertyConfig.KeepNameAsIs
                                        ? tableColumn.Name
                                        : propertyConfig.AppacitivePropertyName;
                    property.Description = propertyConfig.Description ??
                                           string.Format("Property for {0}", property.Name);
                } 
                
                //  Validate property name.
                if (StringValidationHelper.IsAlphanumeric(property.Name) == false)
                    throw new Exception(string.Format("Incorrect name for property '{0}' in schema '{1}'. It should be alphanumeric starting with alphabet.", property.Name, schema.Name));

                //  Add Appacitive business validations according to constraints on columns.
                foreach (var constraint in tableColumn.Constraints)
                {
                    ConstraintsHelper.Process(constraint,ref property);
                }

                //  Figure out Appacitive datatype from database column datatype
                property.DataType = DataTypeHelper.FigureDataType(tableColumn);
                
                schema.Properties.Add(property);
            }
            input.Schemata.Add(schema);
        }
    }
}
