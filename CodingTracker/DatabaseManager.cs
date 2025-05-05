using System.Collections;
using System.Configuration;
using CodingTracker.Models;
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
                                                        date TEXT,
                                                        startTime TEXT,
                                                        endTime TEXT,
                                                        duration TEXT)";
                connection.Execute(cmd);
            }
        }

        internal static List<CodingSession> Query(string command)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                List<CodingSession> results = connection.Query<CodingSession>(command).ToList();

                return results;
               
                //Console.WriteLine(results.Start_Time);
            }
        }
        internal static int NonQuery(string command, object parameters = null)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                return connection.Execute(command, parameters);
            }
        }
        internal static string Scalar(string command, object parameters = null)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                return connection.ExecuteScalar<string>(command, parameters);
            }
        }

        internal static bool ContainsRow()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                int rows = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM coding_sessions");

                return (rows == 0) ? true : false; 
            }
        }
    }
}
