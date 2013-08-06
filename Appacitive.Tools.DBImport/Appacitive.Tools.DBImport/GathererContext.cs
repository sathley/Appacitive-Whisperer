using System.Collections.Generic;
using Appacitive.Tools.DBImport.MySQL;

namespace Appacitive.Tools.DBImport
{
    public static class GathererContext
    {
        public static Dictionary<string, IDataDefinitionGatherer> Gatherers { get; set; } 
        static GathererContext()
        {
            Gatherers=new Dictionary<string, IDataDefinitionGatherer>()
                {
                    {"mysql",new MySqlDataDefinitionGatherer()},
                };
        }

        public static IDataDefinitionGatherer GetGatherer(string dbType)
        {
            return Gatherers[dbType];
        }
    }
}