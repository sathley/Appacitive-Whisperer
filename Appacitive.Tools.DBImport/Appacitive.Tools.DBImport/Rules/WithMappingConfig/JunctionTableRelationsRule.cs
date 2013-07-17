using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Appacitive.Tools.DBImport.Model;

namespace Appacitive.Tools.DBImport
{
    public class JunctionTableRelationsRule : IRule
    {
        public void Apply(Database database, MappingConfig mappingConfig, int tableIndex, ref AppacitiveInput input)
        {
            var table = database.Tables[tableIndex];
            var tableConfig =
                    mappingConfig.TableMappings.FirstOrDefault(t => t.TableName.Equals(database.Tables[tableIndex].Name, StringComparison.InvariantCultureIgnoreCase));

            if (!tableConfig.IsJunctionTable)   return;

            var juncColA = table.Columns.First(col => col.Name.Equals(tableConfig.JunctionsSideAColumn));
            var juncColB = table.Columns.First(col => col.Name.Equals(tableConfig.JunctionsSideBColumn));

            var juncColAfKeyIndex = juncColA.Indexes.First(i => i.Type.Equals("foriegn")) as ForeignIndex;
            var juncColBFKeyIndex = juncColB.Indexes.First(i => i.Type.Equals("foriegn")) as ForeignIndex;

            var relation = new Relation();
            relation.Name = string.IsNullOrEmpty(tableConfig.JunctionTableRelationName)
                                ? string.Format("{0}_{1}", juncColAfKeyIndex.ReferenceTableName,
                                                juncColBFKeyIndex.ReferenceTableName)
                                : tableConfig.JunctionTableRelationName;

            relation.Description = string.IsNullOrEmpty(tableConfig.JunctionTableRelationDescription)
                                       ? string.Format("Many to many Relation for junction table '{0}'", table.Name)
                                       : tableConfig.JunctionTableRelationDescription;

            relation.EndPointA=new EndPoint();
            relation.EndPointB=new EndPoint();
            relation.EndPointA.Label = string.IsNullOrEmpty(tableConfig.JunctionALabel)
                                           ? juncColAfKeyIndex.ReferenceColumnName
                                           : tableConfig.JunctionALabel;

            relation.EndPointB.Label = string.IsNullOrEmpty(tableConfig.JunctionBLabel)
                                           ? juncColBFKeyIndex.ReferenceColumnName
                                           : tableConfig.JunctionBLabel;

            relation.EndPointA.Multiplicity = tableConfig.JunctionaSideAMultiplicity == 0
                                                  ? -1
                                                  : tableConfig.JunctionaSideAMultiplicity;

            relation.EndPointB.Multiplicity = tableConfig.JunctionaSideBMultiplicity == 0
                                                  ? -1
                                                  : tableConfig.JunctionaSideBMultiplicity;

            //  Currently don't change junction table schemas names
            //var tableA = 




        }
    }
}
