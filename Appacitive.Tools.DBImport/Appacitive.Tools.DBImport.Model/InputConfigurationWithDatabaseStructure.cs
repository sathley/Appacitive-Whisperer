using System;
using System.Collections.Generic;

namespace Appacitive.Tools.DBImport.Model
{
    [Serializable]
    public class InputConfigurationWithDatabaseStructure
    {
        public AppacitiveDetails AppacitiveDetails { get; set; }

        public Database Database { get; set; }

        public List<TableMapping> TableMappings { get; set; }
    }
}