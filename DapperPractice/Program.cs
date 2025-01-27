using DapperPractice.UserInterface;
using DapperPractice.Data;
using DapperPractice.Data.DataModels;

namespace DapperPractice 
{

    class Program 
    {

        static void Main(string[] args) 
        {
            MainMenu ui = new MainMenu();
            Context db = new Context();
            db.Initialize();
            ui.PrintLogo();

            Boolean programActive = true;
             while (programActive) 
            {
                String option = ui.GetUserOption();
                switch (option.ToLower()) 
                {
                    case "create":
                        (String language, String notes, String startTime, String endtime) = ui.HandleCreationSubMenu();
                        db.InsertEntry(new TimeEntry(language, notes, startTime, endtime));
                        break;
                    case "search by language":
                        List<TimeEntry> results = db.FindEntryByLanguage(ui.HandleSearchByLanguageSubMenu());
                        ui.PrintResults(results);
                        break;
                    case "exit":
                        return;
                }
            }

        }
    }
}
