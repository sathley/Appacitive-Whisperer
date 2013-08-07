using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Appacitive.Tools.DBImport.Model;

namespace Appacitive.Tools.DBImport
{
    public class CannedListAssignmentRule : IRule
    {
        public void Apply(Database database, List<TableMapping> tableMappings, int tableIndex, ref AppacitiveInput input)
        {
            var currentTable = database.Tables[tableIndex];
            
            TableMapping tableConfig = null;
            if (tableMappings != null && tableMappings.Count != 0)
                tableConfig = tableMappings.FirstOrDefault(t => t.TableName.Equals(database.Tables[tableIndex].Name, StringComparison.InvariantCultureIgnoreCase));
            else
            {
                return;
            }

            foreach (var column in currentTable.Columns)
            {
                foreach (var index in column.Indexes)
                {
                    if (index.Type.Equals("foreign") == false)
                        continue;

                    var fKeyIndex = index as ForeignIndex;

                    //  Get table that became cannedList
                    var referenceTable = database.Tables.Find(t => t.Name.Equals(fKeyIndex.ReferenceTableName, StringComparison.InvariantCultureIgnoreCase));
                    if (referenceTable == null)
                        throw new Exception(string.Format("CannedList table '{0}' not found.", fKeyIndex.ReferenceTableName));

                    var referenceTableConfig =
                        tableMappings.Find(conf => conf.TableName.Equals(referenceTable.Name, StringComparison.InvariantCultureIgnoreCase));

                    if (referenceTableConfig == null || referenceTableConfig.MakeCannedList == false)
                        return;

                    string schemaName;
                    if (tableConfig == null)
                        schemaName = currentTable.Name;
                    else
                        schemaName = tableConfig.KeepNameAsIs ? currentTable.Name : tableConfig.AppacitiveName;

                    var schema = input.Schemata.Find(s => s.Name.Equals(schemaName, StringComparison.InvariantCultureIgnoreCase));
                    if (schema == null)
                        return;

                    string schemaPropertyName = string.Empty;
                    if (tableConfig == null)
                    {
                        schemaPropertyName = column.Name;
                    }
                    else
                    {
                        var propertyConfig = tableConfig.PropertyMappings.Find(pm => pm.ColumnName.Equals(column.Name, StringComparison.InvariantCultureIgnoreCase));
                        if (propertyConfig == null)
                            schemaPropertyName = column.Name;
                        else
                        {
                            schemaPropertyName = propertyConfig.KeepNameAsIs ? column.Name : propertyConfig.AppacitivePropertyName;
                        }
                    }

                    var schemaProperty = schema.Properties.Find(p => p.Name.Equals(schemaPropertyName, StringComparison.InvariantCultureIgnoreCase));
                    if (schemaProperty == null)
                        return;

                    schemaProperty.AreValuesFromCannedList = true;
                    schemaProperty.CannedListName = referenceTableConfig.KeepNameAsIs ? referenceTable.Name : referenceTableConfig.AppacitiveName;

                    //  Mark index as cannedList index
                    index.Type = "cannedlist-foreign";
                }
            }
        }
    }
}
