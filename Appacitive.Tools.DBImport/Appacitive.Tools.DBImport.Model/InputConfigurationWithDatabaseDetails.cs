using System;
using System.Collections.Generic;

namespace Appacitive.Tools.DBImport.Model
{
    [Serializable]
    public class InputConfigurationWithDatabaseDetails : InputConfiguration
    {
        public DatabaseDetails DatabaseDetails { get; set; }
    }
}