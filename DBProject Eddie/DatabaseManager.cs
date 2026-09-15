namespace DBProject_Eddie;

using MySqlConnector;

public class DatabaseManager
{
    //ConnectionString voor de CreateOrFill (zonder geselecteerde database)
    private readonly string connectionString =
        "Server=localhost;Port=3306;Uid=root;Pwd=1234;";

    //ConnectionString voor de rest (met geselecteerde database)
    private readonly string NewConnectionString =
        "Server=localhost;Port=3306;Database=Webwinkel;Uid=root;Pwd=1234;";

    public void CreateOrFillDatabase(bool create)
    {
        if (create)
        {
            Console.WriteLine("Database wordt aangemaakt...");

            string createSQL = File.ReadAllText("SQL/CreateDatabase.sql");

            // Maak een verbinding met de MySQL database
            using MySqlConnection connection = new MySqlConnection(connectionString);
            // Maak een SQL command aan met de SQL-query en de databaseverbinding
            using MySqlCommand command = new MySqlCommand(createSQL, connection);

            connection.Open();
            // Voer het voorheen aangemaakte SQL command uit
            command.ExecuteNonQuery();

            Console.WriteLine("Database succesvol aangemaakt!");
        }
        else
        {
            Console.WriteLine("Database wordt gevuld...");

            string fillSQL = File.ReadAllText("SQL/FillDatabase.sql");

            // Maak een verbinding met de MySQL database
            using MySqlConnection connection = new MySqlConnection(connectionString);
            // Maak een SQL command aan met de SQL-query en de databaseverbinding
            using MySqlCommand command = new MySqlCommand(fillSQL, connection);

            connection.Open();
            // Voer het voorheen aangemaakte SQL command uit
            command.ExecuteNonQuery();

            Console.WriteLine("Database succesvol gevuld!");
        }
    }

    public void AddCustomer()
    {
        try
        {
            // Controleer of de database bestaat en bereikbaar is
            using MySqlConnection connection = new MySqlConnection(NewConnectionString);

            connection.Open();

            Console.Clear();

            Console.WriteLine("☼ Klant Toevoegen ☼");

            Console.Write("Klantnaam: ");
            string? klantNaam = Console.ReadLine();

            Console.Write("Contactpersoon: ");
            string? contactPersoon = Console.ReadLine();

            Console.Write("Adres: ");
            string? adres = Console.ReadLine();

            Console.Write("Stad: ");
            string? stad = Console.ReadLine();

            Console.Write("Postcode: ");
            string? postcode = Console.ReadLine();

            Console.Write("Land: ");
            string? land = Console.ReadLine();

            string sql = """
                         INSERT INTO Klanten
                             (KlantNaam, ContactPersoon, Adres, Stad, Postcode, Land)
                         VALUES
                             (@KlantNaam, @ContactPersoon, @Adres, @Stad, @Postcode, @Land);
                         """;

            using MySqlCommand command = new MySqlCommand(sql, connection);

            command.Parameters.AddWithValue("@KlantNaam", klantNaam);
            command.Parameters.AddWithValue("@ContactPersoon", contactPersoon);
            command.Parameters.AddWithValue("@Adres", adres);
            command.Parameters.AddWithValue("@Stad", stad);
            command.Parameters.AddWithValue("@Postcode", postcode);
            command.Parameters.AddWithValue("@Land", land);

            command.ExecuteNonQuery();

            Console.WriteLine();
            Console.WriteLine("Klant succesvol toegevoegd!");
        }
        //Als de database nog neit is aangemaakt in Main Menu, wordt deze code uitgevoerd om een crash te voorkomen
        catch (MySqlException ex)
        {
            Console.WriteLine("☼ Klant Toevoegen ☼");
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

    public void DeleteCustomer()
    {
        try
        {
            // Controleer of de database bestaat en bereikbaar is
            using MySqlConnection connection = new MySqlConnection(NewConnectionString);

            connection.Open();

            Console.Clear();

            Console.WriteLine("☼ Klant Verwijderen ☼");
            Console.Write("Klant ID: ");

            int klantID = Convert.ToInt32(Console.ReadLine());

            string checkSql = """
                              SELECT KlantNaam
                              FROM Klanten
                              WHERE KlantID = @KlantID;
                              """;

            using MySqlCommand checkCommand = new MySqlCommand(checkSql, connection);

            checkCommand.Parameters.AddWithValue("@KlantID", klantID);
            
            object? result = checkCommand.ExecuteScalar();

            if (result == null)
            {
                Console.WriteLine("Geen klant gevonden met dit ID.");
                return;
            }

            // Voordat de klant wordt verwijderd, moeten we zeker weten of het de juiste is.
            string klantNaam = result.ToString()!;

            Console.WriteLine();
            Console.WriteLine($"Weet je zeker dat je '{klantNaam}' wil verwijderen?");
            Console.Write("(j/n): ");

            string? confirmation = Console.ReadLine();

            //Nee, dat wil ik niet...
            if (confirmation?.ToLower() != "j")
            {
                Console.WriteLine("Verwijderen geannuleerd.");
                return;
            }

            string sql = """
                         DELETE FROM Klanten
                         WHERE KlantID = @KlantID;
                         """;

            using MySqlCommand command = new MySqlCommand(sql, connection);

            command.Parameters.AddWithValue("@KlantID", klantID);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                Console.WriteLine("Klant succesvol verwijderd!");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Geen klant gevonden met dit ID.");
            }
        }
        //Als de database nog neit is aangemaakt in Main Menu, wordt deze code uitgevoerd om een crash te voorkomen
        catch (MySqlException ex)
        {
            Console.WriteLine("☼ Klant Toevoegen ☼");
            Console.WriteLine();

            //1049 is de errorcode voor "database not found"
            if (ex.Number == 1049)
            {
                Console.WriteLine("Er is geen database om data uit te verwijderen,");
                Console.WriteLine("maak eerst en vul daarna de database via het hoofdmenu.");
            }
            else
            {
                Console.WriteLine("Er kon geen verbinding worden gemaakt met de database.");
            }
        }
    }
}