using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace KhumalCraftLTD.Models
{
    public class Product
    {
        // Connection string for the SQL Server database.
        public static string con_string = "Server=tcp:st10404539.database.windows.net,1433;Initial Catalog=CLOUD DEV DATABASE;Persist Security Info=False;User ID=NevilleKabamba;Password=0848423737Nk;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        // Properties of the Product class.
        public int ProductId { get; set; } // Unique identifier for the product.
        public string ProductName { get; set; } // Name of the product.
        public decimal ProductPrice { get; set; } // Price of the product.
        public string ProductImage { get; set; } // Image URL or path of the product.
        public int QuantityInCart { get; set; } // Quantity of the product in the shopping cart.

        // Method to retrieve all products from the database.
        public static List<Product> GetAllProducts()
        {
            // Initialize a list to hold the products.
            List<Product> products = new List<Product>();

            // SQL query to select all products.
            string sql = "SELECT * FROM Products";

            // Using block to ensure the connection is properly disposed.
            using (SqlConnection con = new SqlConnection(con_string))
            {
                SqlCommand cmd = new SqlCommand(sql, con); // Initialize the command with the query and connection.
                con.Open(); // Open the connection.
                SqlDataReader reader = cmd.ExecuteReader(); // Execute the query and get the data reader.

                // Read each product from the database and add it to the list.
                while (reader.Read())
                {
                    products.Add(new Product
                    {
                        ProductId = (int)reader["ProductId"], // Set ProductId.
                        ProductName = reader["ProductName"].ToString(), // Set ProductName.
                        ProductPrice = (decimal)reader["ProductPrice"], // Set ProductPrice.
                        ProductImage = reader["ProductImage"].ToString() // Set ProductImage.
                    });
                }
            }

            return products; // Return the list of products.
        }

        // Method to retrieve a product by its ID from the database.
        public static Product GetProductById(int productId)
        {
            Product product = null; // Initialize product as null.

            // SQL query to select the product by its ID.
            string sql = "SELECT * FROM Products WHERE ProductId = @ProductId";

            // Using block to ensure the connection is properly disposed.
            using (SqlConnection con = new SqlConnection(con_string))
            {
                SqlCommand cmd = new SqlCommand(sql, con); // Initialize the command with the query and connection.
                cmd.Parameters.AddWithValue("@ProductId", productId); // Adding parameter for ProductId.

                con.Open(); // Open the connection.
                SqlDataReader reader = cmd.ExecuteReader(); // Execute the query and get the data reader.

                // Read the product data if available.
                if (reader.Read())
                {
                    product = new Product
                    {
                        ProductId = (int)reader["ProductId"], // Set ProductId.
                        ProductName = reader["ProductName"].ToString(), // Set ProductName.
                        ProductPrice = (decimal)reader["ProductPrice"], // Set ProductPrice.
                        ProductImage = reader["ProductImage"].ToString() // Set ProductImage.
                    };
                }
            }

            return product; // Return the product.
        }
    }
}

