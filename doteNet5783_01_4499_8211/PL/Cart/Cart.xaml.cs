using BLApi;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL.Cart;

/// <summary>
/// Interaction logic for Cart.xaml
/// </summary>
public partial class Cart : Page
{
    IBl bl;
    private BO.Cart myCart;
    public Cart(IBl BL, BO.Cart cart)
    {
        InitializeComponent();
        bl = BL;    
        myCart= cart;
        if (myCart.orderItems!.Count == 0)
        {
            CryBaby.Visibility = Visibility.Visible;
            ApproveOrder.IsEnabled = false;
            PaymentLabel.Visibility = Visibility.Hidden;    
            LabelAmount.Visibility = Visibility.Hidden;
            AmountInCart.Visibility = Visibility.Hidden;
            TotalPriceShow.Visibility = Visibility.Hidden;
            OrderSummery.Visibility = Visibility.Hidden;
        }
        else
        {
            CryBaby.Visibility = Visibility.Hidden;
            GoToCatalog.Visibility = Visibility.Hidden;
        }
        OrderItemView.ItemsSource = myCart.orderItems;
        PaymentLabel.Visibility = Visibility.Visible;
        LabelAmount.Visibility = Visibility.Visible;
        AmountInCart.Visibility = Visibility.Visible;
        TotalPriceShow.Text = myCart.TotalPrice.ToString();
    }

    private void GoBackToCatalog_Click(object sender, RoutedEventArgs e)
    {

    }
    private void UpdateAmount(object sender, RoutedEventArgs e)
    {

    }
}
