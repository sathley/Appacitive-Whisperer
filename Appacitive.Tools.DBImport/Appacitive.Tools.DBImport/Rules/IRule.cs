using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Appacitive.Tools.DBImport.Model;

namespace Appacitive.Tools.DBImport
{
    public interface IRule
    {
        void Apply(ref Table table, TableMapping tableConfig, ref AppacitiveInput input);
    }
}
