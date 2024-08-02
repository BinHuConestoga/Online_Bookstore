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
                AdminWindow.Books.Remove(selectedBook);
                MessageBox.Show("This book is deleted!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                SearchButton_Click(sender, e);
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
