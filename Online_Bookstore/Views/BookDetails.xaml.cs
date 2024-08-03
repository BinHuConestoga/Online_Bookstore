using System.IO;
using System.Windows.Media.Imaging;
using System.Windows;

namespace BookstoreApp
{
    public partial class BookDetails : Window
    {
        public BookDetails(Book book)
        {
            InitializeComponent();
            // Set the details of the book
            TitleTextBlock.Text = $"Title: {book.Title}";
            AuthorTextBlock.Text = $"Author: {book.Author}";
            PriceTextBlock.Text = $"Price: {book.Price:C}";
            CategoryTextBlock.Text = $"Category: {book.Category}";
            DescriptionTextBlock.Text = $"Description: {book.Description}";

            if (book.Picture != null && book.Picture.Length > 0)
            {
                try
                {
                    using (var memoryStream = new MemoryStream(book.Picture))
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
        }
    }
}