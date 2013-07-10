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
                if(tableConfig != null)
                {
                    //  Steps to take in absense of mapping config for this table

                    //  Remove ignored columns
                    table.Columns.RemoveAll(col => tableConfig.IgnoreColumns.Contains(col.Name));

                    //  If table is to be converted to a cannedList
                    if(tableConfig.MakeCannedList)
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
                                                       Name = tableConfig.KeepNameAsIs?table.Name:tableConfig.AppacitiveSchemaName,
                                                       Description = string.Format("CannedList for '{0}'", table.Name),
                                                       Items = new List<ListItem>()//  Populate later
                                                   });
                        continue;
                    }
                    
                }
                else
                {
                    //  Steps to take in presence of mapping config for this table
                }
            }
            return result;
        }
    }
}
