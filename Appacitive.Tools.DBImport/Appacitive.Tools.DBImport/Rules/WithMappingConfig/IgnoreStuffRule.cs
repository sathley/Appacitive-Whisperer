using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Appacitive.Tools.DBImport.Model;

namespace Appacitive.Tools.DBImport
{
    public class IgnoreStuffRule : IRule
    {
        public void Apply(Database database, MappingConfig mappingConfig, int tableIndex, ref AppacitiveInput input)
        {
            var table = database.Tables[tableIndex];
            TableMapping tableMapping = null;
            if(mappingConfig!=null && mappingConfig.TableMappings!=null)
                tableMapping =
                    mappingConfig.TableMappings.FirstOrDefault(t => t.TableName.Equals(database.Tables[tableIndex].Name, StringComparison.InvariantCultureIgnoreCase));

            //  Remove ignored columns
            if (tableMapping == null) return;
            foreach (var ignoredColumn in tableMapping.IgnoreColumns)
            {
                table.Columns.RemoveAll(col => col.Name.Equals(ignoredColumn, StringComparison.InvariantCultureIgnoreCase));
            }

            //  Remove ignored foreign key constraints
            foreach (var ignoredFKey in tableMapping.IgnoreForeignKeyConstraints)
            {
                foreach (var column in table.Columns)
                {
                    column.Indexes.RemoveAll(i => i.Type.Equals("foreign") && i.Name.Equals(ignoredFKey));
                }
            }

            //  Remove ignored unique key constraints
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
