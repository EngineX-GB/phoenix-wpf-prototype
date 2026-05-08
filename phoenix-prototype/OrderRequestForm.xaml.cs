using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using static System.Net.Mime.MediaTypeNames;

namespace phoenix_prototype
{
    public partial class OrderRequestForm : PhoenixThemedWindow, INotifyPropertyChanged
    {
        private SearchEntry searchEntry;

        public event PropertyChangedEventHandler PropertyChanged;
        private void Notify(string propName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        private int _marketPrice;
        public int MarketPrice
        {
            get => _marketPrice;
            set
            {
                _marketPrice = value;
                Notify(nameof(MarketPrice));
            }
        }

        public OrderRequestForm(SearchEntry searchEntry)
        {
            InitializeComponent();
            DataContext = this;   // REQUIRED for binding to work
            this.searchEntry = searchEntry;
            PopulateOrderDetails(searchEntry);
        }

        private void ProposedOrderMode_Click(object sender, RoutedEventArgs e)
        {
            ProposedMode.Background = Brushes.ForestGreen;
            CommittedMode.Background = (Brush)new BrushConverter().ConvertFromString("#333");
        }

        private void CommittedOrderMode_Click(object sender, RoutedEventArgs e)
        {
            CommittedMode.Background = Brushes.ForestGreen;
            ProposedMode.Background = (Brush)new BrushConverter().ConvertFromString("#333");
        }

        private void PopulateOrderDetails(SearchEntry searchEntry)
        {
            if (searchEntry == null)
            {
                MessageBox.Show("Select an asset first", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            TxtUserID.Text = searchEntry.UserId;
            TxtUsername.Text = searchEntry.Username;
            TxtLocation.Text = searchEntry.Location;

            TxtTradeDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            TxtTradeTime.Text = "00:00:00";
        }

        private void AdjustQuantityAndPrice(object sender, RoutedEventArgs e)
        {
            if (searchEntry == null)
                return;

            string qty = TxtQuantity.Text?.Trim();

            if (qty == "1")
            {
                if (searchEntry.R100 > 0)
                    MarketPrice = IncludeSurplusAndDeductions(TxtSurplus.Text, TxtDeductions.Text, searchEntry.R100);
            }
            else if (qty == "1.5")
            {
                if (searchEntry.R150 > 0)
                    MarketPrice = IncludeSurplusAndDeductions(TxtSurplus.Text, TxtDeductions.Text, searchEntry.R150);
            }
            else if (qty == "2")
            {
                if (searchEntry.R200 > 0)
                    MarketPrice = IncludeSurplusAndDeductions(TxtSurplus.Text, TxtDeductions.Text, searchEntry.R200);
            }
            else
            {
                MarketPrice = 0;
            }
        }

        private int IncludeSurplusAndDeductions(string surplusText, string deductionsText, int marketPrice)
        {
            int surplus = surplusText != null && !surplusText.Equals("") ? int.Parse(surplusText) : 0;
            int deductions = deductionsText != null && !deductionsText.Equals("") ? int.Parse(deductionsText) : 0;
            return marketPrice + surplus - deductions;
        }
    }
}