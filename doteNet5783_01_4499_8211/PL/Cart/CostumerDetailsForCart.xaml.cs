using BLApi;
using BO;
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
/// Interaction logic for CostumerDetailsForCart.xaml
/// </summary>
public partial class CostumerDetailsForCart : Window
{
    IBl bl;
    BO.Cart cart;
    public CostumerDetailsForCart(BO.Cart cart, IBl BL)
    {
        InitializeComponent();
        this.cart = cart;
        this.bl = BL;
    }

    private void approve_Click(object sender, RoutedEventArgs e)
    {
        Close();
        cart.CostumerName = AddName.Text;
        cart.CostumerEmail = AddEmail.Text;
        cart.CostumerAdress = AddAddress.Text;

        //MainWindow.framePage.Content = new Cart(bl, cart);
        //new MainWindow(cart, bl).showCart();
    }
}
