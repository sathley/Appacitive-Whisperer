using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Appacitive.Tools.DBImport.Model;

namespace Appacitive.Tools.DBImport
{
    public class IgnoreStuffRule : IRule
    {
        public void Apply(Database database, List<TableMapping> tableMappings, int tableIndex, ref AppacitiveInput input)
        {
            var currentTable = database.Tables[tableIndex];

            TableMapping tableMappingForCurrentTable = null;
            if (tableMappings != null && tableMappings.Count != 0)
                tableMappingForCurrentTable = tableMappings.FirstOrDefault(t => t.TableName.Equals(database.Tables[tableIndex].Name, StringComparison.InvariantCultureIgnoreCase));

            //  In absense of table mapping, this rule does not apply.
            if (tableMappingForCurrentTable == null) 
                return;

            //  Remove ignored columns
            foreach (var ignoredColumn in tableMappingForCurrentTable.IgnoreColumns)
            {
                currentTable.Columns.RemoveAll(col => col.Name.Equals(ignoredColumn, StringComparison.InvariantCultureIgnoreCase));
            }

            //  Remove ignored foreign key constraints
            foreach (var ignoredFKey in tableMappingForCurrentTable.IgnoreForeignKeyConstraints)
            {
                foreach (var column in currentTable.Columns)
                {
                    column.Indexes.RemoveAll(i => i.Type.Equals("foreign") && i.Name.Equals(ignoredFKey));
                }
            }

            //  Remove ignored unique key constraints
            foreach (var ignoredUKey in tableMappingForCurrentTable.IgnoreUniqueKeyConstraints)
            {
                foreach (var column in currentTable.Columns)
                {
                    column.Indexes.RemoveAll(i => i.Type.Equals("unique") && i.Name.Equals(ignoredUKey));
                }
            }
        }
    }
}
