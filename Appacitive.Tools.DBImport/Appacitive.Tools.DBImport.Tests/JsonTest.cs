using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Appacitive.Tools.DBImport.MySQL;

namespace Appacitive.Tools.DBImport.Tests
{
    public class JsonTest
    {
        public void DatabaseToJSONTest()
        {
            var gatherer = new MySqlDataDefinitionGatherer();
            var database = gatherer.GatherData("Server=localhost;Database=dgossamercore;Uid=root;Pwd=test123!@#;", "dgossamercore");
            var jsonString = database.ToJSON();


        }
    }
}
