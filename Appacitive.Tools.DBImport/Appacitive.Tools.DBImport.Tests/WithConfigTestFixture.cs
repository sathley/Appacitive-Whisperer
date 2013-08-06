using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Appacitive.Tools.DBImport.Model;

namespace Appacitive.Tools.DBImport.Tests
{
    public class WithConfigTestFixture
    {
        public void WithConfigMysqlFixture()
        {
            var connectionString = ConfigurationManager.AppSettings["mysql-connectionstring"];
            var database = ConfigurationManager.AppSettings["mysql-database"];
            var dbImport = new DBImport();
            dbImport.Import(new InputConfigurationWithDatabaseDetails()
            {
                AppacitiveDetails = new AppacitiveDetails()
                {

                    BlueprintId = "__EditableBlueprint_AppacitiveApplication2",
                    APIKey = "2",
                    AppacitiveBaseURL = ConfigurationManager.AppSettings["appacitive-base-url"]
                },
                DatabaseDetails = new DatabaseDetails()
                  {
                      DBConnectionString = connectionString,
                      DBProvider = "mysql",
                      DatabaseName = database,
                  },
                TableMappings = new List<TableMapping>()
                                    {
                                        new TableMapping()
                                            {
                                                TableName = "subject",
                                                MakeCannedList = true,
                                                CannedListDescriptionColumn = "name",
                                                CannedListKeyColumn = "id",
                                                CannedListValueColumn = "name",
                                                KeepNameAsIs=true,
                                                IsJunctionTable=false
                                            }
                                    }
            });
        }
    }
}
