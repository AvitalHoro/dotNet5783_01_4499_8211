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
using PL.Product;

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
        cart.OrderItems = new();
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
        framePage.Content = new Cart.Cart(bl, cart, this);
    }

    public void ListCategories_Click(object sender, RoutedEventArgs e)   
    {
        framePage.Content = new ProductCatalogForCostumer(bl, ((Button)sender).Name, cart);
    }

    private void showHomePage(object sender, RoutedEventArgs e)
    {
        framePage.Content = new MainPagePicture();
    }

    private void search(object sender, RoutedEventArgs e)
    {
        if(EnterStringToSearch.Text!=null)
            framePage.Content = new ProductCatalogForCostumer(bl, EnterStringToSearch.Text, cart);
    }
}
