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
            foreach (var tableColumn in table.Columns)
            {
                var property = new Property {Name = tableColumn.Name};

                property.Description = string.Format("Property for {0}", property.Name);

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
                schema.Properties.Add(property);
            }
            input.Schemata.Add(schema);
        }
    }
}
