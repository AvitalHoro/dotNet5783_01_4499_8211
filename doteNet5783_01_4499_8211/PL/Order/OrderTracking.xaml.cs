using BLApi;
using PL.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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
using PO;

namespace PL.Order;

/// <summary>
/// Interaction logic for OrderTracking.xaml
/// </summary>
public partial class OrderTracking : Page
{
    IBl bl;
    PO.OrderPO order = new();
    Frame frame;
    bool isAdminPage;


    public OrderTracking(IBl BL, PO.OrderPO selectedOrder, Frame frame, bool isAdmin)
    {
        InitializeComponent();
        bl = BL;
        order = selectedOrder;    
        order.Items = selectedOrder.Items;
        DataContext = order;
        //if(!isAdmin)
        //{
        //    AdminButton.Visibility = Visibility.Hidden;
        //    UpdateShip.Visibility = Visibility.Hidden;
        //}
        OrderItemView.ItemsSource = order.Items;
        this.frame = frame;
        isAdminPage = isAdmin;
    }

    private void UpdateShip_Click(object sender, RoutedEventArgs e)
    {
         bl.Order.UpdateShipDate(order.ID);
         Tools.CopyPropTo(bl.Order.GetDetailsOrder(order.ID), order);
    }

    private void UpdateDel_Click(object sender, RoutedEventArgs e)
    {
        if (order.State == BO.Status.sent)
        {
            bl.Order.UpdateDeliveryDate(order.ID);
            Tools.CopyPropTo(bl.Order.GetDetailsOrder(order.ID), order);
        }
    }

    private void ReturnBack_Click(object sender, RoutedEventArgs e)
    {
        if (isAdminPage)
            frame.Content = new AdminPage(bl, frame);
        else
            frame.Content = new MainPagePicture();
    }
}
