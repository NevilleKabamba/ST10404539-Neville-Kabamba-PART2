using Microsoft.AspNetCore.Mvc;
using KhumalCraftLTD.Models;

namespace KhumalCraftLTD.Controllers
{
    // The ProductController handles actions related to products and the shopping cart.
    public class ProductController : Controller
    {
        // Private readonly field to hold the shopping cart instance.
        private readonly ShoppingCart _shoppingCart;

        // Constructor to initialize the shopping cart via dependency injection.
        public ProductController(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        // Action method to handle adding a product to the shopping cart.
        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity)
        {
            // Retrieve the product by its ID.
            var product = Product.GetProductById(productId);

            // If the product exists, add it to the shopping cart with the specified quantity.
            if (product != null)
            {
                _shoppingCart.AddItem(product, quantity);
            }

            // Redirect to the Cart action to display the updated cart.
            return RedirectToAction("Cart");
        }

        // Action method to display the contents of the shopping cart.
        public IActionResult Cart()
        {
            // Return the view for the shopping cart, passing the shopping cart instance.
            return View(_shoppingCart);
        }

        // Action method to handle placing an order.
        [HttpPost]
        public IActionResult PlaceOrder(int userId)
        {
            // Placeholder for the logic to place an order.

            // Return the OrderConfirmation view to confirm the order placement.
            return View("OrderConfirmation");
        }
    }
}
