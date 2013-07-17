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
                    if (index.Type.Equals("foriegn") == false) return;
                    var fKeyIndex = index as ForeignIndex;
                    var referenceTable = database.Tables.Find(t => t.Name.Equals(fKeyIndex.ReferenceTableName));
                    var referenceTableConfig =
                        mappingConfig.TableMappings.Find(conf => conf.TableName.Equals(referenceTable.Name));
                    if(referenceTableConfig == null || referenceTableConfig.MakeCannedList == false) return;

                    var schemaName = tableConfig.KeepNameAsIs
                                         ? table.Name
                                         : tableConfig.AppacitiveName;
                    var schema = input.Schemata.Find(s => s.Name.Equals(schemaName));
                    string schemaPropertyName = string.Empty;
                    var propertyConfig =tableConfig.PropertyMappings.Find(pm => pm.ColumnName.Equals(column.Name));
                    if (propertyConfig == null)
                        schemaPropertyName = column.Name;
                    else
                    {
                        schemaPropertyName = propertyConfig.KeepNameAsIs
                                                 ? column.Name
                                                 : propertyConfig.AppacitivePropertyName;
                    }
                    var schemaProperty = schema.Properties.Find(p => p.Name.Equals(schemaPropertyName));
                    schemaProperty.AreValuesFromCannedList = true;
                    schemaProperty.CannedListName = referenceTableConfig.KeepNameAsIs
                                                        ? referenceTable.Name
                                                        : referenceTableConfig.AppacitiveName;
                    
                    //  Mark index as cannedList index
                    index.Type = "cannedlist-foreign";

                }
            }
        }
    }
}
