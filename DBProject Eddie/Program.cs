using DBProject_Eddie;
using MySqlConnector;

// Connect aan de andere scripts in-file
DatabaseManager databaseManager = new DatabaseManager();
DatabaseViewer databaseViewer = new DatabaseViewer();

// Bool voor afsluiten
bool running = true;

// Main Menu
while (running)
{
    Console.Clear();

    Console.WriteLine("--Main Menu");
    Console.WriteLine("1. Database Aanmaken");
    Console.WriteLine("1. Vul Database");
    Console.WriteLine("3. Database Viewer");
    Console.WriteLine("4. Entry Aanpassen");
    Console.WriteLine("5. Entry Verwijderen");
    Console.WriteLine("6. ASCII");
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
            Console.WriteLine("Not Implemented");
            break;

        case "5":
            Console.Clear();
            Console.WriteLine("Not Implemented");
            break;

        case "6":
            Console.Clear();
            Console.WriteLine("Not Implemented");
            break;

        case "7":
            Console.Clear();
            running = false;
            Console.WriteLine("Programma wordt afgesloten.");
            break;

        default:
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