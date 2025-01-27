using Spectre.Console;
using DapperPractice.Data.DataModels;
using DapperPractice.Data;
using System.Security.Cryptography.X509Certificates;

namespace DapperPractice.UserInterface 
{
    class MainMenu
    {
        private readonly String logo = @"
_____                                       
|  _ \  __ _ _ __  _ __   ___ _ __           
| | | |/ _` | '_ \| '_ \ / _ \ '__|          
| |_| | (_| | |_) | |_) |  __/ |             
|____/ \__,_| .__/| .__/ \___|_|             
        ____|_|   |_|         _   _           
        |  _ \ _ __ __ _  ___| |_(_) ___ ___  
        | |_) | '__/ _` |/ __| __| |/ __/ _ \ 
        |  __/| | | (_| | (__| |_| | (_|  __/ 
        |_|   |_|  \__,_|\___|\__|_|\___\___| 
                                              
        I'm trying my best, alright?
            By Gage Schaffer
        ";

        private readonly String SeparatorLine = "========================================================";

        public void PrintLogo()
        {
            AnsiConsole.MarkupLine($"[bold yellow]{logo}[/]");
            AnsiConsole.MarkupLine($"[bold red]{SeparatorLine}[/]");
        }

        public String GetUserOption()
        {
            AnsiConsole.MarkupLine("[bold yellow]Dapper Practice App [/]");
            var userOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("\n[green]Please Select An Option:[/] ")
                .AddChoices( new [] {"Create", "Search By Language", "Update", "Delete", "Exit"})
            );
            return userOption;
        }

        public (String language, String notes, String startDate, String endDate) HandleCreationSubMenu() 
        {
            AnsiConsole.MarkupLine("[green] Create a new Time Entry:[/] ");
            var language =  AnsiConsole.Prompt( new TextPrompt<String>(">                 [underline]Language:[/] "));
            var notes =     AnsiConsole.Prompt( new TextPrompt<String>(">                    [underline]Notes:[/] "));
            var startDate = AnsiConsole.Prompt( new TextPrompt<String>("> [underline]Start (yyyy-MM-DD HH:SS):[/] "));
            var endDate =   AnsiConsole.Prompt( new TextPrompt<String>(">   [underline]End (yyyy-MM-DD HH:SS):[/] "));
            Console.Clear(); 
            return (language, notes, startDate, endDate);
        }

        public String HandleSearchByLanguageSubMenu()
        {
            return AnsiConsole.Prompt( new TextPrompt<String>("[underline]> Language:[/] "));
        }

        public void PrintResults(List<TimeEntry> entries)
        {
            AnsiConsole.MarkupLine("");
            AnsiConsole.MarkupLine("[red]Search Results [/]");
            var table = new Table();
            table.Border(TableBorder.Rounded);
            table.Expand();
            table.AddColumn("Language");
            table.AddColumn("Notes");
            table.AddColumn("Start Time");
            table.AddColumn("End Time");
            table.AddColumn("Total Duration");

            int sumDuration = 0;
            foreach (TimeEntry entry in entries)
            {
                table.AddRow(entry.Language, entry.Notes, entry.StartTime.ToString(), entry.EndTime.ToString(), entry.Duration.ToString());
                if (entry.Duration != null) {
                    sumDuration += (int) entry.Duration;
                }
            }
            AnsiConsole.Write(table);

            AnsiConsole.MarkupLine("");
            AnsiConsole.MarkupLine("[red]Lookup Summary[/]");
            var summaryTable = new Table();
            summaryTable.Border(TableBorder.Minimal);
            summaryTable.AddColumn("Total Records");
            summaryTable.AddColumn("Sum of Duration of Queried Records");
            summaryTable.AddRow(entries.Count.ToString(), sumDuration.ToString());
            AnsiConsole.Write(summaryTable); 
            AnsiConsole.WriteLine("Press Any Key to Continue...");
            Console.ReadLine();
            Console.Clear();
        }



    }
}