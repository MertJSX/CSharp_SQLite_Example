using System;
using Microsoft.Data.Sqlite;

namespace SQLiteTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create or open existing database file
            SqliteConnection connection = new SqliteConnection("Data Source=database.db"); // name of file
            connection.Open();

            // Create table users for example
            SqliteCommand createTableCommand = connection.CreateCommand();
            createTableCommand.CommandText = 
            @"CREATE TABLE IF NOT EXISTS users (
            id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
            username TEXT,
            email TEXT,
            password TEXT);";
            createTableCommand.ExecuteNonQuery();

            // Inserting new user to the table users
            SqliteCommand insertCommand = connection.CreateCommand();
            insertCommand.CommandText = 
            @"INSERT INTO users(username, email, password)
            VALUES($uname, $email, $pass);";

            Console.Write("Please enter name to insert: ");
            insertCommand.Parameters.AddWithValue("$uname", Console.ReadLine());

            Console.Write("Please enter email to insert: ");
            insertCommand.Parameters.AddWithValue("$email", Console.ReadLine());

            Console.Write("Please enter password to insert: ");
            insertCommand.Parameters.AddWithValue("$pass", Console.ReadLine());

            insertCommand.ExecuteNonQuery();

            // Example select query
            SqliteCommand selectCommand = connection.CreateCommand();
            selectCommand.CommandText = 
            @"SELECT username, email, password 
            FROM users 
            WHERE username = @name";

            Console.Write("Please enter name to search: ");
            selectCommand.Parameters.AddWithValue("@name", Console.ReadLine());

            SqliteDataReader reader = selectCommand.ExecuteReader();

            while (reader.Read())
            {
                string userName = reader.GetString(0);
                string userEmail = reader.GetString(1);
                string userPassword = reader.GetString(2);

                Console.WriteLine("Hello, " + userName + "!");
                Console.WriteLine("Your email is " + userEmail);
                Console.WriteLine("Your password is " + userPassword);
            }

            reader.Close();

            // Example update
            SqliteCommand updateCommand = connection.CreateCommand();
            updateCommand.CommandText = 
            @"UPDATE users SET email = @email 
            WHERE username = @username";

            Console.Write("Enter name to change email: ");
            updateCommand.Parameters.AddWithValue("@username", Console.ReadLine());

            Console.Write("Enter new email: ");
            updateCommand.Parameters.AddWithValue("@email", Console.ReadLine());
            updateCommand.ExecuteNonQuery();

            // Close connection
            connection.Close();
        }
    }
}
