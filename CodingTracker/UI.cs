using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodingTracker.Models;
using Spectre.Console;

namespace CodingTracker
{
    internal static class UI
    {
        public static CodingSession AskForSession()
        {
            string userStartTime;
            string userEndTime;
            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now;
            bool isStartTimeValid = false;
            bool isEndTimeValid = false;

            while (!isStartTimeValid)
            {
                userStartTime = AnsiConsole.Ask<string>("Enter the start time of the session: (HH:mm)");
                isStartTimeValid = DateTime.TryParseExact(userStartTime, "HH:mm", new CultureInfo("en-US"), DateTimeStyles.None, out startTime);
                if (!isStartTimeValid) AnsiConsole.Markup("[red]Invalid![/]\n");
            }
            while (!isEndTimeValid)
            {
                userEndTime = AnsiConsole.Ask<string>("Enter the end time of the session: (HH:mm)");
                isEndTimeValid = DateTime.TryParseExact(userEndTime, "HH:mm", new CultureInfo("en-US"), DateTimeStyles.None, out endTime) && (endTime > startTime);
                if (!isEndTimeValid) AnsiConsole.Markup("[red]Invalid![/]\n");
            }

            return new CodingSession(startTime, endTime);
            
        }

    }
}
