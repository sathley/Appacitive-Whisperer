using System;

namespace Appacitive.Tools.DBImport.Model
{
    [Serializable]
    public class InputConfigurationWithDatabaseStructure : InputConfiguration
    {
        public Database Database { get; set; }
    }
}