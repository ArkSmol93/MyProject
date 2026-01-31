using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UI.Models;



namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly HttpClient _http = new HttpClient();
        private readonly SyncService _syncService;
        private HubConnection connection;
        public MainWindow()
        {
            InitializeComponent();

            _syncService = new SyncService("https://localhost:/syncHub");
            _syncService.OnMessageReceived += async (msg) =>
            {
                MessageTextBlock.Text = msg;
                await LoadResources();
            };

            StartSignalR();
            ConnectToSignalR();
        }

        private async void StartSignalR()
        {
            await _syncService.StartAsync();
            await LoadResources();
        }

        private async Task LoadResources()
        {
            try
            {
                var response = await _http.GetAsync("https://localhost:5001/api/resources");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var resources = JsonConvert.DeserializeObject<List<Resource>>(json);

                ResourcesListBox.ItemsSource = resources;
            }
            catch
            {
                MessageTextBlock.Text = "Błąd pobierania danych z API";
            }
        }

        private async void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            await LoadResources();
        }
        private async void ConnectToSignalR()
        {
            connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:5001/syncHub")
                .WithAutomaticReconnect()
                .Build();

            connection.On<string>("ReceiveUpdate", (message) =>
            {
                Console.WriteLine("Otrzymano aktualizację: " + message);
                // Tutaj możesz np. aktualizować UI w WPF:
                Dispatcher.Invoke(() => MyTextBox.Text = message);
            });

            try
            {
                await connection.StartAsync();
                Console.WriteLine("Połączono z SignalR Hub");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd połączenia: " + ex.Message);
            }
        }
    }
}
