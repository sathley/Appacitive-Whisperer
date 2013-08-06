using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Appacitive.Tools.DBImport.Model
{
    [Serializable]
    public class DatabaseDetails
    {
        public string DBConnectionString { get; set; }

        public string DatabaseName { get; set; }

        public string DBProvider { get; set; }  //sql, mysql etc.
    }
}
