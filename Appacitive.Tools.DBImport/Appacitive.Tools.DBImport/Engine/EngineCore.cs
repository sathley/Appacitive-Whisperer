using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Appacitive.Tools.DBImport.Model;

namespace Appacitive.Tools.DBImport
{
    public class EngineCore
    {
        public AppacitiveInput AppacitizeDatabase(Database database, MappingConfig mappingConfig)
        {
            var result = new AppacitiveInput();

            //  Whole method to be replaced with a simple rules processing engine

            foreach (var table in database.Tables)
            {
                var tableConfig =
                    mappingConfig.TableMappings.FirstOrDefault(t => t.TableName.Equals(table.Name, StringComparison.InvariantCultureIgnoreCase));
                if (tableConfig != null)
                {
                    ProcessTable(table, tableConfig, ref result);
                }
                else
                {
                    ProcessTable(table, ref result);
                }
            }

            //  Process indexes
            foreach (var table in database.Tables)
            {
                var tableConfig =mappingConfig.TableMappings.FirstOrDefault(t => t.TableName.Equals(table.Name, StringComparison.InvariantCultureIgnoreCase));
                if (tableConfig != null)
                {
                    if(tableConfig.IsJunctionTable)
                    {
                        var colA = table.Columns.First(col => col.Name.Equals(tableConfig.JunctionsSideAColumn));
                        var colB = table.Columns.First(col => col.Name.Equals(tableConfig.JunctionsSideBColumn));

                        //  Actually ask user to supply many-to many relationships foreign keys
                        var fKeyA = colA.Indexes.First(i => i.Type.Equals("foreign")) as ForeignIndex;
                        var fKeyB = colB.Indexes.First(i => i.Type.Equals("foreign")) as ForeignIndex;

                        var schemaA = result.Schemata.First(s => s.Name.Equals(fKeyA.ReferenceTableName));
                        var schemaB = result.Schemata.First(s => s.Name.Equals(fKeyB.ReferenceTableName));

                        var properties = new List<Property>();
                        
                        var relation = new Relation();
                        relation.Name = tableConfig.KeepNameAsIs ? table.Name : tableConfig.AppacitiveName;
                        relation.Description =
                            tableConfig.Description ?? string.Format("Relation for {0}", table.Name);
                        relation.EndPointA=new EndPoint()
                                               {
                                                   Label = string.IsNullOrEmpty(tableConfig.JunctionALabel)?schemaA.Name:tableConfig.JunctionALabel,
                                                   Multiplicity = tableConfig.JunctionaSideAMultiplicity == 0 ? -1 : tableConfig.JunctionaSideAMultiplicity,
                                                   SchemaName = schemaA.Name
                                               };
                        relation.EndPointB = new EndPoint()
                        {
                            Label = string.IsNullOrEmpty(tableConfig.JunctionBLabel) ? schemaB.Name : tableConfig.JunctionBLabel,
                            Multiplicity = tableConfig.JunctionaSideBMultiplicity == 0 ? -1 : tableConfig.JunctionaSideBMultiplicity,
                            SchemaName = schemaB.Name
                        };

                        //properties.AddRange(tableConfig.AddPropertiesToSchema);
                        foreach (var tableColumn in table.Columns)
                        {
                            var property = new Property();
                            if (tableColumn.Name.Equals(colA.Name) || tableColumn.Name.Equals(colB.Name))
                                continue;
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
                            relation.Properties.Add(property);
                        }
                        result.Relations.Add(relation);
                        continue;
                    }
                    foreach (var column in table.Columns)
                    {
                        foreach (var index in column.Indexes)
                        {
                            if(index.Type.Equals("foreign"))
                            {
                                var foreignKeyIndex = index as ForeignIndex;
                                var relation = new Relation();

                                //  Many Side
                                relation.EndPointA=new EndPoint()
                                                       {
                                                           SchemaName = tableConfig.KeepNameAsIs?table.Name:tableConfig.AppacitiveName
                                                       };
                                var schemaNameB = "";
                                var tableB =
                                    database.Tables.First(t => t.Name.Equals(foreignKeyIndex.ReferenceTableName));
                                var tableConfigB =
                                    mappingConfig.TableMappings.First(tm => tm.TableName.Equals(tableB.Name));
                                if (tableConfigB != null)
                                    schemaNameB = tableConfigB.KeepNameAsIs ? tableB.Name : tableConfigB.AppacitiveName;
                                else
                                {
                                    schemaNameB = tableB.Name;
                                }

                                //  One Side
                                relation.EndPointB=new EndPoint()
                                                       {
                                                           SchemaName = schemaNameB
                                                       };
                                var foreignKeyMapping =
                                    tableConfig.ForeignKeyMappings.First(fkm => fkm.ForeignKeyName.Equals(index.Name));
                                if(foreignKeyMapping !=null)
                                {
                                    relation.Name = foreignKeyMapping.KeepNameAsIs
                                                        ? index.Name
                                                        : foreignKeyMapping.AppacitiveRelationName;
                                    relation.Description = foreignKeyMapping.Description ??
                                                           string.Format("Relation for {0}", foreignKeyIndex.Name);
                                    relation.EndPointA.Label = foreignKeyMapping.ManySideLabel;
                                    relation.EndPointB.Label = foreignKeyMapping.OneSideLabel;
                                    relation.EndPointB.Multiplicity = foreignKeyMapping.RestrictManySideMultiplicityTo;
                                    relation.EndPointA.Multiplicity = 1;
                                    relation.Properties.AddRange(foreignKeyMapping.AddPropertiesToRelation);

                                }
                                else
                                {
                                    relation.Name = foreignKeyIndex.Name;
                                    relation.Description = string.Format("Relation from {0} to {1} for {0}",relation.EndPointA.SchemaName, relation.EndPointB.SchemaName, foreignKeyIndex.Name);
                                    relation.EndPointA.Label = relation.EndPointA.SchemaName;
                                    relation.EndPointB.Label = relation.EndPointB.SchemaName;
                                    relation.EndPointB.Multiplicity = -1;
                                    relation.EndPointA.Multiplicity = 1;
                                }
                            }
                        }
                    }
                    
                }
                else
                {
                    //  In absence of table config
                }
            }

            //  Return result ready to be created in Appacitive
            return result;
        }

        private static void ProcessTable(Table table, TableMapping tableConfig, ref AppacitiveInput result)
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
                    throw new Exception("Table does not contain both key and value columns.");
                }

                //  Ensure cannedList key has unique or primary constraint
                var keyCol = table.Columns.Find(col => col.Name.Equals(tableConfig.CannedListKeyColumn));
                if (keyCol.Indexes.Exists(i => i.Type == "unique") == false && keyCol.Indexes.Exists(i => i.Type == "primary") == false)
                {
                    throw new Exception("CannedList key does not have unique or primary key index/constraint");
                }

                //  Add the cannedList to result
                //  Todo-Need to populate the cannedList too.
                result.CannedLists.Add(new CannedList()
                {
                    Name = tableConfig.KeepNameAsIs ? table.Name : tableConfig.AppacitiveName,
                    Description = tableConfig.Description ?? string.Format("CannedList for '{0}'", table.Name),
                    Items = new List<ListItem>()//  Populate later
                });
                return;
            }

            //  Steps to take if this table is a mapping table in many-to-many relationship.
            if (tableConfig.IsJunctionTable)
            {
                var columnA = table.Columns.First(col => col.Name.Equals(tableConfig.JunctionsSideAColumn));
                var columnB = table.Columns.First(col => col.Name.Equals(tableConfig.JunctionsSideBColumn));
                if (columnA == null || columnB == null)
                {
                    throw new Exception("Incorrect mapping tables columns.");
                }
                return;
            }

            //  If regular table, convert to schema
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

                //  Remove ignored foreign key constraints
                tableColumn.Indexes.RemoveAll(
                    i => i.Type.Equals("foreign") && tableConfig.IgnoreForeignKeyConstraints.Contains(i.Name));

                //  Remove ignored unique key constraints
                tableColumn.Indexes.RemoveAll(
                    i => i.Type.Equals("unique") && tableConfig.IgnoreUniqueKeyConstraints.Contains(i.Name));

                //  Process indexes
                //foreach (var index in tableColumn.Indexes)
                //{
                //    switch (index.Type)
                //    {
                //        case "unique":
                //            var uniqueIndex = index as UniqueIndex;

                //            break;
                //        case "primary":
                //            var primaryIndex = index as PrimaryIndex;
                //            break;
                //        case "foreign":
                //            var foreignKeyIndex = index as ForeignIndex;
                //            if(tableConfig.IgnoreForeignKeys.Contains(foreignKeyIndex.Name))

                //            break;
                //    }
                //}

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

        private static void ProcessTable(Table table, ref AppacitiveInput result)
        {
            //  Steps to take in absence of mapping config for this table

            var schema = new Schema
            {
                Name = table.Name,
                Description = string.Format("Schema for {0}", table.Name),
                Properties = new List<Property>()
            };

            //  Process columns
            foreach (var tableColumn in table.Columns)
            {
                var property = new Property();
                property.Name = tableColumn.Name;
                property.Description = string.Format("Property for {0}", property.Name);


                //  Remove ignored foreign key constraints
               
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
    }
}
