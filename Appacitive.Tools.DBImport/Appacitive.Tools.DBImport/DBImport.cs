using System;
using System.Linq;
using System.Text;
using Appacitive.Tools.DBImport.Model;

namespace Appacitive.Tools.DBImport
{
    public class DBImport
    {
        public void Import(InputConfigurationWithDatabaseDetails config)
        {
            var whisperer = new AppacitiveWhisperer(config.AppacitiveDetails.APIKey, config.AppacitiveDetails.BlueprintId, config.AppacitiveDetails.AppacitiveBaseURL);
            var gatherer =
                GathererContext.GetGatherer(config.DatabaseDetails.DBProvider.ToLower());
            if (gatherer == null) return;
            var databaseStructure = gatherer.GatherData(config.DatabaseDetails.DBConnectionString, config.DatabaseDetails.DatabaseName);
            var appacitiveInput = new EngineCore().AppacitizeDatabase(databaseStructure, config.TableMappings);
            whisperer.Whisper(appacitiveInput);
        }

        public void Import(InputConfigurationWithDatabaseStructure config)
        {
            var whisperer = new AppacitiveWhisperer(config.AppacitiveDetails.APIKey, config.AppacitiveDetails.BlueprintId, config.AppacitiveDetails.AppacitiveBaseURL);
            
            var appacitiveInput = new EngineCore().AppacitizeDatabase(config.Database, config.TableMappings);
            whisperer.Whisper(appacitiveInput);
        }
    }
}
