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

            //  Whole method to be replaced with a simple rules processing engine

            for (int i = 0; i < database.Tables.Count; i++)
            {
                var tableConfig =
                    mappingConfig.TableMappings.FirstOrDefault(
                        t => t.TableName.Equals(database.Tables[i].Name, StringComparison.InvariantCultureIgnoreCase));
                
                List<IRule> rules;
                rules = tableConfig != null ? RulesProvider.GetRulesForWithMappingConfig() : RulesProvider.GetRulesForWithoutMappingConfig();
                foreach (var rule in rules)
                {
                    rule.Apply(database, null, i, ref result);
                }
            }
            return result;

        }

    }
}