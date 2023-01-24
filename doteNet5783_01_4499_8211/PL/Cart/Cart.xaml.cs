using BLApi;
using BO;
using MaterialDesignThemes.Wpf;
using PL.Order;
using PO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
    private BO.Cart cartBo = new();
    MainWindow mainWindow;
    public Cart(IBl BL, BO.Cart cart, MainWindow _mainWindow)
    {
        InitializeComponent();
        mainWindow = _mainWindow;
        bl = BL;
        cartBo = cart;
        Tools.BoCartToPoCart(myCart, cart);
        DataContext = myCart;
    }

    private void GoBackToCatalog_Click(object sender, RoutedEventArgs e)
    {
        mainWindow.framePage.Content = new Product.ProductCatalogForCostumer(bl, "All", cartBo, mainWindow.framePage);
    }

    private void UpdateAmount_Click(object sender, RoutedEventArgs e)
    {
        UpdateAmount(sender, 0);
    }

    private void UpdateAmount(object sender, int amount, bool isTextBox = false)
    {
        try
        {
            PO.OrderItemPO item = new();
            TextBox t;
            Button b;
            if (isTextBox)
            {
                t = (TextBox)sender;
                item = (PO.OrderItemPO)t.DataContext;
            }
            else
            {
                b = (Button)sender;
                item = (PO.OrderItemPO)b.DataContext;
            }
            bl.Cart.UpdateAmountProduct(cartBo, item.ProductID, amount);
            item = myCart.OrderItems.FirstOrDefault(x => x.ID == item.ID);
            myCart.TotalPrice = cartBo.TotalPrice;
            if (amount == 0)
            {
                myCart.OrderItems.Remove(item);
                myCart.TotalPrice = cartBo.TotalPrice;
                return;
            }
            item.Amount = amount;
        }
        catch (BO.OutOfStockException ex)
        {
            MessageBox.Show("אין עוד מהמוצר הזה במלאי" +
               "", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            return;
        }
    }

    private void ApproveOrder_Click(object sender, RoutedEventArgs e)
    {
        mainWindow.fullFrame.Content = new ApprovedOrder(bl, myCart, mainWindow);
    }

    private void cmdDown_Click(object sender, RoutedEventArgs e)
    {
        var b = (Button)sender;
        int amount = ((PO.OrderItemPO)b.DataContext).Amount;
        if (amount == 1)
            return;
        UpdateAmount(sender, amount - 1);
    }

    private void EntersOnlyNumbers(object sender, KeyEventArgs e) => Tools.EnterNumbersOnly(sender, e);

    private void txtNum_TextChanged(object sender, TextChangedEventArgs e)
    {
        var t = (TextBox)sender;
        if (t.Text == "")
            return;
        int amount = int.Parse(t.Text);
        if (amount == 0)
            amount = 1;
        UpdateAmount(sender,amount , true);
    }

    private void cmdUp_Click(object sender, RoutedEventArgs e)
    {
        var b = (Button)sender;
        int amount = ((PO.OrderItemPO)b.DataContext).Amount;
        UpdateAmount(sender, amount + 1);
    }

    private void DeleteCart_Click(object sender, RoutedEventArgs e)
    {
        mainWindow.cart = new();
        mainWindow.cart.OrderItems = new();
        myCart.OrderItems = new();
        myCart = new();
    }
}