using CodingTracker.Models;
using Spectre.Console;

namespace CodingTracker
{
    internal static class SessionController
    {

        internal static void ViewSessions()
        {
            Console.Clear();
            
            List<CodingSession> sessions = DatabaseManager.Query("SELECT * FROM coding_sessions;");

            Table table = new Table();

            table.AddColumn("ID");
            table.AddColumn("Date");
            table.AddColumn("Start_Time");
            table.AddColumn("End_Time");
            table.AddColumn("Duration");
            //table.AddColumn(new TableColumn("Bar").Centered());

            foreach (CodingSession session in sessions)
            {
                table.AddRow(session.Id.ToString(), session.Date, session.StartTime, session.EndTime, session.Duration);
            }

            AnsiConsole.Write(table);


        }
        internal static void AddSession()
        {

            Console.Clear();
            CodingSession session = UI.AskForSession();

            var command = $"INSERT INTO coding_sessions (date, startTime, endTime, duration) VALUES ('{session.Date}', '{session.StartTime}', '{session.EndTime}', '{session.Duration}')";
            DatabaseManager.NonQuery(command);

            AnsiConsole.Markup("Session added successfully!");
            Console.ReadLine();
            Console.Clear();

        }
    }
}
