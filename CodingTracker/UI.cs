using System.Globalization;
using CodingTracker.Models;
using Spectre.Console;

namespace CodingTracker
{
    internal static class UI
    {
        public static CodingSession AskForSession()
        {
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
        public static int AskForId()
        {
            int id = -1;
            while (true)
            {
                id = AnsiConsole.Ask<int>("Enter the ID of the session you want to update: ");

                if (Validation.IDValidation(id))
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
