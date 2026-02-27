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
    /// Interaction logic for search.xaml
    /// </summary>
    public partial class search : Window
    {

        public ObservableCollection<SearchEntry> searchEntries { get; set; } = new ObservableCollection<SearchEntry>();
        public search()
        {
            InitializeComponent();
            DataContext = this;
        }

        // Make the REST call when the window opens
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadAllOrdersAsync();

        }


        public async Task LoadAllOrdersAsync()
        {
            using var client = new HttpClient();

            // Replace with your real API endpoint
            string url = "http://localhost:8081/search?nationality=Romanian";

            var response = await client.GetAsync(url);
            Console.WriteLine(response.Content);

            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();

            var items = JsonSerializer.Deserialize<List<SearchEntry>>(json);

            searchEntries.Clear();
            foreach (var item in items)
                searchEntries.Add(item);
        }




        private void CloseButton_Click(object sender, RoutedEventArgs e) { this.Close(); }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e) { if (e.ChangedButton == MouseButton.Left) this.DragMove(); }

        /* Program the refresh button*/
        private async void RefreshButton_Click(object sender, RoutedEventArgs e) { }


    }
}
