﻿using BLApi;
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
        }
        else
        {
            CryBaby.Visibility = Visibility.Hidden;
            GoToCatalog.Visibility = Visibility.Hidden;
            PaymentLabel.Visibility = Visibility.Visible;
            LabelAmount.Visibility = Visibility.Visible;
            AmountInCart.Visibility = Visibility.Visible;
        }
        OrderItemView.ItemsSource = myCart.orderItems;

        TotalPriceShow.Text = myCart.TotalPrice.ToString();
    }

    private void GoBackToCatalog_Click(object sender, RoutedEventArgs e)
    {

    }
    private void UpdateAmount(object sender, RoutedEventArgs e)
    {

    }
}
