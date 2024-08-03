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
    public partial class ShoppingCartWindow : Window
    {
        private ObservableCollection<Book> _cart;
        private User _currentUser;

        public ShoppingCartWindow(ObservableCollection<Book> cart, User currentUser)
        {
            InitializeComponent();
            _cart = cart;
            _currentUser = currentUser;

            var displayableBooks = _cart.Select(b => new
            {
                Title = b.Title,
                Price = b.Price,
                PictureSource = GetBitmapImageFromBytes(b.Picture) // Convert byte array to BitmapImage
            }).ToList();

            CartListBox.ItemsSource = displayableBooks;
            UpdateTotalPrice();
        }

        private void CheckoutButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentUser == null)
            {
                MessageBox.Show("No user is currently logged in or user could not be retrieved.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var order = new Order
                {
                    OrderId = GenerateOrderId(), // Implement order ID generation logic
                    UserName = _currentUser.UserName,
                    OrderDate = DateTime.Now,
                    OrderItems = new ObservableCollection<OrderItem>(
                        _cart.Select(b => new OrderItem
                        {
                            BookTitle = b.Title,
                            Quantity = 1 // Assuming each book is bought once. Modify as needed.
                        })
                    )
                };

                SaveOrder(order);

                // Clear the cart after checkout
                _cart.Clear();

                MessageBox.Show("Order placed successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during checkout: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private int GenerateOrderId()
        {
            return new Random().Next(1000, 9999); // Example logic
        }

        private void SaveOrder(Order order)
        {
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

                orderCollection.Orders.Add(order);

                var xmlSerializerForSaving = new XmlSerializer(typeof(OrderCollection));
                using (var writer = new StreamWriter(filePath))
                {
                    xmlSerializerForSaving.Serialize(writer, orderCollection);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving order: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CartListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = CartListBox.SelectedItem as dynamic;

            if (selectedItem != null)
            {
                CartDetailsImage.Source = selectedItem.PictureSource;
                CartDetailsTextBlock.Text = $"Title: {selectedItem.Title}\n" +
                                            $"Price: {selectedItem.Price:C}";
            }
            else
            {
                CartDetailsImage.Source = null;
                CartDetailsTextBlock.Text = "Select a book to see details";
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

        private void UpdateTotalPrice()
        {
            var totalPrice = _cart.Sum(b => b.Price);
            TotalPriceTextBlock.Text = $"Total Price: {totalPrice:C}";
        }
    }
}