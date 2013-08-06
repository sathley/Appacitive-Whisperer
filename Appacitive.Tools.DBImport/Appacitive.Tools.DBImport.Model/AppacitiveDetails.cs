using System;

namespace Appacitive.Tools.DBImport.Model
{
    [Serializable]
    public class AppacitiveDetails
    {
        public string BlueprintId { get; set; }

        public string APIKey { get; set; }

        public string AppacitiveBaseURL { get; set; }
    }
}