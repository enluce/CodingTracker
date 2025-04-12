using System.Collections;
using System.Configuration;
using Dapper;
using Microsoft.Data.Sqlite;

namespace CodingTracker
{
    internal class DatabaseManager
    {
        static string connectionString = ConfigurationManager.AppSettings.Get("conString");

        internal static void CreateTable()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                var cmd = @"CREATE TABLE IF NOT EXISTS coding_sessions (
                                                        ID INTEGER PRIMARY KEY AUTOINCREMENT,
                                                        Start_Time TEXT,
                                                        End_Time TEXT,
                                                        Duration TEXT)";
                connection.Execute(cmd);
            }
        }

        internal static IEnumerable Query(string command)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                return connection.Query(command);
            }
        }
        internal static int NonQuery(string command)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                return connection.Execute(command);
            }
        }
    }
}
