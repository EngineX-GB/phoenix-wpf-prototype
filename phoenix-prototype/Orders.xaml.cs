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
    /// Interaction logic for Orders.xaml
    /// </summary>
    public partial class Orders : Window
    {

        public ObservableCollection<OrderEntry> orderEntries { get; set; } = new ObservableCollection<OrderEntry>();

        public Orders()
        {
            InitializeComponent();
            DataContext = this;
            //var dummyOrders = new List<OrderEntry>
            //{
            //    new OrderEntry() { Id = 1, Username = "Charlotte", Region = "UK", Rate = 100, DateOfEvent = new DateOnly(2026,1,1), TimeOfEvent = new TimeSpan(9,0,0), Status = "PROPOSED" }
            //};

            //DataGridOrders.ItemsSource = dummyOrders;
        }

        public async Task LoadAllOrdersAsync()
        {
            using var client = new HttpClient();

            // Replace with your real API endpoint
            string url = "http://localhost:8081/orders/all";

            var response = await client.GetAsync(url);
            Console.WriteLine(response.Content);

            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();

            var items = JsonSerializer.Deserialize<List<OrderEntry>>(json);

            orderEntries.Clear();
            foreach (var item in items)
                orderEntries.Add(item);
        }

        public bool CancelOrder(string orderId)
        {
            using var client = new HttpClient();
            string url = "http://localhost:8081/orders/cancel?id=" + orderId;
            var request = new HttpRequestMessage(HttpMethod.Put, url);
            var response = client.Send(request);
            return response.IsSuccessStatusCode;
        }


        // Make the REST call when the window opens
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        { 
            await LoadAllOrdersAsync();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e) { this.Close(); }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e) { if (e.ChangedButton == MouseButton.Left) this.DragMove(); }

        private async void RefreshButton_Click(object sender, RoutedEventArgs e) { await LoadAllOrdersAsync(); }

        private void CancelOrder_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridOrders.SelectedItem is OrderEntry selectedOrder)
            {
                Console.WriteLine($"Cancelling order for user: {selectedOrder.Id}");
                bool cancelOrderResult = CancelOrder(selectedOrder.Id.ToString());
                if (cancelOrderResult)
                {
                    selectedOrder.Status = "CANCELLED";
                }
                else
                {
                    Console.WriteLine("No row selected.");
                }
            }
        }


    }
}
