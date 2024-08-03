using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace BookstoreApp
{
    public partial class OrderHistoryWindow : Window
    {
        private User _currentUser;

        public OrderHistoryWindow(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            LoadOrderHistory();
        }

        private void LoadOrderHistory()
        {
            if (_currentUser == null)
            {
                MessageBox.Show("No user is currently logged in or user could not be retrieved.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                string filePath = $"{_currentUser.UserName}_orders.xml";
                OrderCollection orderCollection;

                if (File.Exists(filePath))
                {
                    var xmlSerializer = new XmlSerializer(typeof(OrderCollection));
                    using (var reader = new StreamReader(filePath))
                    {
                        orderCollection = (OrderCollection)xmlSerializer.Deserialize(reader);
                    }
                }
                else
                {
                    orderCollection = new OrderCollection { Orders = new ObservableCollection<Order>() };
                }

                var displayableOrders = orderCollection.Orders.Select(order => new
                {
                    OrderId = order.OrderId,
                    OrderDate = order.OrderDate,
                    OrderItems = order.OrderItems.Select(item => new
                    {
                        BookTitle = item.BookTitle,
                        Price = GetBookPrice(item.BookTitle),
                        PictureSource = GetBookPicture(item.BookTitle)
                    }).ToList(),
                    TotalPrice = order.OrderItems.Sum(item => GetBookPrice(item.BookTitle))
                }).ToList();

                OrdersListBox.ItemsSource = displayableOrders;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading order history: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private decimal GetBookPrice(string bookTitle)
        {
            try
            {
                var allBooks = LoadAllBooks();
                var book = allBooks.FirstOrDefault(b => b.Title == bookTitle);
                return book?.Price ?? 0;
            }
            catch
            {
                return 0;
            }
        }

        private BitmapImage GetBookPicture(string bookTitle)
        {
            try
            {
                var allBooks = LoadAllBooks();
                var book = allBooks.FirstOrDefault(b => b.Title == bookTitle);
                return GetBitmapImageFromBytes(book?.Picture);
            }
            catch
            {
                return null;
            }
        }

        private ObservableCollection<Book> LoadAllBooks()
        {
            try
            {
                string filePath = "books.xml";
                if (File.Exists(filePath))
                {
                    var xmlSerializer = new XmlSerializer(typeof(BookCollection));
                    using (var reader = new StreamReader(filePath))
                    {
                        var bookCollection = (BookCollection)xmlSerializer.Deserialize(reader);
                        return new ObservableCollection<Book>(bookCollection.Books);
                    }
                }
                return new ObservableCollection<Book>();
            }
            catch
            {
                return new ObservableCollection<Book>();
            }
        }

        private BitmapImage GetBitmapImageFromBytes(byte[] pictureBytes)
        {
            if (pictureBytes == null || pictureBytes.Length == 0)
                return null;

            using (var memoryStream = new MemoryStream(pictureBytes))
            {
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                return bitmapImage;
            }
        }
    }
}