using Microsoft.AspNetCore.Mvc;
using OnlineBookstore.Models;
using System.Collections.Generic;
using System.Linq;

namespace OnlineBookstore.Controllers
{
    public class CartController : Controller
    {
        private static List<Book> cart = new List<Book>();

        public IActionResult Index()
        {
            return View(cart);
        }

        public IActionResult AddToCart(int id)
        {
            var book = BooksController.books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                cart.Add(book);
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveFromCart(int id)
        {
            var book = cart.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                cart.Remove(book);
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Checkout()
        {
            // Create a new order with the books in the cart
            var order = new Order
            {
                OrderId = 1, // Normally, this would be auto-generated
                UserName = "customer", // This would be dynamically set based on logged-in user
                Books = new List<Book>(cart),
                TotalAmount = cart.Sum(b => b.Price),
                OrderDate = DateTime.Now,
                Status = "Pending"
            };

            // Clear the cart
            cart.Clear();

            // Display the order confirmation
            return View(order);
        }
    }
}
