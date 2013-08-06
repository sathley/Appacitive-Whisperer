using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Appacitive.Tools.DBImport.Model;

namespace Appacitive.Tools.DBImport
{
    public class CannedListBasicRule : IRule
    {
        public void Apply(Database database, List<TableMapping> mappingConfig, int tableIndex, ref AppacitiveInput input)
        {
            var table = database.Tables[tableIndex];
            TableMapping tableConfig = null;
            if (mappingConfig != null)
                tableConfig =
                    mappingConfig.FirstOrDefault(t => t.TableName.Equals(database.Tables[tableIndex].Name, StringComparison.InvariantCultureIgnoreCase));


            if(tableConfig == null) return;

            //  If table is to be converted to a cannedList
            if (tableConfig.MakeCannedList)
            {
                //  Ensure table contains key and value columns
                if (table.Columns.Find(col => col.Name.Equals(tableConfig.CannedListKeyColumn)) == null ||
                    table.Columns.Find(col => col.Name.Equals(tableConfig.CannedListValueColumn)) == null)
                {
                    throw new Exception("Table does not contain both key and value columns.");
                }

                //  Ensure cannedList key has unique or primary constraint
                var keyCol = table.Columns.Find(col => col.Name.Equals(tableConfig.CannedListKeyColumn));
                if (keyCol.Indexes.Exists(i => i.Type.Equals("unique", StringComparison.InvariantCultureIgnoreCase)) == false &&
                    keyCol.Indexes.Exists(i => i.Type.Equals("primary", StringComparison.InvariantCultureIgnoreCase)) == false)
                {
                    throw new Exception("CannedList key does not have unique or primary key index/constraint.");
                }

                var cannedListName = tableConfig.KeepNameAsIs ? table.Name : tableConfig.AppacitiveName;
                //  Validate cannedlist name
                if (
                    StringValidationHelper.IsAlphanumeric(tableConfig.KeepNameAsIs
                                                              ? table.Name
                                                              : tableConfig.AppacitiveName) == false)
                    throw new Exception(
                        string.Format(
                            "Incorrect name for cannedlist '{0}'. It should be alphanumeric starting with alphabet.",
                            cannedListName));

                //  Add the cannedList to result
                //  Todo-Need to populate the cannedList too.
                input.CannedLists.Add(new CannedList()
                                          {
                                              Name = cannedListName,
                                              Description =
                                                  tableConfig.Description ??
                                                  string.Format("CannedList for '{0}'", table.Name),
                                              Items = new List<ListItem>() //  Populate later
                                          });
            }
            
        }
    }
}
