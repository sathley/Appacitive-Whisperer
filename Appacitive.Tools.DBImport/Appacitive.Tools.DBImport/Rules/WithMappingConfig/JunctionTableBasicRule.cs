﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Appacitive.Tools.DBImport.Model;

namespace Appacitive.Tools.DBImport
{
    class JunctionTableBasicRule : IRule
    {
        public void Apply(Database database, MappingConfig mappingConfig, int tableIndex, ref AppacitiveInput input)
        {
            var table = database.Tables[tableIndex];
            var tableConfig =
                    mappingConfig.TableMappings.FirstOrDefault(t => t.TableName.Equals(database.Tables[tableIndex].Name, StringComparison.InvariantCultureIgnoreCase));

            if(tableConfig != null && tableConfig.MakeCannedList) 
                throw new Exception("Same table can't be marked as both CannedList and Junction Table.");

            //  Steps to take if this table is a mapping table in many-to-many relationship.
            if (tableConfig != null && tableConfig.IsJunctionTable)
            {
                var columnA = table.Columns.First(col => col.Name.Equals(tableConfig.JunctionsSideAColumn));
                var columnB = table.Columns.First(col => col.Name.Equals(tableConfig.JunctionsSideBColumn));
                if (columnA == null || columnB == null)
                {
                    throw new Exception("Incorrect mapping tables columns.");
                }

                if(columnA.Indexes.Exists(i=>i.Type.Equals("foriegn"))==false || columnA.Indexes.Exists(i=>i.Type.Equals("foreign"))==false)
                    throw new Exception("Junction tables both columns (A and B) must have a foreign key index.");
            }

            //  The relation will be created later in another rule
        }
    }
}
