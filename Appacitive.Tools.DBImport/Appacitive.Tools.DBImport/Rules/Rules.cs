using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Appacitive.Tools.DBImport
{
    public class Rules
    {
        public List<IRule> GetRulesForWithoutMappingConfig()
        {
            return new List<IRule>();
        }

        public List<IRule> GetRulesForWithMappingConfig()
        {
            return new List<IRule>()
                       {
                           new IgnoreStuffRule(),
                           new CannedListRule(),
                           new JunctionTableRule(),
                           new RegularSchemaRule()
                       };
        }
    }
}
