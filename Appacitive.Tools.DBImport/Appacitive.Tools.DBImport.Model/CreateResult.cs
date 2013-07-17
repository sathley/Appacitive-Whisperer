using System;

namespace Appacitive.Tools.DBImport.Model
{
    [Serializable]
    public class CreateResult
    {
        public bool IsSuccessfull { get; set; }

        public string ErrorMessage { get; set; }

        public string Code { get; set; }
    }
}