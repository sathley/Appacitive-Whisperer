using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Appacitive.Tools.DBImport.Model;

namespace Appacitive.Tools.DBImport
{
    public class CannedListBasicRule : IRule
    {
        public void Apply(Database database, List<TableMapping> tableMappings, int tableIndex, ref AppacitiveInput input)
        {
            var currentTable = database.Tables[tableIndex];

            TableMapping tableMappingForCurrentTable = null;
            if (tableMappings != null && tableMappings.Count != 0)
                tableMappingForCurrentTable = tableMappings.FirstOrDefault(t => t.TableName.Equals(database.Tables[tableIndex].Name, StringComparison.InvariantCultureIgnoreCase));

            //  This rule does not apply in absense of table mapping for this table.
            if (tableMappingForCurrentTable == null)
                return;
            
            if (tableMappingForCurrentTable.MakeCannedList != true) 
                return;

            //  If table is to be converted to a cannedList

            //  Ensure table contains key and value columns
            if (currentTable.Columns.Find(col => col.Name.Equals(tableMappingForCurrentTable.CannedListKeyColumn, StringComparison.InvariantCultureIgnoreCase)) == null ||
                currentTable.Columns.Find(col => col.Name.Equals(tableMappingForCurrentTable.CannedListValueColumn, StringComparison.InvariantCultureIgnoreCase)) == null)
            {
                throw new Exception(string.Format("Table '{0}' does not contain both key '{1}' and value '{2}' columns for it to be made a canned list.", currentTable.Name, tableMappingForCurrentTable.CannedListKeyColumn, tableMappingForCurrentTable.CannedListValueColumn));
            }

            //  Ensure cannedList key has unique or primary constraint
            var keyColumn = currentTable.Columns.Find(col => col.Name.Equals(tableMappingForCurrentTable.CannedListKeyColumn, StringComparison.InvariantCultureIgnoreCase));
            if (keyColumn.Indexes.Exists(i => i.Type.Equals("unique")) == false && keyColumn.Indexes.Exists(i => i.Type.Equals("primary")) == false)
            {
                throw new Exception(string.Format("CannedList key column '{0}' for table '{1}' does not have unique or primary key index/constraint.", keyColumn.Name, currentTable.Name));
            }

            var cannedListName = tableMappingForCurrentTable.KeepNameAsIs ? currentTable.Name : tableMappingForCurrentTable.AppacitiveName;
                
            //  Validate cannedlist name
            if (StringValidationHelper.IsAlphanumeric(cannedListName) == false)
                throw new Exception(string.Format("Incorrect name for cannedlist '{0}'. It should be alphanumeric starting with alphabet.", cannedListName));

            //  Add the cannedList to result
            //  Todo-Need to populate the cannedList too.
            input.CannedLists.Add(new CannedList()
                {
                    Name = cannedListName,
                    Description = tableMappingForCurrentTable.Description ?? string.Format("CannedList for '{0}'", currentTable.Name),
                    Items = new List<ListItem>() //  Populate later
                });
        }
    }
}
