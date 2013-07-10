using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Appacitive.Tools.DBImport.MySQL;

namespace Appacitive.Tools.DBImport.Tests
{
    public class MySqlFixtures
    {
        public void TestMysqlGathere()
        {
            var connString = "Server=sirius;Database=iidentity;Uid=root;Pwd=test123!@#;";
            var db = "iidentity";
            var gatherer = new MySqlDataDefinitionGatherer();
            var database =gatherer.GatherData(connString, db);
        }
    }
}
