using PL.Product;
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

namespace PL.Cart;
/// <summary>
/// Interaction logic for customerDetails.xaml
/// </summary>
public partial class customerDetails : Window
{
    BO.Cart cart;
    public customerDetails(BO.Cart cart)
    {
        InitializeComponent();
        this.cart = cart;
    }

    private void approve_Click(object sender, RoutedEventArgs e)
    {
        Close();
        cart.CostumerName = AddName.Text;
        cart.CostumerEmail = AddEmail.Text;
        cart.CostumerAdress = AddAdress.Text;
        new CartWindow(cart).Show();
    }
}
