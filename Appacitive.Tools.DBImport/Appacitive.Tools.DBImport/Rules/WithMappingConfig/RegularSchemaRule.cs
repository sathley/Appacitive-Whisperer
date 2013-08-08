using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Appacitive.Tools.DBImport.Model;

namespace Appacitive.Tools.DBImport
{
    public class RegularSchemaRule : IRule
    {
        public void Apply(Database database, List<TableMapping> tableMappings, int tableIndex, ref AppacitiveInput input)
        {
            var currentTable = database.Tables[tableIndex];

            TableMapping tableConfigForCurrentTable = null;
            if (tableMappings != null && tableMappings.Count != 0)
                tableConfigForCurrentTable = tableMappings.FirstOrDefault(t => t.TableName.Equals(database.Tables[tableIndex].Name, StringComparison.InvariantCultureIgnoreCase));

            if (tableConfigForCurrentTable == null)
            {
                new RegularSchemaRuleWithNoConfig().Apply(database, tableMappings, tableIndex, ref input);
                return;
            }

            var indexMapping = new Dictionary<string, Dictionary<string, string>>();    //(indexName => (columnName=> coorespondingPropertyName))

            //  Don't convert junction tables or tables marked as cannedlists into schemas
            if (tableConfigForCurrentTable.IsJunctionTable || tableConfigForCurrentTable.MakeCannedList)
                return;

            //  Create schema
            var schema = new Schema
                             {
                                 Name = tableConfigForCurrentTable.KeepNameAsIs ? currentTable.Name : tableConfigForCurrentTable.AppacitiveName,
                                 Description = tableConfigForCurrentTable.Description ?? string.Format("Schema for {0}.", currentTable.Name),
                                 Properties = new List<Property>()
                             };

            //  Validate schema name
            if (schema.Name.IsValidName() == false)
                throw new Exception(string.Format("Incorrect name for schema '{0}'. It should be alphanumeric, starting with alphabet.", schema.Name));

            //  Validate names and add additional properties specified in the config
            foreach (var property in tableConfigForCurrentTable.AddPropertiesToSchema.Where(property => property.Name.IsValidName() == false))
            {
                throw new Exception(string.Format("Incorrect name for property '{0}' in schema '{1}'. It should be alphanumeric, starting with alphabet.", property.Name, schema.Name));
            }
            schema.Properties.AddRange(tableConfigForCurrentTable.AddPropertiesToSchema);

            //  Process columns.
            foreach (var tableColumn in currentTable.Columns)
            {
                var property = new Property();
                var propertyConfig = tableConfigForCurrentTable.PropertyMappings.First(pc => pc.ColumnName.Equals(tableColumn.Name, StringComparison.InvariantCultureIgnoreCase));
                if (propertyConfig == null)
                {
                    property.Name = tableColumn.Name;
                    property.Description = string.Format("Property for {0}.", property.Name);

                    //  Compute index mappings
                    foreach (var index in tableColumn.Indexes)
                    {
                        if (index.Type.Equals("unique") || index.Type.Equals("primary"))
                        {
                            if(indexMapping.ContainsKey(index.Name) ==false)
                                indexMapping[index.Name] = new Dictionary<string, string>();

                            indexMapping[index.Name].Add(tableColumn.Name, tableColumn.Name);
                        }
                        
                    }
                }
                else
                {
                    property.Name = propertyConfig.KeepNameAsIs ? tableColumn.Name : propertyConfig.AppacitivePropertyName;
                    property.Description = propertyConfig.Description ?? string.Empty;

                    //  Compute index mappings
                    foreach (var index in tableColumn.Indexes)
                    {
                        if (index.Type.Equals("unique") || index.Type.Equals("primary"))
                        {
                            if (indexMapping.ContainsKey(index.Name) == false)
                                indexMapping[index.Name] = new Dictionary<string, string>();

                            indexMapping[index.Name].Add(tableColumn.Name, property.Name);
                        }
                    }
                }

                //  Validate property name.
                if (property.Name.IsValidName() == false)
                    throw new Exception(string.Format("Incorrect name for property '{0}' in schema '{1}'. It should be alphanumeric, starting with alphabet.", property.Name, schema.Name));

                //  Add Appacitive business validations according to constraints on columns.
                foreach (var constraint in tableColumn.Constraints)
                {
                    ConstraintsHelper.Process(constraint, ref property);
                }

                //  Figure out Appacitive datatype from database column datatype
                property.DataType = DataTypeHelper.FigureDataType(tableColumn);

                schema.Properties.Add(property);
            }

            //  Process unique and primary indexes
            foreach (var indexMap in indexMapping)
            {
                if (indexMap.Value.Count == 1)
                {
                    foreach (var col in currentTable.Columns)
                    {
                        if (col.Indexes.Any(ind => ind.Name.Equals(indexMap.Key)))
                        {
                            schema.Properties.First(prop => prop.Name.Equals(indexMap.Value.First().Value)).IsUnique = true;
                        }
                    }
                }
                else
                {
                    var uniqueCompositeProperty = new Property();
                    var propertyNameBuilder = new StringBuilder();
                    propertyNameBuilder.Append(indexMap.Value.First().Value);
                    foreach (var propName in indexMap.Value.Keys)
                    {
                        if (propName.Equals(indexMap.Value.First().Value))
                            continue;
                        propertyNameBuilder.Append("__");
                        propertyNameBuilder.Append(propName);
                    }
                    uniqueCompositeProperty.Name = propertyNameBuilder.ToString();
                    uniqueCompositeProperty.IsUnique = true;
                    uniqueCompositeProperty.DataType = "string";
                    uniqueCompositeProperty.IsMandatory = true;
                    uniqueCompositeProperty.Description = string.Format("Additional column for handling composite unique/primary key '{0}'",indexMap.Key);
                    schema.Properties.Add(uniqueCompositeProperty);
                }
            }

            input.Schemata.Add(schema);
        }
    }
}
