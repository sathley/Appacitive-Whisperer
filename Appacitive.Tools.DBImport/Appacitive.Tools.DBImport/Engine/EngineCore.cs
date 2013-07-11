using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Appacitive.Tools.DBImport
{
    public static class EngineCore
    {
        public static AppacitiveInput AppacitizeDatabase(this Database database, MappingConfig mappingConfig)
        {
            var result = new AppacitiveInput();

            foreach (var table in database.Tables)
            {
                var tableConfig =
                    mappingConfig.TableMappings.FirstOrDefault(t => t.TableName.Equals(table.Name, StringComparison.InvariantCultureIgnoreCase));
                if (tableConfig != null)
                {
                    //  Steps to take in presence of mapping config for this table

                    //  Remove ignored columns
                    table.Columns.RemoveAll(col => tableConfig.IgnoreColumns.Contains(col.Name));

                    //  If table is to be converted to a cannedList
                    if (tableConfig.MakeCannedList)
                    {
                        //  Ensure table contains key and value columns
                        if (table.Columns.Find(col => col.Name.Equals(tableConfig.CannedListKeyColumn)) == null || table.Columns.Find(col => col.Name.Equals(tableConfig.CannedListValueColumn)) == null)
                        {
                            throw new Exception("Table must contain both key and value columns.");
                        }

                        //  Ensure cannedList key has unique or primary constraint
                        var keyCol = table.Columns.Find(col => col.Name.Equals(tableConfig.CannedListKeyColumn));
                        if (keyCol.Indexes.Exists(i => i.GetType() == typeof(UniqueIndex)) == false && keyCol.Indexes.Exists(i => i.GetType() == typeof(PrimaryIndex)) == false)
                        {
                            throw new Exception("CannedList key must have unique or primary key index/constraint");
                        }

                        //  Add the cannedList to result
                        //  Todo-Need to populate the cannedList too.
                        result.CannedLists.Add(new CannedList()
                                                   {
                                                       Name = tableConfig.KeepNameAsIs ? table.Name : tableConfig.AppacitiveSchemaName,
                                                       Description = tableConfig.Description ?? string.Format("CannedList for '{0}'", table.Name),
                                                       Items = new List<ListItem>()//  Populate later
                                                   });
                        continue;
                    }

                    //  Steps to take if this table is a mapping table in many-to-many relationship.
                    if (tableConfig.IsJunctionTable)
                    {
                        var columnA = table.Columns.First(col => col.Name.Equals(tableConfig.JunctionsSideAColumn));
                        var columnB = table.Columns.Where(col => col.Name.Equals(tableConfig.JunctionsSideBColumn)).First();
                        if (columnA == null || columnB == null)
                        {
                            throw new Exception("Incorrect mapping tables columns.");
                        }
                        //  Create corresponding relation for junction table
                        continue;
                    }

                    //  If regular table, convert to schema
                    var schema = new Schema
                                     {
                                         Name = tableConfig.KeepNameAsIs ? table.Name : tableConfig.AppacitiveSchemaName,
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
                        }
                        else
                        {
                            property.Name = propertyConfig.KeepNameAsIs
                                                ? tableColumn.Name
                                                : propertyConfig.AppacitivePropertyName;
                        }
                        //  Process indexes
                        foreach (var index in tableColumn.Indexes)
                        {
                            switch (index.Type)
                            {
                                case "unique":
                                    var uniqueIndex = index as UniqueIndex;

                                    break;
                                case "primary":
                                    var primaryIndex = index as PrimaryIndex;
                                    break;
                                case "foreign":
                                    var foreignKeyIndex = index as ForeignIndex;
                                    break;
                            }
                        }

                        //  Process business constraints
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
                    result.Schemata.Add(schema);

                }
                else
                {
                    //  Steps to take in absence of mapping config for this table
                }
            }
            return result;
        }
    }
}
