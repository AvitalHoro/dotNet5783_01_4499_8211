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
using System.Windows.Shapes;

namespace PL.Order;
/// <summary>
/// Interaction logic for SimulatorOrderTracking.xaml
/// </summary>
public partial class SimulatorOrderTracking : Window
{
    BO.OrderTracking orderTracking;
    public SimulatorOrderTracking(BO.OrderTracking order)
    {
        InitializeComponent();
        orderTracking = order;
        DataContext = orderTracking;    
    }
}
