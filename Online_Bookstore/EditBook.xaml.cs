using System.IO;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Media.Imaging;


namespace BookstoreApp
{
    public partial class EditBook : Window
    {
        // Assuming a static list for storing books
        public static List<Book> Books = new List<Book>();

        private Book _originalBook;
        public EditBook(Book originalBook)
        {
            InitializeComponent();
            _originalBook = originalBook;

            // Set the details of the book
            BookTitleTextBox.Text = $"{originalBook.Title}";
            BookAuthorTextBox.Text = $"{originalBook.Author}";
            BookPriceTextBox.Text = $"{originalBook.Price}";
            BookCategoryTextBox.Text = $"{originalBook.Category}";
            BookAvailabilityTextBox.Text = $"{originalBook.Availability}";
            BookDescriptionTextBox.Text = $"{originalBook.Description}";

            if (originalBook.Picture != null && originalBook.Picture.Length > 0)
            {
                try
                {
                    using (var memoryStream = new MemoryStream(originalBook.Picture))
                    {
                        // Reset the position of the memory stream to the beginning
                        memoryStream.Position = 0;

                        var bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.StreamSource = memoryStream;
                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad; // Ensure the image is fully loaded
                        bitmapImage.EndInit();

                        BookPictureImage.Source = bitmapImage;
                    }
                }
                catch (Exception ex)
                {
                    // Log or handle the exception
                    MessageBox.Show($"Error loading image: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                BookPictureImage.Source = null;
            }
            originalBook.Title = BookTitleTextBox.Text;
        }
        

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var title = BookTitleTextBox.Text;
            var author = BookAuthorTextBox.Text;
            var description = BookDescriptionTextBox.Text;
            var priceText = BookPriceTextBox.Text;
            var category = BookCategoryTextBox.Text;

            // Convert price to decimal
            decimal price;
            if (!decimal.TryParse(priceText, out price))
            {
                MessageBox.Show("Please enter a valid number for Price.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(picture));
                    encoder.Save(memoryStream);
                    pictureBytes = memoryStream.ToArray();
                }
            }

            var availabilityText = BookAvailabilityTextBox.Text;

            int availability;
            if (!int.TryParse(availabilityText, out availability))
            {
                MessageBox.Show("Please enter a valid number for Availability.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            _originalBook.Title = title;
            _originalBook.Author = author;
            _originalBook.Description = description;
            _originalBook.Price = price;
            _originalBook.Category = category;
            _originalBook.Availability = availability;
            _originalBook.Picture = pictureBytes;

            MessageBox.Show("Updated Successfully!");
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
        private void ClearForm()
        {
            BookTitleTextBox.Clear();
            BookAuthorTextBox.Clear();
            BookDescriptionTextBox.Clear();
            BookPriceTextBox.Clear();
            BookCategoryTextBox.Clear();
            BookPictureImage.Source = null;
            BookAvailabilityTextBox.Clear();
        }
    }
}