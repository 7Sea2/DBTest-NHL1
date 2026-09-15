using MySqlConnector;
using ScottPlot;

namespace DBProject_Eddie;

public class ASCIIViewer
{
    private readonly string connectionString =
        "Server=localhost;Port=3306;Database=Webwinkel;Uid=root;Pwd=1234;";

    public void ShowASCII()
    {
        bool running = true;

        while (running)
        {
            Console.Clear();

            Console.WriteLine("☼ ASCII Viewer ☼");
            Console.WriteLine("1. Bestellingen per klant");
            Console.WriteLine("2. Terug");
            Console.Write("Keuze: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateOrderChart();
                    break;

                case "2":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Ongeldige keuze.");
                    Console.ReadLine();
                    break;
            }
        }
    }

    // Laat de chart van orders zien
    private void CreateOrderChart()
    {
        Console.Clear();

        Console.WriteLine("☼ Bestellingen Per Klant ☼");
        Console.WriteLine();


        // Haal per klant het aantal bestellingen op
        // COUNT() telt de bestellingen en GROUP BY groupeert de resultaten per klant
        string sql = """
                     SELECT
                         Klanten.KlantNaam,
                         COUNT(Bestellingen.BestellingID) AS AantalBestellingen
                     FROM Klanten
                     JOIN Bestellingen
                         ON Klanten.KlantID = Bestellingen.KlantID
                     GROUP BY Klanten.KlantID, Klanten.KlantNaam
                     ORDER BY AantalBestellingen DESC;
                     """;

        using MySqlConnection connection = new MySqlConnection(connectionString);
        using MySqlCommand command = new MySqlCommand(sql, connection);

        connection.Open();

        using MySqlDataReader reader = command.ExecuteReader();

        // Maak lijsten aan om de klantnamen en hoeveelheden van bestellingen op te slaan
        List<string> klantNamen = new List<string>();
        List<double> aantallen = new List<double>();

        // Lees de resultaten uit de database en voeg ze daarna toe aan de aangemaakte lijsten
        while (reader.Read())
        {
            klantNamen.Add(reader.GetString("KlantNaam"));
            aantallen.Add(reader.GetInt32("AantalBestellingen"));
        }

        // Controleer of er bestellingen gevonden zijn
        if (klantNamen.Count == 0)
        {
            Console.WriteLine("Er zijn geen bestellingen gevonden.");
            Console.ReadLine();
            return;
        }

        // Maak de grafiek met de NuGit package ScottPlot
        Plot plot = new Plot();

        // Voeg de hoeveelheden bestellingen toe als staven (bars) aan de grafiek (niet meer gebruikt)
        var bars = plot.Add.Bars(aantallen.ToArray());

        plot.Title("Aantal bestellingen per klant");
        plot.YLabel("Aantal bestellingen");
        plot.XLabel("Klantnummer");

        // Toon de klantnamen en aantallen ook in de console
        // Dit geeft een overzicht van de gegevens die de gebruiker in de grafiek stopt
        for (int i = 0; i < klantNamen.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {klantNamen[i]} - {aantallen[i]} bestellingen");
        }

        plot.SavePng("BestellingenPerKlant.png", 1000, 600);

        Console.WriteLine("Grafiek succesvol aangemaakt!");
        Console.WriteLine();
        Console.WriteLine("Bestand: BestellingenPerKlant.png in de BIN folder");
        Console.WriteLine();
        Console.WriteLine("Druk op Enter om terug te gaan.");
        Console.ReadLine();
    }
}