using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace BookstoreApp
{
    public partial class EditBook : Window
    {
        private Book _originalBook;

        public EditBook(Book originalBook)
        {
            InitializeComponent();
            _originalBook = originalBook;

            // Set the details of the book
            BookTitleTextBox.Text = originalBook.Title;
            BookAuthorTextBox.Text = originalBook.Author;
            BookPriceTextBox.Text = originalBook.Price.ToString();
            BookCategoryTextBox.Text = originalBook.Category;
            BookAvailabilityTextBox.Text = originalBook.Availability.ToString();
            BookDescriptionTextBox.Text = originalBook.Description;

            if (originalBook.Picture != null && originalBook.Picture.Length > 0)
            {
                try
                {
                    using (var memoryStream = new MemoryStream(originalBook.Picture))
                    {
                        memoryStream.Position = 0;

                        var bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.StreamSource = memoryStream;
                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                        bitmapImage.EndInit();

                        BookPictureImage.Source = bitmapImage;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading image: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                BookPictureImage.Source = null;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var title = BookTitleTextBox.Text;
            var author = BookAuthorTextBox.Text;
            var description = BookDescriptionTextBox.Text;
            var priceText = BookPriceTextBox.Text;
            var category = BookCategoryTextBox.Text;
            var availabilityText = BookAvailabilityTextBox.Text;

            // Convert price to decimal
            if (!decimal.TryParse(priceText, out decimal price))
            {
                MessageBox.Show("Please enter a valid number for Price.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Convert availability to integer
            if (!int.TryParse(availabilityText, out int availability))
            {
                MessageBox.Show("Please enter a valid number for Availability.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Get the image source
            var picture = BookPictureImage.Source as BitmapImage;

            // Convert image to byte array
            byte[] pictureBytes = null;
            if (picture != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    var encoder = new JpegBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(picture));
                    encoder.Save(memoryStream);
                    pictureBytes = memoryStream.ToArray();
                }
            }

            // Update the original book's details
            _originalBook.Title = title;
            _originalBook.Author = author;
            _originalBook.Description = description;
            _originalBook.Price = price;
            _originalBook.Category = category;
            _originalBook.Availability = availability;
            _originalBook.Picture = pictureBytes;

            // Save the updated book list
            SaveBookList();

            MessageBox.Show("Updated Successfully!");
            this.Close();
        }

        private void SaveBookList()
        {
            try
            {
                // Load existing books from XML
                var bookCollection = LoadBookCollection();

                // Replace the updated book in the list
                var bookToUpdate = bookCollection.Books.FirstOrDefault(b => b.Title == _originalBook.Title);
                if (bookToUpdate != null)
                {
                    // Update the book details
                    bookToUpdate.Title = _originalBook.Title;
                    bookToUpdate.Author = _originalBook.Author;
                    bookToUpdate.Description = _originalBook.Description;
                    bookToUpdate.Price = _originalBook.Price;
                    bookToUpdate.Category = _originalBook.Category;
                    bookToUpdate.Availability = _originalBook.Availability;
                    bookToUpdate.Picture = _originalBook.Picture;
                }
                else
                {
                    // If the book does not exist, add it to the collection
                    bookCollection.Books.Add(_originalBook);
                }

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

        private BookCollection LoadBookCollection()
        {
            try
            {
                if (File.Exists("books.xml"))
                {
                    var serializer = new XmlSerializer(typeof(BookCollection));
                    using (var reader = new StreamReader("books.xml"))
                    {
                        return (BookCollection)serializer.Deserialize(reader);
                    }
                }
                return new BookCollection { Books = new List<Book>() };
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading book list: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return new BookCollection { Books = new List<Book>() };
            }
        }

        private void SelectPictureButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp|All Files|*.*",
                Title = "Select a Picture"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                var imagePath = openFileDialog.FileName;
                BookPictureImage.Source = new BitmapImage(new Uri(imagePath));
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}