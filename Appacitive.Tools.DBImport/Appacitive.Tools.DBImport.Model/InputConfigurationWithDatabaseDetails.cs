using System;
using System.Collections.Generic;

namespace Appacitive.Tools.DBImport.Model
{
    [Serializable]
    public class InputConfigurationWithDatabaseDetails
    {
        public AppacitiveDetails AppacitiveDetails { get; set; }

        public DatabaseDetails DatabaseDetails { get; set; }

        public List<TableMapping> TableMappings { get; set; } 
    }
}