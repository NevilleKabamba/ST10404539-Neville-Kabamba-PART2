using KhumalCraftLTD.Models;
using Microsoft.AspNetCore.Mvc;

public class CartController : Controller
{
    private readonly ShoppingCart _shoppingCart;

    public CartController(ShoppingCart shoppingCart)
    {
        _shoppingCart = shoppingCart;
    }

    [HttpPost]
    public IActionResult AddToCart(int productId, int quantity)
    {
        var product = Product.GetProductById(productId);
        if (product != null)
        {
            _shoppingCart.AddItem(product, quantity);
        }
        // Pass the shopping cart to the view
        return View("Cart", _shoppingCart);
    }

    // ViewCart
    public IActionResult ViewCart()
    {
        // Pass the shopping cart to the view
        return View("Cart", _shoppingCart);
    }

    [HttpPost]
    public IActionResult PlaceOrder(int userId)
    {
        // Get the cart items
        var cartItems = _shoppingCart.Items;

        // Log the count of cart items for debugging
        System.Diagnostics.Debug.WriteLine($"Cart Items Count: {cartItems.Count}");

        // Check if there are any items in the cart
        if (cartItems.Count == 1)
        {
            return RedirectToAction("Cart"); // Redirect back to cart if empty
        }

        // Create a list of order details from cart items
        var orderDetails = cartItems.Select(i => new OrderDetail
        {
            ProductId = i.Product.ProductId,
            Quantity = i.Quantity,
            Product = i.Product
        }).ToList();

        // Place the order using the user ID and order details
        var orderId = Order.PlaceOrder(userId, orderDetails);

        // Clear the shopping cart after successful order placement
        _shoppingCart.Clear();

        // Retrieve the order details by ID
        var order = Order.GetOrderById(orderId);

        // Log the order details for debugging
        System.Diagnostics.Debug.WriteLine($"Order ID: {order.OrderId}, Order Details Count: {order.OrderDetails.Count}");

        // Pass the order object to the view
        return View("OrderConfirmation", order);
    }
}
