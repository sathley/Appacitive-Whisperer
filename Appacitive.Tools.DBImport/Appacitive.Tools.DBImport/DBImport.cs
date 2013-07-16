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
            var whisperer = new AppacitiveWhisperer(mappingConfig.Input.APIKey, mappingConfig.Input.BlueprintId);
            IDataDefinitionGatherer gatherer = null;

            switch (mappingConfig.Input.DBProvider.ToLower())
            {
                case "mysql":
                    gatherer = new MySqlDataDefinitionGatherer();
                    break;
                case "sql":
                    break;
                default:
                    break;

            }

            if (gatherer == null) return;
            var data = gatherer.GatherData(mappingConfig.Input.DBConnectionString, mappingConfig.Input.DatabaseName);
            var input = new EngineCore().AppacitizeDatabase(data, mappingConfig);
            whisperer.Whisper(input);
        }
    }
}
