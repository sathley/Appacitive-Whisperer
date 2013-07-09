using System;
using System.Linq;
using System.Text;

namespace Appacitive.Tools.DBImport
{
    public static class EngineCore
    {
        public static AppacitiveInput AppacitizeDatabase(this Database database, MappingConfig mappingConfig)
        {
            var result = new AppacitiveInput();
            foreach (var table in database.Tables)
            {
                var tableConfig =
                    mappingConfig.TableMappings.FirstOrDefault(t => t.TableName.Equals(table.Name, StringComparison.InvariantCultureIgnoreCase));
                if(tableConfig != null)
                {
                    if(tableConfig.MakeCannedList)
                    {
                        table.Columns.RemoveAll(col => tableConfig.IgnoreColumns.Contains(col.Name));
                        if(table.Columns.Count !=2)
                            throw new Exception("CannedList table can only have 2 columns. And one of them has to be primary key column.");

                        result.CannedLists.Add(new CannedList()
                                                   {
                                                       Name = tableConfig.KeepNameAsIs?tableConfig.TableName:tableConfig.AppacitiveName,
                                                       Description = string.Format("Canned List for representing {0}", tableConfig.KeepNameAsIs ? tableConfig.TableName : tableConfig.AppacitiveName)
                                                   });

                    }
                }
            }
            return result;
        }
    }
}
