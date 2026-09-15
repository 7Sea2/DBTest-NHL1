// !!!Important!!!
// DATABASE DATA: Server=localhost Port=3306 Uid=root Pwd=1234

using DBProject_Eddie;
using MySqlConnector;
// Encoding type voor ASCII ( ☼ )
Console.OutputEncoding = System.Text.Encoding.UTF8;

// Maak referenties naar de andere scripts in het project
DatabaseManager databaseManager = new DatabaseManager();
DatabaseViewer databaseViewer = new DatabaseViewer();
ASCIIViewer ASCIIViewer = new ASCIIViewer();

// Bool voor afsluiten
bool running = true;

// Main Menu
while (running)
{
    Console.Clear();

    Console.WriteLine("☼ Main Menu ☼");
    Console.WriteLine("1. Database Aanmaken");
    Console.WriteLine("2. Vul Database");
    Console.WriteLine("3. Database Viewer");
    Console.WriteLine("4. Klant Toevoegen");
    Console.WriteLine("5. Klant Verwijderen");
    Console.WriteLine("6. Grafiekmaker");
    Console.WriteLine("7. Exit");
    Console.Write("Keuze: ");

    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            Console.Clear();
            databaseManager.CreateOrFillDatabase(true);
            break;
        
        case "2":
            Console.Clear();
            databaseManager.CreateOrFillDatabase(false);
            break;

        case "3":
            Console.Clear();
            databaseViewer.ShowDatabase();
            break;

        case "4":
            Console.Clear();
            databaseManager.AddCustomer();
            break;

        case "5":
            Console.Clear();
            databaseManager.DeleteCustomer();
            break;

        case "6":
            Console.Clear();
            ASCIIViewer.ShowASCII();
            break;

        case "7":
            Console.Clear();
            running = false;
            Console.WriteLine("Programma wordt afgesloten.");
            break;

        default:
            Console.Clear();
            Console.WriteLine("Ongeldige keuze.");
            break;
    }

    if (running)
    {
        Console.WriteLine();
        Console.WriteLine("Druk op Enter om door te gaan.");
        Console.ReadLine();
    }
}