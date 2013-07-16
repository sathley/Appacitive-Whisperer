﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Appacitive.Tools.DBImport.Model;

namespace Appacitive.Tools.DBImport
{
    public class CannedListRule : IRule
    {
        public void Apply(ref Table table, TableMapping tableConfig, ref AppacitiveInput input)
        {
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
                input.CannedLists.Add(new CannedList()
                {
                    Name = tableConfig.KeepNameAsIs ? table.Name : tableConfig.AppacitiveName,
                    Description = tableConfig.Description ?? string.Format("CannedList for '{0}'", table.Name),
                    Items = new List<ListItem>()//  Populate later
                });
                return;
            }
        }
    }
}
