using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace Appacitive.Tools.DBImport.MySQL
{
    public class MySqlDriver : IRelationalDatabase
    {
        public string ConnectionString { get; set; }

        public System.Data.DataSet ExecuteQuery(DataCommand command)
        {
            MySqlConnection conn = null;
            var ds = new DataSet();
            try
            {
                conn = new MySqlConnection(ConnectionString);
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = command.CommandText;
                cmd.Prepare();
                foreach (var parameter in command.Parameters)
                {
                    cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);
                }
                var adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(ds);

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }

            }
            return ds;
        }

        public void ExecuteNonQuery(DataCommand command)
        {
            MySqlConnection conn = null;

            try
            {
                conn = new MySqlConnection(ConnectionString);
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = command.CommandText;
                cmd.Prepare();
                foreach (var parameter in command.Parameters)
                {
                    cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);
                }
                cmd.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }

            }
        }
    }

    public static class CommandBuilderUtility
    {
        public static string BuildSp(string spName, params object[] parameters)
        {
            var commandBuilder = new StringBuilder();
            commandBuilder.Append("call ").Append(spName);
            if (!parameters.Any())
            {
                commandBuilder.Append("();");
                return commandBuilder.ToString();
            }
            commandBuilder.Append("(").Append(GetValue(parameters.First())).Append("");
            foreach (var parameter in parameters.Skip(1))
            {
                commandBuilder.Append(",").Append(GetValue(parameter)).Append("");
            }
            commandBuilder.Append(");");
            return commandBuilder.ToString();
        }

        private static string GetValue(object param)
        {
            if (param == null)
                return "null";
            string value = string.Empty;
            bool quotes = param is string || param is DateTime;
            if (param is string) value = (string)param;
            if (param is DateTime) value = ((DateTime)param).ToString("yyyy-MM-dd HH:mm:ss");
            if (quotes)
            {
                return "'" + (value).EscapeSingleQuote() + "'";
            }
            return param.ToString();
        }

        public static string EscapeSingleQuote(this string parameter)
        {
            string oldVal = "'";
            string newVal = "\\'";
            parameter = parameter.Replace(@"\", @"\\");
            string escaped = parameter.Replace(oldVal, newVal);
            return escaped;
        }
    }
}