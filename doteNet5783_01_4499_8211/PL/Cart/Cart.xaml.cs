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
    private int ItemsAmount = 0;
    MainWindow mainWindow;
    public Cart(IBl BL, BO.Cart cartBo , MainWindow _mainWindow)
    {
        InitializeComponent();
        mainWindow = _mainWindow;
        bl = BL;
        Tools.BoCartToPoCart(myCart, cartBo);
        DataContext= myCart;
        ItemsAmount = myCart.OrderItems.Count();
        LeftGrid.DataContext = ItemsAmount;
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

    private void GoBackToCatalog_Click(object sender, RoutedEventArgs e)
    {
        mainWindow.ListCategories_Click(sender, e);
    }

    private void UpdateAmount_Click(object sender, RoutedEventArgs e)
    {
        BO.Cart cartBo= new BO.Cart();  
        Tools.PoCartToBoCart(myCart, cartBo);
        var b = (Button)sender;
        OrderItem item = (OrderItem)b.DataContext;
        bl.Cart.UpdateAmountProduct(cartBo, item.ProductID, 0);
        Tools.BoCartToPoCart(myCart, cartBo);
    }
}
