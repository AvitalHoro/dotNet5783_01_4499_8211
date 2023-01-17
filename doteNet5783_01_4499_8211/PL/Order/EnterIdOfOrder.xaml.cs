using BLApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
/// Interaction logic for EnterIdOfOrder.xaml
/// </summary>
public partial class EnterIdOfOrder : Page
{
    IBl bl;
    Frame frame;

    public EnterIdOfOrder(IBl BL, Frame frameHome)
    {
        InitializeComponent();
        bl = BL;
        frame = frameHome;
    }
    private void Aprrove_Click(object sender, RoutedEventArgs e)
    {
        EnterPassword();
    }

    private void EnterPressed_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter) EnterPassword();
    }

  
    private void EnterPassword()
    {
        try
        {
            int idOrder = int.Parse(OrderIdBox.Text);
            if (OrderIdBox.Text != "")
            {
                PO.OrderPO orderPo = Tools.CopyPropTo(bl.Order.GetDetailsOrder(idOrder), new PO.OrderPO());
                frame.Content = new PL.Order.OrderTracking(bl, orderPo, frame, false);
            }
        } 
        catch
        {
            MessageBox.Show("מספר הזמנה זה לא קיים במערכת");
        } 
    }
}

