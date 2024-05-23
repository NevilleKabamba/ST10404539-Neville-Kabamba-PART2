using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace KhumalCraftLTD.Models
{
    // The Users class represents a user in the system and provides methods to interact with the user data in the database.
    public class Users
    {
        // Private field to hold the database connection string.
        private readonly string _connectionString;

        // Parameterless constructor.
        public Users()
        {
        }

        // Constructor that initializes the connection string using dependency injection.
        public Users(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // Properties of the Users class.
        public string Name { get; set; } // User's first name.
        public string Surname { get; set; } // User's surname.
        public string Email { get; set; } // User's email address.

        // Method to insert a new user into the database.
        public int InsertUser(Users user)
        {
            // Using block to ensure the SQL connection is properly disposed.
            using (var con = new SqlConnection(_connectionString))
            {
                // SQL query to insert a new user.
                string sql = "INSERT INTO Users (UserName, UserSurname, UserEmail) VALUES (@UserName, @UserSurname, @UserEmail)";

                // Using block to ensure the SQL command is properly disposed.
                using (var cmd = new SqlCommand(sql, con))
                {
                    // Adding parameters to the SQL command.
                    cmd.Parameters.AddWithValue("@UserName", user.Name); // Add parameter for UserName.
                    cmd.Parameters.AddWithValue("@UserSurname", user.Surname); // Add parameter for UserSurname.
                    cmd.Parameters.AddWithValue("@UserEmail", user.Email); // Add parameter for UserEmail.

                    con.Open(); // Open the SQL connection.
                    int rowsAffected = cmd.ExecuteNonQuery(); // Execute the command and get the number of affected rows.
                    return rowsAffected; // Return the number of rows affected.
                }
            }
        }

        // Method to get a user by their ID (not yet implemented).
        internal object GetUserById(int userId)
        {
            throw new NotImplementedException(); // Placeholder for the method implementation.
        }
    }
}

