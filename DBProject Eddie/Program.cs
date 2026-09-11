using MySqlConnector;

Console.WriteLine("Hello, World!");


string connectionString = "Server=localhost;Database=company;Port=3306;Uid=root;Pwd=1234;"; 
using MySqlConnection connection = new MySqlConnection(connectionString);

string sqlRead = """
                 SELECT d.name, e.firstname
                 FROM company.department d
                          JOIN company.employees e ON d.idDepartment = e.idEmployee
                 """;

using MySqlCommand command = new MySqlCommand(sqlRead, connection);
{
    connection.Open();
    using MySqlDataReader reader = command.ExecuteReader();
    {

        while (reader.Read())
        {
            string name = reader.GetString(name: "name");
            string firstname = reader.GetString(name: "firstname");
            Console.WriteLine($"{name} - {firstname}");
        }
    }
}

connection.Close();