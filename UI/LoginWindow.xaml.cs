using System.Windows;
using System.Windows.Controls;
using UI.Models;

namespace UI
{
    public partial class LoginWindow : Window
    {
        private readonly ApiService _apiService = new ApiService();

        public LoginWindow()
        {
            InitializeComponent();
        }

        // ===== TextBox GotFocus =====
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb && tb.Text == "Wpisz login")
            {
                tb.Clear();
                tb.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        // ===== PasswordBox GotFocus =====
        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox pb && pb.Password == "hasło")
            {
                pb.Clear();
                pb.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        // ===== LostFocus dla obu =====
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb && string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = "Wpisz login";
                tb.Foreground = System.Windows.Media.Brushes.Gray;
            }

            if (sender is PasswordBox pb && string.IsNullOrWhiteSpace(pb.Password))
            {
                pb.Password = "hasło";
                pb.Foreground = System.Windows.Media.Brushes.Gray;
            }
        }

        // ===== Logowanie =====
        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var user = new User
            {
                Username = UsernameTextBox.Text,
                Password = PasswordBox.Password
            };

            if (!string.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                string? token = await _apiService.Login(user);

                if (!string.IsNullOrEmpty(token))
                {
                    MessageTextBlock.Text = "Zalogowano!";
                    // TODO: zapisz token
                }
                else
                {
                    MessageTextBlock.Text = "Błąd logowania";
                }
            }
        }
    }
}
