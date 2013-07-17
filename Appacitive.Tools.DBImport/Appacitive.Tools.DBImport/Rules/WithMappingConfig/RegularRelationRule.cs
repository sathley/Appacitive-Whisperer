using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Appacitive.Tools.DBImport.Model;

namespace Appacitive.Tools.DBImport
{
    public class RegularRelationRule : IRule
    {
        public void Apply(Database database, MappingConfig mappingConfig, int tableIndex, ref AppacitiveInput input)
        {
            var table = database.Tables[tableIndex];
            var tableConfig =
                    mappingConfig.TableMappings.FirstOrDefault(t => t.TableName.Equals(database.Tables[tableIndex].Name, StringComparison.InvariantCultureIgnoreCase));

            if (tableConfig != null && (tableConfig.MakeCannedList || tableConfig.IsJunctionTable)) return;
            
            foreach (var column in table.Columns)
            {
                foreach (var index in column.Indexes)
                {
                    if (index.Type.Equals("foreign") == false) return;
                    var fKeyIndex = index as ForeignIndex;
                    var fKeyMapping = tableConfig.ForeignKeyMappings.Find(map => map.ForeignKeyName.Equals(index.Name));
                    var relation = new Relation();

                    var manySideTableName = fKeyIndex.ReferenceTableName;
                    var manySideTable = database.Tables.Find(t => t.Name.Equals(manySideTableName));
                    var manySideTableConfig =
                        mappingConfig.TableMappings.Find(conf => conf.TableName.Equals(manySideTableName));

                    string manySideSchemaName = string.Empty;
                    if(manySideTableConfig==null)
                        manySideSchemaName = manySideTable.Name;
                    else
                    {
                        manySideSchemaName = manySideTableConfig.KeepNameAsIs
                                                 ? manySideTableName
                                                 : manySideTableConfig.AppacitiveName;
                    }

                    //  Should use sub-rules
                    if(fKeyMapping != null)
                    {
                        relation.Name = fKeyMapping.KeepNameAsIs
                                            ? fKeyMapping.ForeignKeyName
                                            : fKeyMapping.AppacitiveRelationName;
                        relation.Description = string.IsNullOrEmpty(fKeyMapping.Description)
                                                   ? string.Format("Relation for '{0}'", fKeyMapping.ForeignKeyName)
                                                   : fKeyMapping.Description;
                        relation.Properties.AddRange(fKeyMapping.AddPropertiesToRelation);
                        relation.EndPointA = new EndPoint
                                                 {
                                                     Multiplicity = 1,
                                                     Label = fKeyMapping.OneSideLabel ?? column.Name,
                                                     SchemaName = tableConfig.KeepNameAsIs
                                                                      ? table.Name
                                                                      : tableConfig.AppacitiveName
                                                 };

                        relation.EndPointB = new EndPoint
                                                 {
                                                     Multiplicity = fKeyMapping.RestrictManySideMultiplicityTo == 0
                                                                        ? -1
                                                                        : fKeyMapping.RestrictManySideMultiplicityTo,
                                                     Label = fKeyMapping.ManySideLabel ?? fKeyIndex.ReferenceColumnName,
                                                     SchemaName = manySideSchemaName
                                                 };



                    
                    }
                    else
                    {
                        relation.Name = fKeyIndex.Name;
                        relation.Description = string.Format("Relation for '{0}'", fKeyMapping.ForeignKeyName);
                        relation.EndPointA = new EndPoint
                        {
                            Multiplicity = 1,
                            Label = column.Name,
                            SchemaName = tableConfig.KeepNameAsIs
                                             ? table.Name
                                             : tableConfig.AppacitiveName
                        };
                        relation.EndPointB = new EndPoint
                        {
                            Multiplicity =  -1,
                            Label = fKeyIndex.ReferenceColumnName,
                            SchemaName = manySideSchemaName
                        };
                    }
                    if (StringValidation.IsAlphanumeric(relation.Name) == false)
                        throw new Exception(string.Format("Incorrect name for relation '{0}'. It should be alphanumeric starting with alphabet.", relation.Name));
                    input.Relations.Add(relation);
                }
            }
        }
    }
}
