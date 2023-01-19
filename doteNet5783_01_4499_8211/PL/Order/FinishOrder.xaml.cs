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

namespace PL.Order;
/// <summary>
/// Interaction logic for FinishOrder.xaml
/// </summary>
public partial class FinishOrder : Page
{
    IBl bl;
    BO.Order order = new();
    Frame frame;
    public FinishOrder(IBl BL, BO.Order approvedOrder, Frame _frame, int paymentMethod)
    {
        InitializeComponent();
        frame = _frame;
        bl = BL;
        order = approvedOrder;
        DataContext = order;
        switch(paymentMethod)
        {
            case 0:
                pay.Text = "כרטיס אשראי/חיוב";
                break;
                case 1:
                pay.Text = "PayPal שלם עם";
                break;
            case 2:
                pay.Text = "Google Pay";
                break;
        }
    }

    private void trackOrder_Click(object sender, RoutedEventArgs e)
    {
        frame.Content = new PL.Order.OrderTracking
            (bl, Tools.CopyPropTo(bl.Order.GetDetailsOrder(order.ID), new PO.OrderPO()), frame, false);
    }
}
