using Microsoft.AspNetCore.Mvc;
using OnlineBookstore.Models;
using System.Collections.Generic;
using System.Linq;

namespace OnlineBookstore.Controllers
{
    public class BooksController : Controller
    {
        public static List<Book> books = new List<Book>
        {
            new Book { Id = 1, Title = "C# Programming", Author = "John Doe", Description = "Learn C# programming", Price = 29.99m, Category = "Programming", Availability = true },
            new Book { Id = 2, Title = "ASP.NET Core MVC", Author = "Jane Smith", Description = "Master ASP.NET Core MVC", Price = 39.99m, Category = "Web Development", Availability = true }
        };

        
        public IActionResult Index()
        {
            var books = BookRepository.GetBooks();
            return View(books);
        }

        public IActionResult Details(int id)
        {
            var book = BookRepository.GetBookById(id);
            return View(book);
        }
    }
}
