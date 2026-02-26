using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace phoenix_prototype
{
    /// <summary>
    /// Interaction logic for Watchlist.xaml
    /// </summary>
    public partial class Watchlist : Window
    {
        public ObservableCollection<WatchlistEntry> WatchlistCollection { get; set; } = new ObservableCollection<WatchlistEntry>();
        public Watchlist()
        {
            InitializeComponent();

            DataContext = this;

            //var dummyData = new List<WatchlistEntry>
            //{
            //    new WatchlistEntry { UserId = "1", Username = "Alice", Nationality = "British", Telephone = "555-1234", Rate = 120, Location = "London" },
            //    new WatchlistEntry { UserId = "2",  Username = "Bob", Nationality = "British", Telephone = "555-5678", Rate = 95, Location = "Manchester" },
            //    new WatchlistEntry { UserId = "3",  Username = "Charlie", Nationality = "British",  Telephone = "555-9012", Rate = 150, Location = "Birmingham" },
            //    new WatchlistEntry { UserId = "4",  Username = "Diana", Nationality = "British", Telephone = "555-3456", Rate = 200, Location = "Liverpool" }
            //};

            //DataGridWatchlist.ItemsSource = dummyData;
        }


        public async Task LoadWatchlistAsync()
        {
            using var client = new HttpClient();

            // Replace with your real API endpoint
            string url = "http://localhost:8081/watchlist/today";

            var response = await client.GetAsync(url);
            Console.WriteLine(response.Content);

            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            Console.WriteLine("JSON RECEIVED: " + json); // <--- IMPORTANT

            var items = JsonSerializer.Deserialize<List<WatchlistEntry>>(json);

            WatchlistCollection.Clear();
            foreach (var item in items)
                WatchlistCollection.Add(item);
        }

        // Make the REST call when the window opens
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {

            //var orders = new Orders();
            //orders.Owner = this; //this means that the owner of the Orders window is "Watchlist". 
            //// add this snippet to the orders.xaml code:
            //// ShowInTaskbar="False"
            //orders.Show();

            await LoadWatchlistAsync();


        }

        private void CloseButton_Click(object sender, RoutedEventArgs e) { this.Close(); }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e) { if (e.ChangedButton == MouseButton.Left) this.DragMove(); }

        /* Program the refresh button*/
        private async void RefreshButton_Click(object sender, RoutedEventArgs e) { await LoadWatchlistAsync(); }




    }
}
