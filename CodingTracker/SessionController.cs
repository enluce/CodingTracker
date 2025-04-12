using CodingTracker.Models;
using Spectre.Console;

namespace CodingTracker
{
    internal static class SessionController
    {

        internal static void ViewSessions()
        {
            Console.Clear();
            throw new NotImplementedException();
        }
        internal static void AddSession()
        {

            Console.Clear();
            CodingSession session = UI.AskForSession();

            var command = $"INSERT INTO coding_sessions (Start_Time, End_Time, Duration) VALUES ('{session.startTime}', '{session.endTime}', '{session.duration}')";
            DatabaseManager.NonQuery(command);

            AnsiConsole.Markup("Session added successfully!");
            Console.ReadLine();
            Console.Clear();

        }
    }
}
