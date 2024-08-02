using Microsoft.AspNetCore.Mvc;
using OnlineBookstore.Models;
using System.Collections.Generic;
using System.Linq;

namespace OnlineBookstore.Controllers
{
    public class AccountController : Controller
    {
        private static List<User> users = new List<User>
        {
            new User { UserName = "admin", Password = "admin", Role = "Admin" },
            new User { UserName = "customer", Password = "customer", Role = "Customer" }
        };

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            var existingUser = users.FirstOrDefault(u => u.UserName == user.UserName && u.Password == user.Password);
            if (existingUser == null)
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View(user);
            }

            // Set session or cookie
            // HttpContext.Session.SetString("username", existingUser.UserName);
            // HttpContext.Session.SetString("role", existingUser.Role);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                users.Add(user);
                return RedirectToAction(nameof(Login));
            }
            return View(user);
        }

        public IActionResult Logout()
        {
            // Clear session or cookie
            // HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }
    }
}
