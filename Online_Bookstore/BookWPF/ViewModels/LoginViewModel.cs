using OnlineBookstore.Helpers;
using System.Windows;
using System.Windows.Input;

namespace OnlineBookstore.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _username;
        private string _password;

        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        public ICommand LoginCommand { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(Login);
        }

        private void Login(object parameter)
        {
            if (Username == "admin" && Password == "password")
            {
                MainView mainView = new MainView();
                mainView.Show();
                Application.Current.Windows[0].Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }
    }
}
