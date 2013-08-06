using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Appacitive.Tools.DBImport.Model;

namespace Appacitive.Tools.DBImport
{
    public class RegularRelationRule : IRule
    {
        public void Apply(Database database, List<TableMapping> tableMappings, int tableIndex, ref AppacitiveInput input)
        {
            var currentTable = database.Tables[tableIndex];

            TableMapping tableConfig = null;
            if (tableMappings != null && tableMappings.Count != 0)
                tableConfig = tableMappings.FirstOrDefault(t => t.TableName.Equals(database.Tables[tableIndex].Name, StringComparison.InvariantCultureIgnoreCase));

            if (tableConfig == null)
            {
                new RegularRelationRuleWithNoMapping().Apply(database, tableMappings, tableIndex, ref input);
                return;
            }


            if (tableConfig.MakeCannedList || tableConfig.IsJunctionTable)
                return;

            foreach (var column in currentTable.Columns)
            {
                foreach (var index in column.Indexes)
                {
                    if (index.Type.Equals("foreign") == false)
                        return;
                    var fKeyIndex = index as ForeignIndex;

                    var fKeyMapping = tableConfig.ForeignKeyMappings.Find(map => map.ForeignKeyName.Equals(index.Name, StringComparison.InvariantCultureIgnoreCase));
                    var relation = new Relation();

                    var manySideTableName = fKeyIndex.ReferenceTableName;
                    var manySideTable = database.Tables.Find(t => t.Name.Equals(manySideTableName, StringComparison.InvariantCultureIgnoreCase));

                    if (manySideTable == null)
                        throw new Exception(string.Format("Many side table '{0}' not found for relation for foreign key index '{1}'.", manySideTableName, fKeyIndex.Name));

                    var manySideTableConfig = tableMappings.Find(conf => conf.TableName.Equals(manySideTableName, StringComparison.InvariantCultureIgnoreCase));

                    string manySideSchemaName;//    Schema created for many side table

                    if (manySideTableConfig == null)
                        manySideSchemaName = manySideTable.Name;
                    else
                    {
                        manySideSchemaName = manySideTableConfig.KeepNameAsIs ? manySideTableName : manySideTableConfig.AppacitiveName;
                    }

                    var manySideSchema = input.Schemata.Find(s => s.Name.Equals(manySideSchemaName, StringComparison.InvariantCultureIgnoreCase));
                    if (manySideSchema == null)
                        throw new Exception(string.Format("No schema found for table '{0}'.", manySideTable.Name));

                    //  Should use sub-rules
                    if (fKeyMapping != null)
                    {
                        relation.Name = fKeyMapping.KeepNameAsIs ? fKeyMapping.ForeignKeyName : fKeyMapping.AppacitiveRelationName;
                        relation.Description = fKeyMapping.Description ?? string.Empty;
                        foreach (var property in fKeyMapping.AddPropertiesToRelation)
                        {
                            if (StringValidationHelper.IsAlphanumeric(property.Name) == false)
                                throw new Exception(string.Format("Incorrect name for property '{0}' in relation '{1}'. It should be alphanumeric starting with alphabet.", property.Name, relation.Name));
                        }
                        relation.Properties.AddRange(fKeyMapping.AddPropertiesToRelation);
                        relation.EndPointA = new EndPoint
                                                 {
                                                     Multiplicity = 1,
                                                     Label = fKeyMapping.OneSideLabel ?? column.Name,
                                                     SchemaName = tableConfig.KeepNameAsIs ? currentTable.Name : tableConfig.AppacitiveName
                                                 };

                        relation.EndPointB = new EndPoint
                                                 {
                                                     Multiplicity = fKeyMapping.RestrictManySideMultiplicityTo == 0 ? -1 : fKeyMapping.RestrictManySideMultiplicityTo,
                                                     Label = fKeyMapping.ManySideLabel ?? fKeyIndex.ReferenceColumnName,
                                                     SchemaName = manySideSchema.Name
                                                 };
                    }
                    else
                    {
                        relation.Name = fKeyIndex.Name;
                        relation.Description = string.Format("Relation for '{0}'.", fKeyMapping.ForeignKeyName);
                        relation.EndPointA = new EndPoint
                                                 {
                                                     Multiplicity = 1,
                                                     Label = column.Name,
                                                     SchemaName = tableConfig.KeepNameAsIs ? currentTable.Name : tableConfig.AppacitiveName
                                                 };
                        relation.EndPointB = new EndPoint
                                                 {
                                                     Multiplicity = -1,
                                                     Label = fKeyIndex.ReferenceColumnName,
                                                     SchemaName = manySideSchema.Name
                                                 };
                    }
                    //  Validate relation name
                    if (StringValidationHelper.IsAlphanumeric(relation.Name) == false)
                        throw new Exception(string.Format("Incorrect name for relation '{0}'. It should be alphanumeric starting with alphabet.", relation.Name));

                    input.Relations.Add(relation);
                }
            }
        }
    }
}
