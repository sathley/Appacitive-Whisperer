using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Appacitive.Tools.DBImport.Model;

namespace Appacitive.Tools.DBImport
{
    class RegularSchemaRuleWithNoConfig : IRule
    {
        public void Apply(Database database, MappingConfig mappingConfig, int tableIndex, ref Model.AppacitiveInput input)
        {
            var table = database.Tables[tableIndex];
            var schema = new Schema
            {
                Name = table.Name,
                Description =
                    string.Format("Schema for {0}", table.Name),
                Properties = new List<Property>()
            };
            if(StringValidationHelper.IsAlphanumeric(schema.Name) == false)
                throw new Exception(string.Format("Incorrect name for schema '{0}'. It should be alphanumeric starting with alphabet.",schema.Name));
            foreach (var tableColumn in table.Columns)
            {
                var property = new Property {Name = tableColumn.Name};
                if (StringValidationHelper.IsAlphanumeric(property.Name) == false)
                    throw new Exception(string.Format("Incorrect name for property '{0}'. It should be alphanumeric starting with alphabet.", property.Name));
                property.Description = string.Format("Property for {0}", property.Name);


                property.DataType = DataTypeHelper.FigureDataType(tableColumn);
                
                foreach (var index in tableColumn.Indexes)
                {
                    if (index.Type.Equals("unique") || index.Type.Equals("primary"))
                        property.IsUnique = true;
                    //  Handle composite uniqie/primary key
                }
                foreach (var constraint in tableColumn.Constraints)
                {
                    ConstraintsHelper.Process(constraint, ref property);
                }
                schema.Properties.Add(property);
            }
            input.Schemata.Add(schema);
        }
    }
}
