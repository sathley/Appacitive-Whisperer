using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Appacitive.Tools.DBImport.Model;

namespace Appacitive.Tools.DBImport
{
    public class EngineCore
    {
        public AppacitiveInput AppacitizeDatabase(Database database, MappingConfig mappingConfig)
        {
            var result = new AppacitiveInput();

            for (int i = 0; i < database.Tables.Count; i++)
            {
                TableMapping tableConfig = null;
                List<IRule> rules = null;
                if (mappingConfig != null && mappingConfig.TableMappings != null)
                {
                    rules = mappingConfig.TableMappings.Exists(tm =>
                                                               tm.TableName.Equals(database.Tables[i].Name,
                                                                                   StringComparison.InvariantCultureIgnoreCase)) ? RulesProvider.GetRulesForWithMappingConfig() : RulesProvider.GetRulesForWithoutMappingConfig();
                }
                else
                {
                    rules = RulesProvider.GetRulesForWithoutMappingConfig();
                }
                if (rules != null)
                    foreach (var rule in rules)
                    {
                        rule.Apply(database, null, i, ref result);
                    }
            }
            return result;

        }

    }
}