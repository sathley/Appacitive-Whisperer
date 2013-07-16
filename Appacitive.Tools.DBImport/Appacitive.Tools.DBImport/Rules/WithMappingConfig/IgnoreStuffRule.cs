using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Appacitive.Tools.DBImport.Model;

namespace Appacitive.Tools.DBImport
{
    public class IgnoreStuffRule : IRule
    {
        public void Apply(ref Table table, TableMapping tableMapping, ref AppacitiveInput input)
        {

            //  Remove columns
            foreach (var ignoredColumn in tableMapping.IgnoreColumns)
            {
                table.Columns.RemoveAll(col => col.Name.Equals(ignoredColumn, StringComparison.InvariantCultureIgnoreCase));
            }

            //  Remove foreign key constraints
            foreach (var ignoredFKey in tableMapping.IgnoreForeignKeyConstraints)
            {
                foreach (var column in table.Columns)
                {
                    column.Indexes.RemoveAll(i => i.Type.Equals("foreign") && i.Name.Equals(ignoredFKey));
                }
            }

            //  Remove unique key constraints
            foreach (var ignoredUKey in tableMapping.IgnoreUniqueKeyConstraints)
            {
                foreach (var column in table.Columns)
                {
                    column.Indexes.RemoveAll(i => i.Type.Equals("unique") && i.Name.Equals(ignoredUKey));
                }
            }
        }
    }
}
