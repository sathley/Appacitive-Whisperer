using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Appacitive.Tools.DBImport.Model;
using Appacitive.Tools.DBImport.MySQL;

namespace Appacitive.Tools.DBImport
{
    public class DBImport
    {
        public void Import(MappingConfig mappingConfig)
        {
            AppacitiveWhisperer whisperer = new AppacitiveWhisperer(mappingConfig.Input.APIKey, mappingConfig.Input.BlueprintId);
            //switch (mappingConfig.Input.DBProvider.ToLower())
            //{
            //    case "mysql":
            //        //var gatherer = new MySqlDataDefinitionGatherer();

            //}

        }
    }
}
