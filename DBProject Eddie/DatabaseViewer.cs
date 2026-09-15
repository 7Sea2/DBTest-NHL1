using MySqlConnector;
namespace DBProject_Eddie;

public class DatabaseViewer
{
    private readonly string connectionString =
        "Server=localhost;Port=3306;Database=Webwinkel;Uid=root;Pwd=1234;";

    public void ShowDatabase()
    {
        try
        {
            // Controleer of de database bestaat en bereikbaar is
            using MySqlConnection connection = new MySqlConnection(connectionString);

            connection.Open();

            bool running = true;

            while (running)
            {
                Console.Clear();

                Console.WriteLine("☼ Database Viewer ☼");
                Console.WriteLine("1. Klanten & Producten");
                Console.WriteLine("2. Alle Producten");
                Console.WriteLine("3. Alle Klanten");
                Console.WriteLine("4. Bestellingen");
                Console.WriteLine("5. Categorieën");
                Console.WriteLine("6. Leveranciers");
                Console.WriteLine("7. Medewerkers");
                Console.WriteLine("8. Verzenddiensten");
                Console.WriteLine("9. Terug");
                Console.Write("Keuze: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowCustomersAndProducts();
                        break;

                    case "2":
                        Console.Clear();
                        Console.WriteLine("Alle producten");
                        Console.WriteLine("Not Implemented");
                        break;

                    case "3":
                        Console.Clear();
                        Console.WriteLine("Alle klanten");
                        Console.WriteLine("Not Implemented");
                        break;

                    case "4":
                        Console.Clear();
                        Console.WriteLine("Alle bestellingen");
                        Console.WriteLine("Not Implemented");
                        break;

                    case "5":
                        Console.Clear();
                        Console.WriteLine("Alle categorieën");
                        Console.WriteLine("Not Implemented");
                        break;

                    case "6":
                        Console.Clear();
                        Console.WriteLine("Alle leveranciers");
                        Console.WriteLine("Not Implemented");
                        break;

                    case "7":
                        Console.Clear();
                        Console.WriteLine("Alle medewerkers");
                        Console.WriteLine("Not Implemented");
                        break;

                    case "8":
                        Console.Clear();
                        Console.WriteLine("Alle verzenddiensten");
                        Console.WriteLine("Not Implemented");
                        break;

                    case "9":
                        Console.Clear();
                        running = false;
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
        }
        
        //Als de database nog neit is aangemaakt in Main Menu, wordt deze code uitgevoerd om een crash te voorkomen
        catch (MySqlException ex)
        {
            Console.WriteLine("☼ Database Viewer ☼");
            Console.WriteLine();

            //1049 is de errorcode voor "database not found"
            if (ex.Number == 1049)
            {
                Console.WriteLine("De database is nog niet aangemaakt,");
                Console.WriteLine("maak eerst de database aan via het hoofdmenu.");
            }
            else
            {
                Console.WriteLine("Er kon geen verbinding worden gemaakt met de database.");
            }
        }
    }


    // -- EERSTE OPTIE: Klanten en Producten
    private void ShowCustomersAndProducts()
    {
        Console.Clear();

        Console.WriteLine("☼ Klanten & Producten ☼");
        Console.WriteLine();

        string sql = """
            SELECT
                Klanten.KlantNaam,
                Bestellingen.BestellingID,
                Producten.ProductNaam,
                Bestelregels.Aantal
            FROM Klanten
            JOIN Bestellingen
                ON Klanten.KlantID = Bestellingen.KlantID
            JOIN Bestelregels
                ON Bestellingen.BestellingID = Bestelregels.BestellingID
            JOIN Producten
                ON Bestelregels.ProductID = Producten.ProductID
            ORDER BY Klanten.KlantNaam;
            """;

        using MySqlConnection connection = new MySqlConnection(connectionString);
        using MySqlCommand command = new MySqlCommand(sql, connection);

        connection.Open();

        using MySqlDataReader reader = command.ExecuteReader();

        // Laat de header bovenin het scherm zien
        Console.WriteLine(
            $"{"Klant",-35} {"Bestelling",-15} {"Product",-30} {"Aantal",-10}");

        Console.WriteLine(new string('-', 120));

        while (reader.Read())
        {
            string klantNaam = reader.GetString("KlantNaam");
            int bestellingID = reader.GetInt32("BestellingID");
            string productNaam = reader.GetString("ProductNaam");
            int aantal = reader.GetInt32("Aantal");

            // Laat de data in mooie kolommen zien
            Console.WriteLine(
                $"{klantNaam,-35} {bestellingID,-15} {productNaam,-30} {aantal,-10}");
        }
    }
}