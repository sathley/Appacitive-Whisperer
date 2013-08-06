using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Appacitive.Tools.DBImport.Model;

namespace Appacitive.Tools.DBImport
{
    public class EngineCore
    {
        public AppacitiveInput AppacitizeDatabase(Database database, List<TableMapping> mappingConfig)
        {
            var result = new AppacitiveInput();


            var rules = RulesProvider.GetBasicRules();
            foreach (var rule in rules)
            {
                for (int i = 0; i < database.Tables.Count; i++)
                {
                    rule.Apply(database, mappingConfig, i, ref result);
                }
            }

            rules = RulesProvider.GetAdvancedRules();
            foreach (var rule in rules)
            {
                for (int i = 0; i < database.Tables.Count; i++)
                {
                    rule.Apply(database, mappingConfig, i, ref result);
                }
            }

            //for (int i = 0; i < database.Tables.Count; i++)
            //{
            //    TableMapping tableConfig = null;
            //    List<IRule> rules = null;
            //    if (mappingConfig != null && mappingConfig.TableMappings != null)
            //    {
            //        rules = mappingConfig.TableMappings.Exists(tm =>
            //                                                   tm.TableName.Equals(database.Tables[i].Name,
            //                                                                       StringComparison.InvariantCultureIgnoreCase)) ? RulesProvider.GetRulesForWithMappingConfig() : RulesProvider.GetRulesForWithoutMappingConfig();
            //    }
            //    else
            //    {
            //        rules = RulesProvider.GetRulesForWithoutMappingConfig();
            //    }
            //    if (rules != null)
            //        foreach (var rule in rules)
            //        {
            //            rule.Apply(database, mappingConfig, i, ref result);
            //        }
            //}
            return result;

        }

    }
}