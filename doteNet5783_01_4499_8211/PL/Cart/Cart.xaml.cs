using BLApi;
using BO;
using MaterialDesignThemes.Wpf;
using PO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    private CartPO myCart = new();
    MainWindow mainWindow;
    public Cart(IBl BL, BO.Cart cartBo , MainWindow _mainWindow)
    {
        InitializeComponent();
        mainWindow = _mainWindow;
        bl = BL;
        BoCartToPoCart(cartBo);
        LeftGrid.DataContext = myCart;
        OrderItemView.DataContext = myCart.OrderItems;
        CartGrid.DataContext = myCart;
        if (myCart.OrderItems.Count() == 0)
        {
            CryBaby.Visibility = Visibility.Visible;
            All.Visibility = Visibility.Visible;
            LeftGrid.Visibility = Visibility.Hidden;
        }
        else
        {
            LeftGrid.Visibility = Visibility.Visible;
            CryBaby.Visibility = Visibility.Hidden;
            All.Visibility = Visibility.Hidden;
        }
    }

    private void BoCartToPoCart(BO.Cart cartBo)
    {
        myCart.OrderItems = new();
        Tools.CopyPropTo(cartBo, myCart);
        Tools.IEnumerableToObservable(myCart.OrderItems, cartBo.OrderItems);
    }

    private void GoBackToCatalog_Click(object sender, RoutedEventArgs e)
    {
        mainWindow.ListCategories_Click(sender, e);
    }

    private void UpdateAmount_Click(object sender, RoutedEventArgs e)
    {
        var b = (Button)sender;
        OrderItem item = (OrderItem)b.DataContext;
       // bl.Cart.UpdateAmountProduct(myCart, item.ProductID, 0);
    }
}
