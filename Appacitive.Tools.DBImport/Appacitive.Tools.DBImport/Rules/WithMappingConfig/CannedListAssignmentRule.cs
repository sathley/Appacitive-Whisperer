using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Appacitive.Tools.DBImport.Model;

namespace Appacitive.Tools.DBImport
{
    public class CannedListAssignmentRule : IRule
    {
        public void Apply(Database database, MappingConfig mappingConfig, int tableIndex, ref AppacitiveInput input)
        {
            var table = database.Tables[tableIndex];
            var tableConfig =
                    mappingConfig.TableMappings.FirstOrDefault(t => t.TableName.Equals(database.Tables[tableIndex].Name, StringComparison.InvariantCultureIgnoreCase));

            foreach (var column in table.Columns)
            {
                foreach (var index in column.Indexes)
                {
                    if (!index.Type.Equals("foriegn")) return;
                    var fKeyIndex = index as ForeignIndex;
                    var referenceTable = database.Tables.Find(t => t.Name.Equals(fKeyIndex.ReferenceTableName));
                    var referenceTableConfig =
                        mappingConfig.TableMappings.Find(conf => conf.TableName.Equals(referenceTable.Name));
                    if(referenceTableConfig == null || referenceTableConfig.MakeCannedList == false) return;

                    var schemaName = referenceTableConfig.KeepNameAsIs
                                         ? referenceTable.Name
                                         : referenceTableConfig.AppacitiveName;
                    var schema = input.Schemata.Find(s => s.Name.Equals(schemaName));
                    

                }
            }
        }
    }
}
