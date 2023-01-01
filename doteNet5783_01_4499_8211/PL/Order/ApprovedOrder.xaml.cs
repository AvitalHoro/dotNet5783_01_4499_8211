using BLApi;
using PO;
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
/// Interaction logic for ApprovedOrder.xaml
/// </summary>
public partial class ApprovedOrder : Page
{
    private CartPO cart;
    private IBl bl;
    public ApprovedOrder(IBl _bl, CartPO _cart)
    {
        InitializeComponent();
        bl = _bl;
        cart = _cart;
        DataContext = cart;    
        
    }
}
