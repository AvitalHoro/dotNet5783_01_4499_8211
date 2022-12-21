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
using BlApi;
using BLApi;
using BO;
using MaterialDesignThemes.Wpf;
using PL.Cart;

namespace PL;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private IBl bl = BlFactory.GetBl();
    private BO.Cart cart = new();

    public MainWindow() //החלון הראשי של החנות
    {
        InitializeComponent();
        framePage.Content = new MainPagePicture();
        cart.orderItems = new();
        ListCategories.Visibility = Visibility.Collapsed;
       
    }

    private void SelectAdmin_Click(object sender, RoutedEventArgs e)
    {
        framePage.Content = new AdminPage(bl);
    }

    private void OpenCategories_Click(object sender, RoutedEventArgs e)
    {
            ListCategories.Visibility = Visibility.Visible;
    }

    private void showCategory(object sender, RoutedEventArgs e)
    {
        ListCategories.Visibility = Visibility.Visible;
    }

    private void hideCategory(object sender, RoutedEventArgs e)
    {
        ListCategories.Visibility = Visibility.Hidden;
    }

    private void showCartDetails(object sender, RoutedEventArgs e)
    {
        framePage.Content = new Cart.Cart(bl, cart);
    }

}
