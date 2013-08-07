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
            var connectionString = "Server=localhost;Database=whisperer;Uid=root;Pwd=test123!@#;";
            var database = "whisperer";
            var dbImport = new DBImport();
            dbImport.Import(new InputConfigurationWithDatabaseDetails()
            {
                AppacitiveDetails = new AppacitiveDetails()
                {

                    BlueprintId = "__EditableBlueprint_junctionTest",
                    APIKey = "ZASGowUEjAy1hpCW9ewORveHSIbxXDO627dzULG/kGo=",
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
                                                TableName = "schemaproperty",
                                                Description = "Relates schema and property",
                                                AppacitiveName = "relation_schema_property",
                                                IsJunctionTable = true,
                                                JunctionALabel = "lbl_schema",
                                                JunctionBLabel = "lbl_property",
                                                JunctionTableRelationDescription = "Relates schema and property (for relation)",
                                                JunctionsSideAColumn = "schemaid",
                                                JunctionsSideBColumn = "propertyid"
                                            }
                                    }
            });
        }
    }
}
