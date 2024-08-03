using Online_Bookstore;
using System.Windows;

namespace BookstoreApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SignOnButton_Click(object sender, RoutedEventArgs e)
        {
            var username = UserNameTextBox.Text;
            var password = PasswordBox.Password;

            var user = AuthenticateUser(username, password);
            if (user != null)
            {
                UserManager.SetCurrentUser(user);
                // Open the appropriate window based on user role
                Console.WriteLine("role"+user.UserRole);
                if (user.UserRole.ToString().Equals("Admin"))
                {
                    /*var adminWindow = new AdminWindow();
                    adminWindow.Show();*/
                    var adminMainWindow = new AdminMainWindow();
                    this.Close();
                    adminMainWindow.Show();
                }
                else if (user.UserRole.ToString().Equals("Customer"))
                {
                    var customerWindow = new CustomerWindow();
                    customerWindow.Show();
                }
                this.Close(); // Close the login window
            }
            else
            {
                LoginWarningTextBlock.Text = "Invalid username or password.";
            }
        }

        private User AuthenticateUser(string username, string password)
        {
            // Call the UserStore to authenticate
            return UserStore.AuthenticateUser(username, password);
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var registerWindow = new RegisterWindow();
            registerWindow.ShowDialog(); // Show registration window as a dialog
        }
    }
}