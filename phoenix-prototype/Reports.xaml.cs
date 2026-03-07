using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;


namespace phoenix_prototype
{
    /// <summary>
    /// Interaction logic for Reports.xaml
    /// </summary>
    public partial class Reports : Window
    {
        public ObservableCollection<FeedbackEntry> FeedbackEntries { get; set; } = new ObservableCollection<FeedbackEntry>();
        public ObservableCollection<ServiceReportHeadlineEntry> ServiceReportHeadlineEntries{ get; set; } = new ObservableCollection<ServiceReportHeadlineEntry>();

        public Reports()
        {
            InitializeComponent();
            DataContext = this;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {

            //var orders = new Orders();
            //orders.Owner = this; //this means that the owner of the Orders window is "Watchlist". 
            //// add this snippet to the orders.xaml code:
            //// ShowInTaskbar="False"
            //orders.Show();

            //await LoadWatchlistAsync();


        }

        private void CloseButton_Click(object sender, RoutedEventArgs e) { this.Close(); }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e) { if (e.ChangedButton == MouseButton.Left) this.DragMove(); }

        /* Program the search button*/
        private async void SearchButton_Click(object sender, RoutedEventArgs e) { 
            if (UserID.Text != "")
            {
                await LoadFeedbackAsync(UserID.Text, null);
                await LoadServiceReportsAsync(UserID.Text);
            }
        }

        private async void SearchField_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (UserID.Text != "")
                {
                    await LoadFeedbackAsync(UserID.Text, null);
                }
            }
        }

        public async void NavigateNextPageResults_Click(object sender, RoutedEventArgs e)
        {
            await LoadFeedbackAsync(UserID.Text, "FORWARD");
        }

        public async void NavigateToFirstPageResults_Click(object sender, RoutedEventArgs e)
        {
            await LoadFeedbackAsync(UserID.Text, null);
        }

        /**
         * TODO: 2026-03-07: This needs to be further investigated as it doesn't appear to work correctly.
         */
        public async void NavigatePreviousPageResults_Click(object sender, RoutedEventArgs e)
        {
            await LoadFeedbackAsync(UserID.Text, "BACKWARD");
        }


        public async Task LoadFeedbackAsync(string userId, string direction)
        {
            string url = null;
            using var client = new HttpClient();

            if (direction == null)
            {
                url = "http://localhost:8081/feedback?userId=" + userId;
            }
            else if (direction == "FORWARD")
            {
                var feedbackEntry = FeedbackEntries[^1];    // get the last entry in the collection that is currently viewing (before getting the next batch)
                int offsetId = feedbackEntry.Oid;
                url = "http://localhost:8081/feedback?userId=" + userId + "&pageDirection=" + direction + "&offset=" + offsetId.ToString();
            }
            else
            {
                var feedbackEntry = FeedbackEntries.First();
                int offsetId = feedbackEntry.Oid;
                url = "http://localhost:8081/feedback?userId=" + userId + "&pageDirection=" + direction + "&offset=" + offsetId.ToString();
            }

            var response = await client.GetAsync(url);
            Console.WriteLine(response.Content);

            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();

            var items = JsonSerializer.Deserialize<FeedbackResponse>(json);

            FeedbackEntries.Clear();
            foreach (var item in items.entries)
                FeedbackEntries.Add(item);
        }

        public async Task LoadServiceReportsAsync(string userId)
        {
            string url = null;
            using var client = new HttpClient();
            url = "http://localhost:8081/servicereports?userId=" + userId;
            var response = await client.GetAsync(url);
            Console.WriteLine(response.Content);
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();
            var items = JsonSerializer.Deserialize<List<ServiceReportHeadlineEntry>>(json);

            ServiceReportHeadlineEntries.Clear();
            foreach (var item in items)
                ServiceReportHeadlineEntries.Add(item);
        }

    }
}
