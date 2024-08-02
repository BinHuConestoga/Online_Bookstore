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
            new Book { Id = 1, Title = "C# in Depth", Author = "Jon Skeet", Description = "Deep dive into C#", Price = 39.99M, Category = "Programming", ImageUrl = "/images/csharp.jpg" },
            new Book { Id = 2, Title = "Clean Code", Author = "Robert C. Martin", Description = "A Handbook of Agile Software Craftsmanship", Price = 29.99M, Category = "Programming", ImageUrl = "/images/cleancode.jpg" }
        };

        public IActionResult Index()
        {
            return View(books);
        }

        public IActionResult Details(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null) return NotFound();
            return View(book);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                book.Id = books.Max(b => b.Id) + 1;
                books.Add(book);
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        public IActionResult Edit(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null) return NotFound();
            return View(book);
        }

        [HttpPost]
        public IActionResult Edit(Book book)
        {
            var existingBook = books.FirstOrDefault(b => b.Id == book.Id);
            if (existingBook == null) return NotFound();

            if (ModelState.IsValid)
            {
                existingBook.Title = book.Title;
                existingBook.Author = book.Author;
                existingBook.Description = book.Description;
                existingBook.Price = book.Price;
                existingBook.Category = book.Category;
                existingBook.ImageUrl = book.ImageUrl;

                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        public IActionResult Delete(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null) return NotFound();
            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null) return NotFound();
            books.Remove(book);
            return RedirectToAction(nameof(Index));
        }
    }
}
