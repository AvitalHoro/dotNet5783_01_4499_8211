﻿using BLApi;
using BO;
using MaterialDesignThemes.Wpf;
using PL.Order;
using PO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    private BO.Cart cartBo = new();
    private int ItemsAmount = 0;
    MainWindow mainWindow;
    public Cart(IBl BL, BO.Cart cart , MainWindow _mainWindow)
    {
        InitializeComponent();
        mainWindow = _mainWindow;
        bl = BL;
        cartBo = cart;
        Tools.BoCartToPoCart(myCart, cart);
        DataContext= myCart;
        ItemsAmount = myCart.OrderItems.Count();
        IsCartEmpty();
    }

    private void GoBackToCatalog_Click(object sender, RoutedEventArgs e)
    {
        mainWindow.ListCategories_Click(sender, e);
    }

    private void UpdateAmount_Click(object sender, RoutedEventArgs e)
    { 
       // Tools.PoCartToBoCart(myCart, cartBo);
        var b = (Button)sender;
        OrderItem item = (OrderItem)b.DataContext;
        bl.Cart.UpdateAmountProduct(cartBo, item.ProductID, 0);
        Tools.BoCartToPoCart(myCart, cartBo);
        IsCartEmpty();
    }

    private void IsCartEmpty()
    {
        if(myCart.OrderItems.Count() == 0)
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

    private void ApproveOrder_Click(object sender, RoutedEventArgs e)
    {
        mainWindow.framePage.Content = new ApprovedOrder(bl, myCart);
    }

    //public event PropertyChangedEventHandler PropertyChanged;

    //private float? value;
    //public float? Value
    //{
    //    get { return value; }
    //    set
    //    {
    //        this.value = value;
    //        if (PropertyChanged != null)
    //        {
    //            PropertyChanged(this, new PropertyChangedEventArgs("CList"));
    //        }
    //    }
    //}

    //private float? maxValue;
    //public float? MaxValue
    //{
    //    get { return value; }
    //    set
    //    {
    //        maxValue = value;
    //        if (PropertyChanged != null)
    //        {
    //            PropertyChanged(this, new PropertyChangedEventArgs("CList"));
    //        }
    //    }
    //}

    //private void txtNum_TextChanged(object sender, TextChangedEventArgs e)
    //{
    //    var textNumber = (TextBox)sender;
    //    MaxValue = bl.Product.GetProductDetails(((OrderItem)textNumber.DataContext).ProductID).InStock;
    //    if (textNumber == null || textNumber.Text == "" || textNumber.Text == "-")
    //    {
    //        Value = null;
    //        return;
    //    }

    //    float val;
    //    if (!float.TryParse(textNumber.Text, out val))
    //        textNumber.Text = Value.ToString();
    //    else
    //    {
    //        if(val> MaxValue)
    //            Value= (float?)MaxValue;
    //        else if (value < 1)
    //            Value = 1;
    //        else Value = val;
    //    }    
    //}

    //private void cmdUp_Click(object sender, RoutedEventArgs e)
    //{
    //    var b = (Button)sender;
    //    MaxValue = bl.Product.GetProductDetails(((OrderItem)b.DataContext).ProductID).InStock;
    //    if (Value< MaxValue)
    //        Value++;
    //}

    //private void cmdDown_Click(object sender, RoutedEventArgs e)
    //{
    //    var b = (Button)sender;
    //    MaxValue = bl.Product.GetProductDetails(((OrderItem)b.DataContext).ProductID).InStock;
    //    if (Value > 1)
    //        Value--;
    //}
}
