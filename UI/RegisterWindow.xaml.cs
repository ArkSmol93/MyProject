using System.Windows;
using System.Windows.Controls;
using UI.Models;

namespace UI
{
    public partial class RegisterWindow : Window
    {
        private readonly ApiService _apiService = new ApiService();

        public RegisterWindow()
        {
            InitializeComponent();
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var user = new User
            {
                Username = UsernameTextBox.Text,
                Password = PasswordBox.Password
            };

            bool success = await _apiService.RegisterUser(user);
            MessageTextBlock.Text = success ? "Zarejestrowano!" : "Błąd rejestracji";
        }
    }
}
