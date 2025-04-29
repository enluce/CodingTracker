using CodingTracker;
using Spectre.Console;
using static CodingTracker.Enums;
public static class Program
{
    static bool exitApp = false;
    public static void Main(string[] args)
    {
        DatabaseManager.CreateTable();
        
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
                        SessionController.ViewSessions();
                        break;
                    case MenuOption.Add:
                        SessionController.AddSession();
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
                AnsiConsole.WriteLine($"Error encountered: {e}");
            }

        }
    }




}