using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Appacitive.Tools.DBImport.Model;

namespace Appacitive.Tools.DBImport
{
    public class CannedListAssignmentRuleWithNoConfig: IRule
    {
        public void Apply(Database database, List<TableMapping> mappingConfig, int tableIndex, ref AppacitiveInput input)
        {
            throw new NotImplementedException();
        }
    }
}
