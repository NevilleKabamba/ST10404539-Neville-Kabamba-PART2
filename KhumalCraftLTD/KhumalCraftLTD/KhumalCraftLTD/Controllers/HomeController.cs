using Microsoft.AspNetCore.Mvc;
using KhumalCraftLTD.Models; 
using Microsoft.Extensions.Logging; 
using System.Collections.Generic; 
using System.Diagnostics;

namespace KhumalCraftLTD.Controllers
{
    // The HomeController handles the main actions for the home-related views.
    public class HomeController : Controller
    {
        // Private readonly field to hold the logger instance.
        private readonly ILogger<HomeController> _logger;

        // Constructor to initialize the logger via dependency injection.
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Action method to handle requests to the home page.
        public IActionResult Index()
        {
            // Return the default view for the home page.
            return View();
        }

        // Action method to handle requests to the privacy page.
        public IActionResult Privacy()
        {
            // Return the default view for the privacy page.
            return View();
        }

        // Action method to handle GET requests to the about page.
        [HttpGet]
        public IActionResult About()
        {
            // Return the default view for the about page.
            return View();
        }

        // Action method to handle GET requests to the contact page.
        [HttpGet]
        public IActionResult Contact()
        {
            // Return the default view for the contact page.
            return View();
        }

        // Action method to handle GET requests to the MyWork page, which displays a list of products.
        [HttpGet]
        public IActionResult MyWork()
        {
            // Retrieve all products from the model.
            List<Product> products = Product.GetAllProducts();
            // Return the view for the MyWork page with the list of products.
            return View(products);
        }

        // Action method to handle errors and display the error view.
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Create a new ErrorViewModel with the current request ID.
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
