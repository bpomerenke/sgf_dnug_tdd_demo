using System;
using System.Data.SqlClient;

namespace TDDDemoApp.FunctionalTest
{
    public static class DatabaseHelper
    {

        public static void ExecuteCommand(string cmdText)
        {
            UsingConnection(cmdText, command => command.ExecuteNonQuery());
        }

        private static TReturn UsingConnection<TReturn>(string query, Func<SqlCommand, TReturn> action)
        {
            using (var connection = new SqlConnection("Data Source=localhost;Initial Catalog=TDDDemoAppDb;Integrated Security=SSPI;"))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.CommandTimeout = 0;
                    return action(command);
                }
            }
        }
    }
}