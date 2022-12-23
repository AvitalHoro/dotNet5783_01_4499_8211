using BLApi;
using BO;
using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL.Product;

/// <summary>
/// Interaction logic for ProductCatalogForCostumer.xaml
/// </summary>
public partial class ProductCatalogForCostumer : Page
{
    IBl bl;
    BO.Cart cart;
    public ProductCatalogForCostumer(IBl BL, string ButtonName, BO.Cart cart)
    {
        InitializeComponent();
        bl = BL;
        this.cart = cart;
        switch (ButtonName)
        {
            case "Toys":
                ProductListview.ItemsSource = bl.Product.GetProductList(BO.Filters.filterByCategory, BO.Category.Toys);
                break;
            case "Carts":
                ProductListview.ItemsSource = bl.Product.GetProductList(BO.Filters.filterByCategory, BO.Category.Carts);
                break;
            case "Clothes":
                ProductListview.ItemsSource = bl.Product.GetProductList(BO.Filters.filterByCategory, BO.Category.Clothes);
                break;
            case "Diapers":
                ProductListview.ItemsSource = bl.Product.GetProductList(BO.Filters.filterByCategory, BO.Category.Diapers);
                break;
            case "Bottles":
                ProductListview.ItemsSource = bl.Product.GetProductList(BO.Filters.filterByCategory, BO.Category.Bottles);
                break;
            case "All":
            case "GoBackToCatalog":
                ProductListview.ItemsSource = bl.Product.GetProductList();
                break;
            default:
                ProductListview.ItemsSource = bl.Product.GetProductList(BO.Filters.filterByName, ButtonName);
                break;
        }
    }

    ////כשלוחצים על כפתור של הוספת מוצר, נפתח חלון של הוספת מוצר
    //private void Button_Click(object sender, RoutedEventArgs e) => new AddOrUpdateProduct(bl).Show();

    ////אם מתבצעת לחיצה כפולה על מוצר מהרשימה, נפתח חלון של עידכון של אותו מוצר
    //private void GoUpdateProduct(object sender, RoutedEventArgs e) => new AddOrUpdateProduct(bl).Show();

    private void addProductToCart(object sender, RoutedEventArgs e)
    {
        bl.Cart.AddProduct(cart, 100001);//פה צריך להיות הid. איך מגיעים אליו??
    }
}
