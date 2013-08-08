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
            //var connectionString = ConfigurationManager.AppSettings["mysql-connectionstring"];
            var connectionString = "Server=localhost;Database=dgossamercore;Uid=root;Pwd=test123!@#;";

            //var database = ConfigurationManager.AppSettings["mysql-database"];
            var database = "dgossamercore";
            var dbImport = new DBImport();
            dbImport.Import(new InputConfigurationWithDatabaseDetails()
                                {
                                    AppacitiveDetails = new AppacitiveDetails()
                                              {

                                                  BlueprintId = "__EditableBlueprint_gossamercoredb",
                                                  APIKey = "D9Z4d1Qk3G0789nemYl+cqsPVSPOGQuPtxy67+gFS1Q=",
                                                  AppacitiveBaseURL = ConfigurationManager.AppSettings["appacitive-base-url"]
                                              },
                                    DatabaseDetails = new DatabaseDetails()
                                      {
                                          DBConnectionString = connectionString,
                                          DBProvider = "mysql",
                                          DatabaseName = database,
                                      },
                                    TableMappings = null
                                });
        }
    }
}
