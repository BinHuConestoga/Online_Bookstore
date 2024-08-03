using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace BookstoreApp
{
    public partial class CustomerWindow : Window
    {
        public ObservableCollection<Book> Cart { get; private set; }
        private User _currentUser;

        public CustomerWindow()
        {
            InitializeComponent();
            LoadBooks();
            Cart = new ObservableCollection<Book>();
        }

        private void LoadBooks()
        {
            try
            {
                if (File.Exists("books.xml"))
                {
                    var serializer = new XmlSerializer(typeof(BookCollection));
                    using (var reader = new StreamReader("books.xml"))
                    {
                        var bookCollection = (BookCollection)serializer.Deserialize(reader);
                        AdminWindow.Books = bookCollection.Books;
                    }
                }
                else
                {
                    AdminWindow.Books = new List<Book>();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading books: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                AdminWindow.Books = new List<Book>();
            }

            var filteredBooks = AdminWindow.Books.Take(9).ToList();

            BooksListBox.ItemsSource = filteredBooks;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var searchTerm = SearchTextBox.Text.ToLower();

            var filteredBooks = AdminWindow.Books
                .Where(b => b.Title.ToLower().Contains(searchTerm) || b.Description.ToLower().Contains(searchTerm))
                .Take(9)  // Limit to 9 results
                .ToList();

            BooksListBox.ItemsSource = filteredBooks;
        }

        private void BookImage_Click(object sender, RoutedEventArgs e)
        {
            // Find the Image that was clicked
            var image = sender as Image;
            if (image != null)
            {
                var selectedBook = image.DataContext as Book;
                if (selectedBook != null)
                {
                    // Open the book details in a new window
                    var bookDetailsWindow = new BookDetails(selectedBook);
                    bookDetailsWindow.Show();
                }
            }
        }

        private void BooksListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BooksListBox.SelectedItem is Book selectedBook)
            {
                BookDetailsTextBlock.Text = $"Title: {selectedBook.Title}\n" +
                                            $"Author: {selectedBook.Author}\n" +
                                            $"Description: {selectedBook.Description}\n" +
                                            $"Price: {selectedBook.Price}\n" +
                                            $"Category: {selectedBook.Category}\n" +
                                            $"Availability: {selectedBook.Availability}";

                // Convert byte array to image
                if (selectedBook.Picture != null)
                {
                    using (var memoryStream = new MemoryStream(selectedBook.Picture))
                    {
                        var bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.StreamSource = memoryStream;
                        bitmapImage.EndInit();
                        BookPictureImage.Source = bitmapImage;
                    }
                }
                else
                {
                    BookPictureImage.Source = null;
                }
            }
        }

        private void AddToCartButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var book = button.DataContext as Book;
                if (book != null)
                {
                    // Add the book to the cart
                    Cart.Add(book);

                    MessageBox.Show($"Added {book.Title} to cart.", "Added to Cart", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void CheckoutButton_Click(object sender, RoutedEventArgs e)
        {
            var currentUser = UserManager.GetCurrentUser();
            if (currentUser == null)
            {
                MessageBox.Show("No user is currently logged in or user could not be retrieved.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Pass the current cart and current user to the ShoppingCartWindow
            var shoppingCartWindow = new ShoppingCartWindow(Cart, currentUser);
            shoppingCartWindow.Show();

        }

        private void OrderHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            var currentUser = UserManager.GetCurrentUser();
            var orderHistoryWindow = new OrderHistoryWindow(currentUser);
            orderHistoryWindow.Show();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
        }
    }
}