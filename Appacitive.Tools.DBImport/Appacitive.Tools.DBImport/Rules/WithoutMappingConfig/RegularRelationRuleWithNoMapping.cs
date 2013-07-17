using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Appacitive.Tools.DBImport.Model;

namespace Appacitive.Tools.DBImport
{
    public class RegularRelationRuleWithNoMapping : IRule
    {
        public void Apply(Database database, MappingConfig mappingConfig, int tableIndex, ref AppacitiveInput input)
        {
            var table = database.Tables[tableIndex];
            foreach (var column in table.Columns)
            {
                foreach (var index in column.Indexes)
                {
                    if (index.Type.Equals("foreign") == false) return;
                    var fKeyIndex = index as ForeignIndex;
                    var relation = new Relation();

                    var manySideTableName = fKeyIndex.ReferenceTableName;
                    var manySideTable = database.Tables.Find(t => t.Name.Equals(manySideTableName));

                    var manySideSchemaName = manySideTable.Name;

                    relation.Name = fKeyIndex.Name;
                    if (StringValidation.IsAlphanumeric(relation.Name) == false)
                        throw new Exception(string.Format("Incorrect name for relation '{0}'. It should be alphanumeric starting with alphabet.", relation.Name));
                    relation.Description = string.Format("Relation for '{0}'", fKeyIndex.Name);
                    relation.EndPointA = new EndPoint
                    {
                        Multiplicity = 1,
                        Label = column.Name,
                        SchemaName = table.Name
                    };
                    relation.EndPointB = new EndPoint
                    {
                        Multiplicity = -1,
                        Label = fKeyIndex.ReferenceColumnName,
                        SchemaName = manySideSchemaName
                    };

                    input.Relations.Add(relation);
                }
            }
        }
    }
}
