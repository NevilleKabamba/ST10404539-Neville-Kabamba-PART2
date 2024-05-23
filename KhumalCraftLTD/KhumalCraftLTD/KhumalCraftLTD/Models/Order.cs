using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace KhumalCraftLTD.Models
{
    public class Order
    {
        // Connection string for the SQL Server database.
        public static string con_string = "Server=tcp:st10404539.database.windows.net,1433;Initial Catalog=CLOUD DEV DATABASE;Persist Security Info=False;User ID=NevilleKabamba;Password=0848423737Nk;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        // SQL connection using the connection string.
        public static SqlConnection con = new SqlConnection(con_string);

        // Properties of the Order class.
        public int OrderId { get; set; } // Unique identifier for the order.
        public int UserId { get; set; } // Unique identifier for the user who placed the order.
        public DateTime OrderDate { get; set; } // Date and time the order was placed.
        public List<OrderDetail> OrderDetails { get; set; } // List of order details associated with the order.

        // Method to place an order in the database.
        public static int PlaceOrder(int UserId, List<OrderDetail> orderDetails)
        {
            // SQL query to insert a new order and return the generated OrderId.
            string orderSql = "INSERT INTO Orders (UserId, OrderDate) OUTPUT INSERTED.OrderId VALUES (@UserId, @OrderDate)";
            SqlCommand orderCmd = new SqlCommand(orderSql, con);
            orderCmd.Parameters.AddWithValue("@UserId", UserId); // Adding parameter for UserId.
            orderCmd.Parameters.AddWithValue("@OrderDate", DateTime.Now); // Adding parameter for OrderDate.

            // Open the connection, execute the query, and close the connection.
            con.Open();
            int orderId = (int)orderCmd.ExecuteScalar(); // Execute the query and get the new OrderId.
            con.Close();

            // Loop through each order detail and insert it into the database.
            foreach (var detail in orderDetails)
            {
                // SQL query to insert order details.
                string detailSql = "INSERT INTO OrderDetails (OrderId, ProductId, Quantity, Product) VALUES (@OrderId, @ProductId, @Quantity)";
                SqlCommand detailCmd = new SqlCommand(detailSql, con);
                detailCmd.Parameters.AddWithValue("@OrderId", orderId); // Adding parameter for OrderId.
                detailCmd.Parameters.AddWithValue("@ProductId", detail.ProductId); // Adding parameter for ProductId.
                detailCmd.Parameters.AddWithValue("@Quantity", detail.Quantity); // Adding parameter for Quantity.

                // Open the connection, execute the query, and close the connection.
                con.Open();
                detailCmd.ExecuteNonQuery();
                con.Close();
            }

            return orderId; // Return the new OrderId.
        }

        // Method to retrieve an order by its ID from the database.
        public static Order GetOrderById(int orderId)
        {
            Order order = null; // Initialize order as null.

            // SQL query to select the order by its ID.
            string sql = "SELECT * FROM Orders WHERE OrderId = @OrderId";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@OrderId", orderId); // Adding parameter for OrderId.

            con.Open(); // Open the connection.
            SqlDataReader reader = cmd.ExecuteReader(); // Execute the query and get the reader.

            // Read the order data if available.
            if (reader.Read())
            {
                order = new Order
                {
                    OrderId = (int)reader["OrderId"], // Set OrderId.
                    UserId = (int)reader["UserId"], // Set UserId.
                    OrderDate = (DateTime)reader["OrderDate"], // Set OrderDate.
                    OrderDetails = new List<OrderDetail>() // Initialize the list of OrderDetails.
                };
            }
            reader.Close(); // Close the reader.

            // If the order was found, retrieve its details.
            if (order != null)
            {
                // SQL query to select the order details and associated product information.
                string detailsSql = "SELECT od.*, p.ProductName, p.ProductPrice FROM OrderDetails od JOIN Products p ON od.ProductId = p.ProductId WHERE od.OrderId = @OrderId";
                SqlCommand detailsCmd = new SqlCommand(detailsSql, con);
                detailsCmd.Parameters.AddWithValue("@OrderId", orderId); // Adding parameter for OrderId.
                SqlDataReader detailsReader = detailsCmd.ExecuteReader(); // Execute the query and get the reader.

                // Read the order details data if available.
                while (detailsReader.Read())
                {
                    order.OrderDetails.Add(new OrderDetail
                    {
                        OrderDetailId = (int)detailsReader["OrderDetailId"], // Set OrderDetailId.
                        OrderId = (int)detailsReader["OrderId"], // Set OrderId.
                        ProductId = (int)detailsReader["ProductId"], // Set ProductId.
                        Quantity = (int)detailsReader["Quantity"], // Set Quantity.
                        Product = new Product
                        {
                            ProductId = (int)detailsReader["ProductId"], // Set ProductId.
                            ProductName = detailsReader["ProductName"].ToString(), // Set ProductName.
                            ProductPrice = (decimal)detailsReader["ProductPrice"] // Set ProductPrice.
                        }
                    });
                }
                detailsReader.Close(); // Close the reader.
            }

            con.Close(); // Close the connection.
            return order; // Return the order.
        }
    }
}


