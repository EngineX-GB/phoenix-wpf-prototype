using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public search()
        {
            InitializeComponent();
        }

        // Make the REST call when the window opens
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {

            //var orders = new Orders();
            //orders.Owner = this; //this means that the owner of the Orders window is "Watchlist". 
            //// add this snippet to the orders.xaml code:
            //// ShowInTaskbar="False"
            //orders.Show();



        }

        private void CloseButton_Click(object sender, RoutedEventArgs e) { this.Close(); }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e) { if (e.ChangedButton == MouseButton.Left) this.DragMove(); }

        /* Program the refresh button*/
        private async void RefreshButton_Click(object sender, RoutedEventArgs e) { }


    }
}
