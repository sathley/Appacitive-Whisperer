using System;
using System.Collections.Generic;

namespace Appacitive.Tools.DBImport.Model
{
    [Serializable]
    public class InputConfiguration
    {
        public AppacitiveDetails AppacitiveDetails { get; set; }

        public List<TableMapping> TableMappings { get; set; }
    }
}