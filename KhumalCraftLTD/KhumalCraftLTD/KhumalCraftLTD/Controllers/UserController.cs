using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration; 
using KhumalCraftLTD.Models; 

namespace KhumalCraftLTD.Controllers
{
    // The UserController handles actions related to user management.
    public class UserController : Controller
    {
        // Private readonly field to hold the Users instance.
        private readonly Users _user;

        // Constructor to initialize the Users instance via dependency injection.
        public UserController(Users user)
        {
            _user = user;
        }

        // Action method to handle user information submission via a POST request.
        [HttpPost]
        public ActionResult About(Users user)
        {
            // Insert the user information into the data store.
            var result = _user.InsertUser(user);

            // Redirect to the Index action of the HomeController after the user is inserted.
            return RedirectToAction("Index", "Home");
        }

        // Action method to display the About view with a new Users instance for a GET request.
        [HttpGet]
        public ActionResult About()
        {
            // Return the About view with a new Users instance.
            return View(new Users());
        }
    }
}

