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

            //  Step 1. Apply basic rules
            var basicRules = RulesProvider.GetBasicRules();
            foreach (var basicRule in basicRules)
            {
                for (var tableIndex = 0; tableIndex < database.Tables.Count; tableIndex++)
                {
                    basicRule.Apply(database, mappingConfig, tableIndex, ref result);
                }
            }

            //  Step 2. Apply advanced rules
            var advancedRules = RulesProvider.GetAdvancedRules();
            foreach (var advancedRule in advancedRules)
            {
                for (var tableIndex = 0; tableIndex < database.Tables.Count; tableIndex++)
                {
                    advancedRule.Apply(database, mappingConfig, tableIndex, ref result);
                }
            }
            return result;

        }

    }
}