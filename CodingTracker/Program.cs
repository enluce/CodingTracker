using Spectre.Console;
using System.Configuration;
using System.Collections.Specialized;
using static CodingTracker.Enums;

public static class Program
{
    public static void Main(string[] args)
    {

        AnsiConsole.Markup("Hello and welcome to the Coding Tracker!\n\n");

        MenuOption menuSelection = AnsiConsole.Prompt(new SelectionPrompt<MenuOption>()
            .Title("[green]Select[/] what you want to do.")
            .AddChoices(Enum.GetValues<MenuOption>()));


        switch (menuSelection)
        {
            case MenuOption.View:
                //ViewSessions();
                break;
            case MenuOption.Add:
                //AddSession();
                break;
            case MenuOption.Delete:
                //DeleteSession();
                break;
            case MenuOption.Update:
                //UpdateSession();
                break;
        }
    }
}