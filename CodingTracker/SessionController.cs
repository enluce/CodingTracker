using System.Globalization;
using CodingTracker.Models;
using Spectre.Console;

namespace CodingTracker
{ 
    internal static class SessionController
    {

        internal static void ViewSessions()
        {
     
            List<CodingSession> sessions = DatabaseManager.Query("SELECT * FROM coding_sessions;");

            UI.TableVisualizer(sessions);

        }
        internal static void AddSession()
        {

            CodingSession session = UI.AskForSession();

            var command = $"INSERT INTO coding_sessions (date, startTime, endTime, duration) VALUES ('{session.Date}', '{session.StartTime}', '{session.EndTime}', '{session.Duration}')";
            DatabaseManager.NonQuery(command);

            UI.Message("Session added successfully!", Enums.MessageOutcome.Positive);


        }
        internal static void DeleteSession()
        {
            if (DatabaseManager.ContainsRow())
            {
                UI.Message("No sessions available!", Enums.MessageOutcome.Negative);
                return;
            }
            ViewSessions();

            int sessionID = UI.AskForId();

            string command = "DELETE from coding_sessions where ID = @ID";

            DatabaseManager.NonQuery(command, new { ID = sessionID });

            UI.Message("Session deleted successfully!", Enums.MessageOutcome.Positive);

        }

        internal static void UpdateSession()
        {
            if (DatabaseManager.ContainsRow())
            {
                UI.Message("No sessions available!", Enums.MessageOutcome.Negative);
                return;
            }

            ViewSessions();

            DateOnly dateDateOnly = DateOnly.MaxValue;
            int sessionID = UI.AskForId();
            string command = "";
            bool exitLoop = false;

            
            while (!exitLoop)
            {
                string menuSelection = AnsiConsole.Prompt(new SelectionPrompt<string>()
          .Title("[green]Select[/] what you want to do.")
          .AddChoices(["Date", "StartTime", "EndTime"]));

                switch (menuSelection)
                {
                    case "Date":
                        string date = AnsiConsole.Ask<string>("Enter the date: (d.M.yyyy)");
                        if (!DateOnly.TryParseExact(date, "d.M.yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out dateDateOnly))
                        {
                            AnsiConsole.Markup("[red]Invalid Date[/]");
                            Console.ReadLine();

                        }
                        else
                        {
                            command = $"UPDATE coding_sessions SET date = '{dateDateOnly.ToString("D")}' where ID = {sessionID}";
                            exitLoop = true;
                        }
                        break;
                    case "StartTime":
                        string startTime = AnsiConsole.Ask<string>("Enter the start time: (HH:mm)");
                        if (!DateTime.TryParseExact(startTime, "HH:mm", new CultureInfo("en-US"), DateTimeStyles.None, out _))
                        {
                            AnsiConsole.Markup("[red]Invalid Time[/]");
                            Console.ReadLine();
                        }
                        else
                        {
                            if (Validation.UpdateValidation(startTime, sessionID, Enums.TimeType.Start))
                            {
                                command = $"UPDATE coding_sessions SET startTime = '{TimeOnly.Parse(startTime).ToLongTimeString()}' where ID = {sessionID}";
                                exitLoop = true;
                            }
                            else
                            {
                                AnsiConsole.Markup("[red]Start time can't be greater than or equal to end time![/]");
                                Console.ReadLine();

                            }
                        }
                        break;
                    case "EndTime":
                        string endTime = AnsiConsole.Ask<string>("Enter the end time: (HH:mm");
                        if (!DateTime.TryParseExact(endTime, "HH:mm", new CultureInfo("en-US"), DateTimeStyles.None, out _))
                        {
                            AnsiConsole.Markup("[red]Invalid Time[/]");
                            Console.ReadLine();

                        }
                        else
                        {
                            if (Validation.UpdateValidation(endTime, sessionID, Enums.TimeType.End))
                            {
                                command = $"UPDATE coding_sessions SET endTime = '{TimeOnly.Parse(endTime).ToLongTimeString()}' where ID = {sessionID}";
                                exitLoop = true;
                            }
                            else
                            {
                                AnsiConsole.Markup("[red]End time can't be less than or equal to start time![/]");
                                Console.ReadLine();
                            }
                        }

                        break;
                }

            }

            DatabaseManager.NonQuery(command);
         
            AnsiConsole.Write("Session updated sucessfully!");
            Console.ReadLine();
        }

    }
}
