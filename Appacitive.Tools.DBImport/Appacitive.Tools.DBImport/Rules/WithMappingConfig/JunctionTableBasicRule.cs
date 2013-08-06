using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Appacitive.Tools.DBImport.Model;

namespace Appacitive.Tools.DBImport
{
    class JunctionTableBasicRule : IRule
    {
        public void Apply(Database database, List<TableMapping> tableMappings, int tableIndex, ref AppacitiveInput input)
        {
            var currentTable = database.Tables[tableIndex];

            TableMapping tableConfigForCurrentTable = null;
            if (tableMappings != null && tableMappings.Count != 0)
                tableConfigForCurrentTable = tableMappings.FirstOrDefault(t => t.TableName.Equals(database.Tables[tableIndex].Name, StringComparison.InvariantCultureIgnoreCase));

            //  This rule does not apply in absense of table mapping for this table.
            if (tableConfigForCurrentTable == null)
                return;

            if (tableConfigForCurrentTable.MakeCannedList && tableConfigForCurrentTable.IsJunctionTable)
                throw new Exception("Same table can't be marked as both CannedList and Junction Table.");

            //  Steps to take if this table is a mapping table in a many-to-many relationship.
            if (tableConfigForCurrentTable.IsJunctionTable)
            {
                var columnA = currentTable.Columns.First(col => col.Name.Equals(tableConfigForCurrentTable.JunctionsSideAColumn, StringComparison.InvariantCultureIgnoreCase));
                var columnB = currentTable.Columns.First(col => col.Name.Equals(tableConfigForCurrentTable.JunctionsSideBColumn, StringComparison.InvariantCultureIgnoreCase));
                if (columnA == null || columnB == null)
                {
                    throw new Exception(string.Format("Incorrect mapping tables columns for junction table '{0}'.", currentTable.Name));
                }

                if (columnA.Indexes.Exists(i => i.Type.Equals("foreign")) == false || columnB.Indexes.Exists(i => i.Type.Equals("foreign")) == false)
                    throw new Exception(string.Format("Junction tables '{2}' both columns ('{0}' and '{1}') must have a foreign key index.", columnA.Name, columnB.Name, currentTable.Name));
            }

            //  The relation will be created later in another rule. This was just a sanity check.
        }
    }
}
