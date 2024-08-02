using Microsoft.AspNetCore.Mvc;
using OnlineBookstore.Models;
using System.Collections.Generic;

namespace OnlineBookstore.Controllers
{
    public class CartController : Controller
    {
        public static List<CartItem> cart = new List<CartItem>();

        public IActionResult Index()
        {
            var cartItems = CartRepository.GetCartItems();
            return View(cartItems);
        }

        public IActionResult AddToCart(int id)
        {
            var book = BookRepository.GetBookById(id);
            CartRepository.AddToCart(book);


            return RedirectToAction("Index");
        }

        public IActionResult Checkout()
        {
            // Simulate checkout logic
            CartRepository.ClearCart();
            return View();
        }
    }
}
