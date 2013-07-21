using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Appacitive.Tools.DBImport
{
    public static class RulesProvider
    {
        //public static List<IRule> GetRulesForWithoutMappingConfig()
        //{
        //    return new List<IRule>()
        //               {
        //                   new RegularSchemaRuleWithNoConfig(),
        //                   new RegularRelationRuleWithNoMapping()
        //               };
        //}

        //public static List<IRule> GetRulesForWithMappingConfig()
        //{
        //    return new List<IRule>()
        //               {
        //                   new IgnoreStuffRule(),
        //                   new CannedListBasicRule(),
        //                   new JunctionTableBasicRule(),
        //                   new RegularSchemaRule(),
        //                   new JunctionTableRelationsRule(),
        //                   new CannedListAssignmentRule(),
        //                   new RegularRelationRuleWithConfig()
        //               };
        //}

        public static List<IRule> GetBasicRules()
        {
            return new List<IRule>()
                       {
                           new IgnoreStuffRule(),
                           new CannedListBasicRule(),
                           new JunctionTableBasicRule(),
                           new RegularSchemaRule(),
                           
                       };
        }

        public static List<IRule> GetAdvancedRules()
        {
            return new List<IRule>()
                       {
                           new JunctionTableRelationsRule(),
                           new CannedListAssignmentRule(),
                           new RegularRelationRule()
                       };
        }
    }
}
