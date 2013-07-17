using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Appacitive.Tools.DBImport.Model;

namespace Appacitive.Tools.DBImport.Tests
{
    public class WithoutConfigTestFixtures
    {
        public void TestWithoutConfigWhisperingUsingMysqlDB()
        {
            var connectionString = ConfigurationManager.AppSettings["mysql-connectionstring"];
            var database = ConfigurationManager.AppSettings["mysql-database"];
            var dbImport = new DBImport();
            dbImport.Import(new MappingConfig()
                                {
                                    Input=new Input()
                                              {
                                                  DBConnectionString = connectionString,
                                                  DBProvider = "mysql",
                                                  DatabaseName = database,
                                                  BlueprintId = "0",
                                                  APIKey = "1",
                                                  AppacitiveBaseURL = ConfigurationManager.AppSettings["appacitive-base-url"]
                                              },
                                              TableMappings = null
                                });
        }
    }
}
