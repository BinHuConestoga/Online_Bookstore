using System.IO;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace BookstoreApp
{
    public partial class AdminWindow : Window
    {
        // Assuming a static list for storing books
        public static List<Book> Books = new List<Book>();

        public AdminWindow()
        {
            InitializeComponent();

            if (File.Exists("books.xml"))
            {
                LoadBooksFromXml("books.xml");
            }
        }

        private void SaveBooksToXml(string filePath)
        {
            var serializer = new XmlSerializer(typeof(BookCollection));
            using (var writer = new StreamWriter(filePath))
            {
                var bookCollection = new BookCollection { Books = Books };
                serializer.Serialize(writer, bookCollection);
            }
        }

        public void LoadBooksFromXml(string filePath)
        {
            var serializer = new XmlSerializer(typeof(BookCollection));
            using (var reader = new StreamReader(filePath))
            {
                var bookCollection = (BookCollection)serializer.Deserialize(reader);
                Books = bookCollection.Books;
            }
        }

        private void QueryBooks()
        {
            // Assuming books are already loaded into Books list
            var query = from book in Books
                        where book.Price > 20m
                        orderby book.Title
                        select book;

            foreach (var book in query)
            {
                Console.WriteLine($"{book.Title} by {book.Author}");
            }
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

            SaveBook(title, author, description, price, category, pictureBytes, availability);

            SaveBooksToXml("books.xml");
        }

        private void SaveBook(string title, string author, string description, decimal price, string category, byte[] picture, int availability)
        {
            var book = new Book
            {
                Title = title,
                Author = author,
                Description = description,
                Price = price,
                Category = category,
                Picture = picture,
                Availability = availability
            };

            Books.Add(book);

            MessageBox.Show($"Book saved:\nTitle: {title}\nAuthor: {author}\nDescription: {description}\nPrice: {price}\nCategory: {category}\nAvailability: {availability}", "Book Saved", MessageBoxButton.OK, MessageBoxImage.Information);

            ClearForm();
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