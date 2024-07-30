using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.IO;
using System.Collections.Generic;

namespace BookstoreApp
{
    public partial class CustomerWindow : Window
    {
        public CustomerWindow()
        {
            InitializeComponent();
            LoadBooks();
        }

        private void LoadBooks()
        {
            // Retrieve the first 9 books from the list
            var booksToDisplay = AdminWindow.Books.Take(9).ToList();
            BooksListBox.ItemsSource = booksToDisplay;
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
    }
}