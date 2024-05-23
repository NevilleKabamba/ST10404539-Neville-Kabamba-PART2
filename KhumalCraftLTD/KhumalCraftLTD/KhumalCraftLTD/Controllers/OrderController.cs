using Microsoft.AspNetCore.Mvc;
using KhumalCraftLTD.Models; 
using System.Linq; 
using System.Collections.Generic; 

namespace KhumalCraftLTD.Controllers
{
    
    public class OrderController : Controller
    {
        // Sample list of orders initialized for demonstration purposes.
        private readonly List<Order> orders = new List<Order>
        {
            new Order
            {
                OrderId = 0, // Unique identifier for the order.
                OrderDate = DateTime.Now, // Date and time the order was placed.
                OrderDetails = new List<OrderDetail> // Details of the order, including products and quantities.
                {
                    new OrderDetail
                    {
                        OrderDetailId = 1, // Unique identifier for the order detail.
                        Product = new Product { ProductId = 1, ProductName = "Product 1", ProductPrice = 10.0m }, // Product details.
                        Quantity = 2 // Quantity of the product in the order.
                    },
                    new OrderDetail
                    {
                        OrderDetailId = 2, // Unique identifier for the order detail.
                        Product = new Product { ProductId = 2, ProductName = "Product 2", ProductPrice = 15.5m }, // Product details.
                        Quantity = 1 // Quantity of the product in the order.
                    }
                }
            }
        };

        // Action method to handle order confirmation requests by order ID.
        public IActionResult OrderConfirmation(int orderId)
        {
            // Find the order with the specified order ID.
            var order = orders.FirstOrDefault(o => o.OrderId == orderId);

            // If the order is not found, return a 404 Not Found response.
            if (order == null)
            {
                return NotFound();
            }

            // If the order is found, return the order confirmation view with the order details.
            return View(order);
        }
    }
}

