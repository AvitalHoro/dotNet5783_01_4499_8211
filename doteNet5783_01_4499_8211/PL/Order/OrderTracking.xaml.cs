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

namespace PL.Order;

/// <summary>
/// Interaction logic for OrderTracking.xaml
/// </summary>
public partial class OrderTracking : Page
{
    IBl bl;
    BO.Order order = new();
    Frame frame;

    public OrderTracking(IBl BL, BO.Order selectedOrder, Frame frame, bool isAdmin)
    {
        InitializeComponent();
        bl = BL;
        order = selectedOrder;
        DataContext = order;
        if (isAdmin&& bl.Order.Tracking(order.ID).State == BO.Status.approved)
            AdminButton.Visibility = Visibility.Visible;    
        else if(isAdmin && bl.Order.Tracking(order.ID).State == BO.Status.sent)
        {
            AdminButton.Visibility = Visibility.Visible;
            UpdateShip.Visibility=Visibility.Hidden;    
        }
        else
            AdminButton.Visibility = Visibility.Hidden;
        if (bl.Order.Tracking(order.ID).State == BO.Status.approved)
            approved.Visibility = Visibility.Visible;
        if (bl.Order.Tracking(order.ID).State == BO.Status.sent)
        {
            approved.Visibility = Visibility.Visible;
            shipped.Visibility = Visibility.Visible;
        }
        if (bl.Order.Tracking(order.ID).State == BO.Status.delivered)
        {
            approved.Visibility = Visibility.Visible;
            shipped.Visibility = Visibility.Visible;
            delevired.Visibility = Visibility.Visible;
        }
        OrderItemView.ItemsSource = order.Items;
        this.frame = frame; 
    }

    private void UpdateShip_Click(object sender, RoutedEventArgs e)
    {
        if (bl.Order.Tracking(order.ID).State == BO.Status.approved)
            bl.Order.UpdateShipDate(order.ID);
        if (bl.Order.Tracking(order.ID).State == BO.Status.sent)
        {
            approved.Visibility = Visibility.Visible;
            shipped.Visibility = Visibility.Visible;
            ShipDate.Visibility=Visibility.Visible;
           ShipDate.Text = order.ShipDate.ToString();//הביידיניג לא מתעדכן אוטומטית למרבה הצער..
        }
    }

    private void UpdateDel_Click(object sender, RoutedEventArgs e)
    {
        if (bl.Order.Tracking(order.ID).State == BO.Status.sent)
            bl.Order.UpdateDeliveryDate(order.ID);
        if (bl.Order.Tracking(order.ID).State == BO.Status.delivered)
        {
            approved.Visibility = Visibility.Visible;
            shipped.Visibility = Visibility.Visible;
            delevired.Visibility = Visibility.Visible;
            DelDate.Visibility = Visibility.Visible;
           DelDate.Text = order.DeliveryDate.ToString();
        }
    }
}
