using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Appacitive.Tools.DBImport
{
    public class Input
    {
        public string BlueprintId { get; set; }

        public string APIKey { get; set; }

        public string AppacitiveBaseURL { get; set; }

        public string DBConnectionString { get; set; }

        public string DatabaseName { get; set; }

        public string DBProvider { get; set; }  //sql, mysql etc.
    }
}
