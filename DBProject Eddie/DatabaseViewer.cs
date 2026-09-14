namespace DBProject_Eddie;
using MySqlConnector;

public class DatabaseViewer
{
    private readonly string connectionString =
        "Server=localhost;Database=company;Port=3306;Uid=root1;Pwd=1234;";

    public void ShowDatabase()
    {
        string sqlRead = """
                         SELECT d.name, e.firstname
                         FROM company.department d
                         JOIN company.employees e 
                             ON d.idDepartment = e.idEmployee
                         """;

        using MySqlConnection connection = new MySqlConnection(connectionString);
        using MySqlCommand command = new MySqlCommand(sqlRead, connection);

        connection.Open();

        using MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            string department = reader.GetString("name");
            string firstname = reader.GetString("firstname");

            Console.WriteLine($"{department} - {firstname}");
        }
    }
}