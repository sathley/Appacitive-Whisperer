using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Appacitive.Tools.DBImport.Model;

namespace Appacitive.Tools.DBImport
{
    public interface IRule
    {
        void Apply(Database database, MappingConfig mappingConfig, int tableIndex, ref AppacitiveInput input);
    }
}
