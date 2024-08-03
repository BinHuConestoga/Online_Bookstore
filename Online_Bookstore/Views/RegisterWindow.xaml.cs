using System;
using System.Windows;
using System.Windows.Controls;

namespace BookstoreApp
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var email = EmailTextBox.Text;
            var username = RegisterUserNameTextBox.Text;
            var password = RegisterPasswordBox.Password;
            var dateOfBirth = DateOfBirthPicker.SelectedDate;
            var address = HomeAddressTextBox.Text;

            if (RoleComboBox.SelectedItem is ComboBoxItem selectedRoleItem)
            {
                if (!Enum.TryParse(selectedRoleItem.Content.ToString(), out Role role))
                {
                    RegisterWarningTextBlock.Text = "Invalid role selected.";
                    return;
                }

                // Basic validation
                if (string.IsNullOrWhiteSpace(email) ||
                    string.IsNullOrWhiteSpace(username) ||
                    string.IsNullOrWhiteSpace(password) ||
                    dateOfBirth == null ||
                    string.IsNullOrWhiteSpace(address))
                {
                    RegisterWarningTextBlock.Text = "Please fill all fields.";
                    return;
                }

                if (UserStore.UserExists(username))
                {
                    RegisterWarningTextBlock.Text = "User already exists.";
                    return;
                }

                // Save user data
                SaveUser(email, username, password, dateOfBirth.Value, address, role);

                MessageBox.Show("Registration successful!");
                this.Close();
            }
            else
            {
                RegisterWarningTextBlock.Text = "Please select a role.";
            }
        }

        private void SaveUser(string email, string username, string password, DateTime dateOfBirth, string address, Role role)
        {
            var newUser = new User
            {
                UserName = username,
                Password = password,
                UserRole = role,
                Email = email,
                DateOfBirth = dateOfBirth,
                HomeAddress = address
            };

            UserStore.SaveUser(newUser, password);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}