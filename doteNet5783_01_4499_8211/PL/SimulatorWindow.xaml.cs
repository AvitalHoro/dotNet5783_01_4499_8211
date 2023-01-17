using BLApi;
using BO;
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
using System.Windows.Shapes;
using PO;

namespace PL;
/// <summary>
/// Interaction logic for SimulatorWindow.xaml
/// </summary>
public partial class SimulatorWindow : Window
{
    private IBl bl;
    ObservableCollection<PO.OrderPO> listOrders = new();

    public SimulatorWindow(IBl BL)
    {
        InitializeComponent();
        bl = BL;
        var list = (bl.Order.GetOrderList()).Select(order => Tools.CopyPropTo(order, new PO.OrderPO()));
        Tools.IEnumerableToObservable(listOrders, list);
        DataContext = listOrders;
    }
}
