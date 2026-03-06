using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace phoenix_prototype
{
    /// <summary>
    /// Interaction logic for Watchlist.xaml
    /// </summary>
    public partial class Watchlist : Window
    {

        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(
            IntPtr hwnd,
            int attr,
            ref int attrValue,
            int attrSize);

        private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;
        private const int DWMWA_BORDER_COLOR = 34;

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            var hwnd = new WindowInteropHelper(this).Handle;

            // Enable dark mode frame (prevents white border)
            int dark = 1;
            DwmSetWindowAttribute(hwnd, DWMWA_USE_IMMERSIVE_DARK_MODE, ref dark, sizeof(int));

            // Set border colour (ARGB)
            int borderColor = unchecked((int)0xFF1A1A1A); // match your window background
            DwmSetWindowAttribute(hwnd, DWMWA_BORDER_COLOR, ref borderColor, sizeof(int));
        }

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

        private async void ImportWatchlist_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Title = "Select file to import",
                Filter = "All files (*.*)|*.*"
            };

            if (dialog.ShowDialog() == true)
            {
                string filePath = dialog.FileName;

                try
                {
                    await UploadFileAsync(filePath);
                    await LoadWatchlistAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Upload failed: " + ex.Message);
                }
            }
        }

        private async Task UploadFileAsync(string filePath)
        {
            using var client = new HttpClient();
            using var form = new MultipartFormDataContent();

            var fileBytes = await File.ReadAllBytesAsync(filePath);
            var fileContent = new ByteArrayContent(fileBytes);

            // IMPORTANT: "myFile" must match your server's expected field name
            form.Add(fileContent, "file", System.IO.Path.GetFileName(filePath));

            var response = await client.PostAsync("http://localhost:8081/watchlist/import", form);
            response.EnsureSuccessStatusCode();
        }




    }
}
