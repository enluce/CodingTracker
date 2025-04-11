using Spectre.Console;
using System.Configuration;
using System.Collections.Specialized;
using static CodingTracker.Enums;
using Dapper;
using Microsoft.Data.Sqlite;
using System.Globalization;
using CodingTracker.Models;
using CodingTracker;

public static class Program
{
    static string connectionString = ConfigurationManager.AppSettings.Get("conString");
    static bool exitApp = false;
    public static void Main(string[] args)
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

        AnsiConsole.Markup("Hello and welcome to the Coding Tracker!\n\n");
        Console.ReadLine();
        while (!exitApp)
        {
            try
            {
                MenuOption menuSelection = AnsiConsole.Prompt(new SelectionPrompt<MenuOption>()
            .Title("[green]Select[/] what you want to do.")
            .AddChoices(Enum.GetValues<MenuOption>()));


                switch (menuSelection)
                {
                    case MenuOption.View:
                        ViewSessions();
                        break;
                    case MenuOption.Add:
                        AddSession();
                        break;
                    case MenuOption.Delete:
                        //DeleteSession();
                        break;
                    case MenuOption.Update:
                        //UpdateSession();
                        break;
                    case MenuOption.Exit:
                        exitApp = true;
                        Console.Clear();
                        break;
                }
            }
            catch (Exception e)
            {
                Console.Clear();
                AnsiConsole.WriteLine($"Error encountered: {e.Message}");
            }
            
        }
    }

    private static void ViewSessions()
    {
        Console.Clear();
        throw new NotImplementedException();
    }

    private static void AddSession()
    {

        Console.Clear();
        CodingSession session = Helpers.AskForSession();

        var command = $"INSERT INTO coding_sessions (Start_Time, End_Time, Duration) VALUES ('{session.startTime}', '{session.endTime}', '{session.duration}')";

        using (var connection = new SqliteConnection(connectionString))
        {
            int recordsAffected = connection.Execute(command);

            if (recordsAffected == 0)
            {
                AnsiConsole.Markup("[red] Couldn't add session.");
            }
            else AnsiConsole.Markup("Session added successfully!");
            Console.ReadLine();
            Console.Clear();

        }

    }
}