using System.Collections.Generic;
using System.Linq;

namespace KhumalCraftLTD.Models
{
    // The ShoppingCart class represents a user's shopping cart.
    public class ShoppingCart
    {
        // List to store the items in the cart.
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        // Method to add an item to the shopping cart.
        public void AddItem(Product product, int quantity)
        {
            // Check if the product already exists in the cart.
            var existingItem = Items.FirstOrDefault(i => i.Product.ProductId == product.ProductId);

            // If the product exists, update the quantity.
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            // If the product does not exist, add it as a new item.
            else
            {
                Items.Add(new CartItem { Product = product, Quantity = quantity });
            }

            // Log the current items in the cart for debugging purposes.
            System.Diagnostics.Debug.WriteLine($"Added Item: {product.ProductName}, Quantity: {quantity}. Total Items: {Items.Count}");
        }

        // Method to clear all items from the shopping cart.
        public void Clear()
        {
            Items.Clear(); // Clear the list of items.

            // Log the clear action for debugging purposes.
            System.Diagnostics.Debug.WriteLine("Shopping cart cleared.");
        }

        // Method to convert the shopping cart items into order details.
        public List<OrderDetail> GetOrderDetails()
        {
            // Create and return a list of OrderDetail objects from the cart items.
            return Items.Select(i => new OrderDetail
            {
                ProductId = i.Product.ProductId, // Set ProductId.
                Quantity = i.Quantity, // Set Quantity.
                Product = i.Product // Set Product.
            }).ToList();
        }
    }

    // The CartItem class represents an item in the shopping cart.
    public class CartItem
    {
        // The product associated with the cart item.
        public Product Product { get; set; }

        // The quantity of the product in the cart.
        public int Quantity { get; set; }
    }
}


