using System.Globalization;
using CodingTracker.Models;
using Spectre.Console;

namespace CodingTracker
{
    internal static class UI
    {
        public static CodingSession AskForSession()
        {
            Console.Clear();
            string userStartTime = "";
            string userEndTime = "";
            bool isStartTimeValid = false;
            bool isEndTimeValid = false;
            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now;

            while (!isStartTimeValid)
            {
                userStartTime = AnsiConsole.Ask<string>("Enter the start time of the session: (HH:mm)");
                isStartTimeValid = DateTime.TryParseExact(userStartTime, "HH:mm", new CultureInfo("en-US"), DateTimeStyles.None, out startTime);
                if (!isStartTimeValid) AnsiConsole.Markup("[red]Invalid![/]\n");
            }
            while (!isEndTimeValid)
            {
                userEndTime = AnsiConsole.Ask<string>("Enter the end time of the session: (HH:mm)");
                isEndTimeValid = DateTime.TryParseExact(userEndTime, "HH:mm", new CultureInfo("en-US"), DateTimeStyles.None, out endTime)
                    && (DateTime.Parse(userEndTime) > DateTime.Parse(userStartTime));
                if (!isEndTimeValid) AnsiConsole.Markup("[red]Invalid![/]\n");
            }

            return new CodingSession(startTime.ToString(), userEndTime.ToString());

        }

        public static void TableVisualizer(List<CodingSession> sessions)
        {
            Console.Clear();
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

        public static void Message(string message, Enums.MessageOutcome messageOutcome)
        {
            Dictionary<Enums.MessageOutcome, string> dict = new()
            {
                {Enums.MessageOutcome.Positive, "green" },
                {Enums.MessageOutcome.Negative, "red" }
            };

            AnsiConsole.Markup($"[{dict[messageOutcome]}]{message}[/]");

            Console.ReadLine();
            Console.Clear();
        }
        public static int AskForId()
        {
            int id = -1;
            while (true)
            {
                id = AnsiConsole.Ask<int>("Enter the ID of the session: ");

                if (!Validation.IDValidation(id))
                {
                    AnsiConsole.Markup("[red]Invalid ID![/]");
                    Console.ReadLine();
                }
                else break;
            }

            return id;

        }

    }

}
