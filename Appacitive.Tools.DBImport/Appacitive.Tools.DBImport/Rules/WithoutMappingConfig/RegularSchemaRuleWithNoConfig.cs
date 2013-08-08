using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Appacitive.Tools.DBImport.Model;

namespace Appacitive.Tools.DBImport
{
    public class RegularSchemaRuleWithNoConfig : IRule
    {
        public void Apply(Database database, List<TableMapping> mappingConfig, int tableIndex, ref AppacitiveInput input)
        {
            var indexMapper = new Dictionary<string, List<string>>();

            var table = database.Tables[tableIndex];
            var schema = new Schema
            {
                Name = table.Name,
                Description = string.Format("Schema for {0}", table.Name),
                Properties = new List<Property>()
            };
            if (schema.Name.IsValidName() == false)
                throw new Exception(string.Format("Incorrect name for schema '{0}'. It should be alphanumeric, starting with alphabet.", schema.Name));

            foreach (var tableColumn in table.Columns)
            {
                var property = new Property { Name = tableColumn.Name };
                if (property.Name.IsValidName() == false)
                    throw new Exception(string.Format("Incorrect name for property '{0}'. It should be alphanumeric, starting with alphabet.", property.Name));
                property.Description = string.Format("Property for {0}", property.Name);

                property.DataType = DataTypeHelper.FigureDataType(tableColumn);

                //  Handle composite uniqie/primary key
                foreach (var index in tableColumn.Indexes)
                {
                    if (index.Type.Equals("unique") || index.Type.Equals("primary"))
                    {
                        if (indexMapper.ContainsKey(index.Name) == false)
                            indexMapper[index.Name] = new List<string>();

                        indexMapper[index.Name].Add(tableColumn.Name);
                    }
                }

                //  Handle constraints
                foreach (var constraint in tableColumn.Constraints)
                {
                    ConstraintsHelper.Process(constraint, ref property);
                }
                schema.Properties.Add(property);
            }

            //  Process unique and primary indexes
            foreach (var indexMap in indexMapper)
            {
                if (indexMap.Value.Count == 1)
                {
                    foreach (var col in table.Columns)
                    {
                        if (col.Indexes.Any(ind => ind.Name.Equals(indexMap.Key)))
                        {
                            schema.Properties.First(prop => prop.Name.Equals(col.Name)).IsUnique = true;
                        }
                    }
                }
                else
                {
                    var uniqueCompositeProperty = new Property();
                    var propertyNameBuilder = new StringBuilder();
                    propertyNameBuilder.Append(indexMap.Value.First());
                    foreach (var colName in indexMap.Value)
                    {
                        if (colName.Equals(indexMap.Value.First()))
                            continue;
                        propertyNameBuilder.Append("__");
                        propertyNameBuilder.Append(colName);
                    }
                    uniqueCompositeProperty.IsUnique = true;
                    schema.Properties.Add(uniqueCompositeProperty);
                }
            }
            input.Schemata.Add(schema);
        }
    }
}
