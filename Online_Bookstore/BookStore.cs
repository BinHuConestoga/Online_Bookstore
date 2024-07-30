using System.Collections.Generic;
using System.Linq;

namespace BookstoreApp
{
    public static class BookStore
    {
        // In-memory list to store books
        private static readonly List<Book> Books = new List<Book>();

        // Method to add or update a book
        public static void SaveBook(Book book)
        {
/*            var existingBook = Books.FirstOrDefault(b => b.Title == book.Title);
            if (existingBook != null)
            {
                Books.Remove(existingBook);
            }*/
            Books.Add(book);
        }

        // Method to get all books
        public static IEnumerable<Book> GetBooks()
        {
            return Books;
        }
    }
}