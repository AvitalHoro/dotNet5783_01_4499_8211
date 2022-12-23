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
    MainWindow mainWindow;
    public Cart(IBl BL, BO.Cart cart, MainWindow _mainWindow)
    {
        mainWindow = _mainWindow;
        InitializeComponent();
        bl = BL;    
        myCart= cart;
        AmountInCart.Text = myCart.OrderItems.Count.ToString();
        OrderItemView.DataContext = myCart.OrderItems;
        CartGrid.DataContext = myCart;
        if (myCart.OrderItems!.Count == 0)
        {
            CryBaby.Visibility = Visibility.Visible;
            All.Visibility = Visibility.Visible;
        }
        else
        {
            CryBaby.Visibility = Visibility.Hidden;
            All.Visibility = Visibility.Hidden;
            PaymentLabel.Visibility = Visibility.Visible;
            LabelAmount.Visibility = Visibility.Visible;
            AmountInCart.Visibility = Visibility.Visible;
        }
        OrderItemView.ItemsSource = myCart.OrderItems;

        TotalPriceShow.Text = myCart.TotalPrice.ToString();
    }

    private void GoBackToCatalog_Click(object sender, RoutedEventArgs e)
    {
        mainWindow.ListCategories_Click(sender, e);
    }
    private void UpdateAmount(object sender, RoutedEventArgs e)
    {

    }
}
