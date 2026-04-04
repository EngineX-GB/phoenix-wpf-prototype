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
using System.Diagnostics;
using System.CodeDom;

namespace phoenix_prototype
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Window windowWatchlist;
        private Window windowOrders;
        private Window windowNews;
        private Window windowSearch;

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

            windowSearch = new search();
            windowSearch.Owner = this;
            windowSearch.Show();
        }

        private void WindowAdjustTestButton_Click(object sender, RoutedEventArgs e)
        {
            setHardDimensionsOnWindow(windowWatchlist, 425.6, 396, -0.8, 76);
            setHardDimensionsOnWindow(windowOrders, 326.40000000000003, 397.6, 0, 498.40000000000003);
            setHardDimensionsOnWindow(windowSearch, 450.40000000000003, 627.2, 388.8, 73.60000000000001);
            setHardDimensionsOnWindow(windowNews, 305.6, 627.2, 389.6, 520);
        }

        private void WindowStatsButton_Click(object sender, RoutedEventArgs e)
        {

            Debug.WriteLine("Full Screen Width: " + SystemParameters.PrimaryScreenWidth);
            Debug.WriteLine("Full Screen Height: " + SystemParameters.PrimaryScreenHeight);


            // work area excluding the task bar:

            Rect workArea = SystemParameters.WorkArea;

            double usableWidth = workArea.Width;
            double usableHeight = workArea.Height;
            double usableLeft = workArea.Left;
            double usableTop = workArea.Top;

            readWindowStats(windowSearch, "SEARCH");
            readWindowStats(windowOrders, "ORDERS");
            readWindowStats(windowNews, "NEWS");
            readWindowStats(windowWatchlist, "WATCHLIST");

        }



        private void readWindowStats (Window window, string windowName)
        {
            if (window != null)
            {
                Debug.WriteLine("DEBUG>> [" + windowName + "_WINDOW] Height is " + window.Height);
                Debug.WriteLine("DEBUG>> [" + windowName+ "] Width is " + window.Width);
                Debug.WriteLine("DEBUG>> [" + windowName + "] X point coordinate: " + window.Left);
                Debug.WriteLine("DEBUG>> [" + windowName+  "] Y point coordinate: " + window.Top);

            }
        }

        private void setHardDimensionsOnWindow(Window window, double height, double width, double pointX, double pointY)
        {
            if (window != null)
            {
                window.Height = height;
                window.Width = width;
                window.Left = pointX;
                window.Top = pointY;
            }
        }


        private void CloseButton_Click(object sender, RoutedEventArgs e) { this.Close(); }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e) { if (e.ChangedButton == MouseButton.Left) this.DragMove(); }

        private void Watchlist_Click(object sender, RoutedEventArgs e)
        {
            windowWatchlist = new Watchlist();   // Watchlist.xaml → class Watchlist
            windowWatchlist.Owner = this;            // Optional: keeps it tied to main window
            windowWatchlist.Show();                  // or ShowDialog() if you want modal
        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            windowOrders = new Orders();
            windowOrders.Owner = this;
            windowOrders.Show();
        }

        private void News_Click(object sender, RoutedEventArgs e)
        {
            windowNews = new Reports();
            windowNews.Owner = this;
            windowNews.Show();
        }
        private void MinimiseButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }


    }
}