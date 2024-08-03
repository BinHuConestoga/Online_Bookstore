using BookstoreApp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Reflection.Metadata.BlobBuilder;
using System.Xml.Serialization;

namespace Online_Bookstore
{
    /// <summary>
    /// Interaction logic for AdminMainWindow.xaml
    /// </summary>
    public partial class AdminMainWindow : Window
    {
        public AdminMainWindow()
        {
            InitializeComponent();

            LoadBooks();
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

        private void BooksListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BooksListBox.SelectedItem is Book selectedBook)
            {
/*                BookDetailsTextBlock.Text = $"Title: {selectedBook.Title}\n" +
                                            $"Author: {selectedBook.Author}\n" +
                                            $"Description: {selectedBook.Description}\n" +
                                            $"Price: {selectedBook.Price}\n" +
                                            $"Category: {selectedBook.Category}\n" +
                                            $"Availability: {selectedBook.Availability}";*/

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
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var adminWindow = new AdminWindow();
            adminWindow.Show();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (BooksListBox.SelectedItem is Book selectedBook)
            {
                // Confirm removal
                var result = MessageBox.Show("Are you sure you want to remove this book?", "Confirm Removal", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    // Remove the book from the in-memory collection
                    var bookToRemove = AdminWindow.Books.FirstOrDefault(b => b.Title == selectedBook.Title);
                    if (bookToRemove != null)
                    {
                        AdminWindow.Books.Remove(bookToRemove);

                        // Save the updated collection to the XML file
                        SaveBookList();

                        MessageBox.Show("The book has been removed successfully.", "Book Removed", MessageBoxButton.OK, MessageBoxImage.Information);
                        SearchButton_Click(sender, e); // Refresh the search results
                    }
                    else
                    {
                        MessageBox.Show("The selected book could not be found in the collection.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a book to remove.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void SaveBookList()
        {
            try
            {
                // Create a new book collection and populate it with the current list
                var bookCollection = new BookCollection
                {
                    Books = AdminWindow.Books
                };

                // Save the updated list back to XML
                var serializer = new XmlSerializer(typeof(BookCollection));
                using (var writer = new StreamWriter("books.xml"))
                {
                    serializer.Serialize(writer, bookCollection);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving book list: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (BooksListBox.SelectedItem is Book selectedBook)
            {
                var editBook = new EditBook(selectedBook);
                editBook.Show();
/*                EditBook.
                AdminWindow.Books.Remove(selectedBook);
                MessageBox.Show("This book is deleted!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                SearchButton_Click(sender, e);*/
            }
        }
    }
}
