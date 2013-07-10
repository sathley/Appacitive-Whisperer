using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data;
using MySql.Web;

namespace Appacitive.Tools.DBImport.MySQL
{
    public class MySqlDataDefinitionGatherer : IDataDefinitionGatherer
    {
        private IRelationalDatabase _driver = new MySqlDriver();

        public Database GatherData(string connectionString, string database)
        {
            var result = new Database();
            //  Iniialize driver
            _driver.ConnectionString = connectionString;

            //  Get all tables
            var tableNames = new List<string>();
            var ds = _driver.ExecuteQuery(new DataCommand()
                                     {
                                         CommandText =
                                             string.Format("SHOW TABLES;")
                                     });
            if (ds == null) return null;
            if (ds.Tables.Count == 0) return null;
            var table = ds.Tables[0];
            if (table.Rows.Count == 0) return null;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                tableNames.Add(table.Rows[i][0] as string);
            }

            //  Process tables
            foreach (var tableName in tableNames)
            {
                var newTable = new Table()
                                   {
                                       Name = tableName
                                   };
                ds = _driver.ExecuteQuery(new DataCommand()
                {
                    CommandText =
                        string.Format("DESCRIBE `{0}`;", tableName)
                });
                table = ds.Tables[0];

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    var dataType = table.Rows[i]["Type"] as string;
                    var index = dataType.IndexOf('(');
                    if (index > 0)
                        dataType = dataType.Substring(0, index);
                    var column = new Column()
                                     {
                                         Name = table.Rows[i]["Field"] as string,
                                         Type = (DbDataType)Enum.Parse(typeof(DbDataType), dataType, true),

                                     };

                    //  Not null Consrtaint
                    if (table.Rows[i]["Null"].Equals("NO"))
                        column.Constraints.Add(new NotNullConstraint());

                    //  Default constraint
                    if (table.Rows[i]["Default"].Equals(DBNull.Value) == false)
                        column.Constraints.Add(new DefaultConstraint()
                                                   {
                                                       DefaultValue = table.Rows[i]["Default"]
                                                   });

                    //  Foreign Key indexes
                    var ds1 = _driver.ExecuteQuery(new DataCommand()
                                                  {
                                                      CommandText = string.Format("USE INFORMATION_SCHEMA; SELECT * FROM KEY_COLUMN_USAGE WHERE TABLE_SCHEMA = '{0}' AND TABLE_NAME = '{1}' AND COLUMN_NAME = '{2}' AND REFERENCED_TABLE_NAME <> 'null';", database, tableName, column.Name)
                                                  });
                    var table1 = ds1.Tables[0];
                    for (int j = 0; j < table1.Rows.Count; j++)
                    {
                        column.Indexes.Add(new ForeignIndex()
                                                   {
                                                       ReferenceTableName = table1.Rows[j]["REFERENCED_TABLE_NAME"] as string,
                                                       ReferenceColumnName = table1.Rows[j]["REFERENCED_COLUMN_NAME"] as string
                                                   });
                    }

                    var ds2 = _driver.ExecuteQuery(new DataCommand()
                                                       {
                                                           CommandText = string.Format("SHOW INDEXES FROM `{0}` WHERE `Column_name` = '{1}' AND `Non_unique` = 0;", tableName, column.Name)
                                                       });
                    var table2 = ds2.Tables[0];
                    for (int k = 0; k < table2.Rows.Count; k++)
                    {
                        //  Primary Key index
                        if ((table2.Rows[k]["Key_name"] as string).Equals("PRIMARY"))
                            column.Indexes.Add(new PrimaryIndex()
                                                   {
                                                       SequenceInIndex = (long)table2.Rows[k]["Seq_in_index"]
                                                   });
                        else
                            //  Unique key indexes
                            column.Indexes.Add(new UniqueIndex()
                                                   {
                                                       IndexName = table2.Rows[k]["Key_name"] as string,
                                                       SequenceInIndex = (long)table2.Rows[k]["Seq_in_index"],
                                                   });
                    }
                    newTable.Columns.Add(column);

                }
                result.Tables.Add(newTable);
            }
            return result;
        }
    }
}
