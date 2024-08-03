using OnlineBookstore.Models;
using System.Collections.ObjectModel;

namespace OnlineBookstore.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<Book> Books { get; set; }
        public ObservableCollection<Order> Orders { get; set; }
        public User CurrentUser { get; set; }

        public MainViewModel()
        {
            Books = new ObservableCollection<Book>();
            Orders = new ObservableCollection<Order>();

            // Mock Data
            Books.Add(new Book
            {
                Id = 1,
                Title = "C# Programming",
                Author = "John Doe",
                Description = "A comprehensive guide to C# programming.",
                Price = 29.99m,
                Category = "Programming",
                IsAvailable = true,
                ImagePath = "Assets/book1.png"
            });

            Books.Add(new Book
            {
                Id = 2,
                Title = "ASP.NET Core MVC",
                Author = "Jane Smith",
                Description = "Learn ASP.NET Core MVC from scratch.",
                Price = 39.99m,
                Category = "Web Development",
                IsAvailable = true,
                ImagePath = "Assets/book2.png"
            });
        }

        public void AddBook(Book book)
        {
            Books.Add(book);
        }

        public void PlaceOrder(Order order)
        {
            Orders.Add(order);
        }
    }
}
