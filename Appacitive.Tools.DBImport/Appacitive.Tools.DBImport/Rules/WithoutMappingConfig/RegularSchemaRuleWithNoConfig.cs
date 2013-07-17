using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Appacitive.Tools.DBImport.Model;

namespace Appacitive.Tools.DBImport
{
    class RegularSchemaRuleWithNoConfig : IRule
    {
        public void Apply(Database database, MappingConfig mappingConfig, int tableIndex, ref Model.AppacitiveInput input)
        {
            throw new NotImplementedException();
        }
    }
}
