using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Appacitive.Tools.DBImport
{
    public static class RulesProvider
    {
        public static List<IRule> GetRulesForWithoutMappingConfig()
        {
            return new List<IRule>();
        }

        public static List<IRule> GetRulesForWithMappingConfig()
        {
            return new List<IRule>()
                       {
                           new IgnoreStuffRule(),
                           new CannedListBasicRule(),
                           new JunctionTableRule(),
                           new RegularSchemaRule(),
                           new JunctionTableRelationsRule(),
                           new CannedListAssignmentRule()
                       };
        }
    }
}
