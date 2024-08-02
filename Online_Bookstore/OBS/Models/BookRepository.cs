using System.Collections.Generic;
using System.Linq;

namespace OnlineBookstore.Models
{
    public static class BookRepository
    {
        private static List<Book> books = new List<Book>
        {
            new Book { Id = 1, Title = "C# Programming", Author = "John Doe", Description = "Learn C# programming", Price = 29.99m, Category = "Programming", Availability = true },
            new Book { Id = 2, Title = "ASP.NET Core MVC", Author = "Jane Smith", Description = "Master ASP.NET Core MVC", Price = 39.99m, Category = "Web Development", Availability = true }
        };

        public static List<Book> GetBooks()
        {
            return books;
        }

        public static Book GetBookById(int id)
        {
            return books.FirstOrDefault(b => b.Id == id);
        }
    }
}
