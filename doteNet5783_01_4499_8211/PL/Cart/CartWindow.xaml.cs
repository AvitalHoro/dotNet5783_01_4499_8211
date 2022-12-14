using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using BlApi;
using BLApi;
using PL.Product;

namespace PL.Cart;
/// <summary>
/// Interaction logic for CartWindow.xaml
/// </summary>
public partial class CartWindow : Window
{
    private IBl bl= BlFactory.GetBl();
    private BO.Cart myCart;
    public CartWindow(BO.Cart cart)
    {
        myCart = cart;
        InitializeComponent();
        Hello.Text = "hello " + cart.CostumerName;
        if (myCart.orderItems!.Count == 0)
        {
            CryBaby.Visibility = Visibility.Visible;
            Approve.IsEnabled = false;
        }
        else
            CryBaby.Visibility = Visibility.Hidden;
        OrderItemView.ItemsSource = myCart.orderItems;
        ProductList.Visibility = Visibility.Hidden;
        LabelAMount.Visibility = Visibility.Hidden;
        Amount.Visibility = Visibility.Hidden;
        ApproveAmount.Visibility = Visibility.Hidden;
        TotalPriceShow.Text = myCart.TotalPrice.ToString();

    }

    private void AddProduct_Click(object sender, RoutedEventArgs e)
    {
        ProductList.Visibility = Visibility.Visible;
        ProductList.ItemsSource = bl.Product.GetProductList();
    }
    private void AddProductToCart(object sender, RoutedEventArgs e)
    {
        BO.ProductForList product = (BO.ProductForList)ProductList.SelectedItem;
        bl.Cart.AddProduct(myCart, product.ID);
        ProductList.Visibility = Visibility.Hidden;
        if (myCart.orderItems!.Count != 0)
        {
            CryBaby.Visibility = Visibility.Hidden;
            Approve.IsEnabled = true;
        }
        OrderItemView.ItemsSource = myCart.orderItems;
        TotalPriceShow.Text = myCart.TotalPrice.ToString();
        Approve.IsEnabled = true;
    }

    private void AmountValidition(object sender, KeyEventArgs e) => Tools.EnterNumbersOnly(sender, e);

    private void UpdateAmount(object sender, RoutedEventArgs e)
    {
        LabelAMount.Visibility = Visibility.Visible;
        Amount.Visibility = Visibility.Visible;
        ApproveAmount.Visibility = Visibility.Visible;
    }

    private void ApproveAmount_Click(object sender, RoutedEventArgs e)
    {
        BO.OrderItem item = (BO.OrderItem)OrderItemView.SelectedItem;
        bl.Cart.UpdateAmountProduct(myCart, item.ProductID, int.Parse(Amount.Text));
        if (myCart.orderItems!.Count == 0)
        {
            CryBaby.Visibility = Visibility.Visible;
            Approve.IsEnabled = false;
        }
        OrderItemView.ItemsSource = myCart.orderItems;
        Amount.Text = string.Empty;
        LabelAMount.Visibility = Visibility.Hidden;
        Amount.Visibility = Visibility.Hidden;
        ApproveAmount.Visibility = Visibility.Hidden;
        TotalPriceShow.Text = myCart.TotalPrice.ToString();
    }

    private void Approve_Click(object sender, RoutedEventArgs e)
    {
        bl.Cart.MakeOrder(myCart);
        MessageBox.Show("הזמנתך אושרה");
        Close();
    }
}
