using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Appacitive.Tools.DBImport.Model;

namespace Appacitive.Tools.DBImport
{
    class JunctionTableRule : IRule
    {
        public void Apply(ref Table table, TableMapping tableConfig, ref AppacitiveInput input)
        {
            if(tableConfig.MakeCannedList) throw new Exception("Same table can't be marked as both Canned List and Junction Table.");
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

            //  The relation will be created later in another rule
        }
    }
}
