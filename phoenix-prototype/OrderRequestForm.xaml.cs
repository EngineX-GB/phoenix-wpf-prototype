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
    /// Interaction logic for OrderRequestForm.xaml
    /// </summary>
    public partial class OrderRequestForm : PhoenixThemedWindow
    {
        public OrderRequestForm()
        {
            InitializeComponent();
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

    }
}
