using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;
using System.Text.Json;

namespace phoenix_prototype
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {


            var workArea = SystemParameters.WorkArea; 
            this.Left = workArea.Left; 
            this.Top = workArea.Top; 
            this.Width = workArea.Width; 
            this.Height = workArea.Height;

            var orders = new Orders();
            orders.Owner = this; //this means that the owner of the Orders window is "Watchlist". 
            // add this snippet to the orders.xaml code:
            // ShowInTaskbar="False"
            orders.Show();

            var watchlist = new Watchlist();
            watchlist.Owner = this;
            watchlist.Show();


        }

        private void CloseButton_Click(object sender, RoutedEventArgs e) { this.Close(); }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e) { if (e.ChangedButton == MouseButton.Left) this.DragMove(); }


    }
}