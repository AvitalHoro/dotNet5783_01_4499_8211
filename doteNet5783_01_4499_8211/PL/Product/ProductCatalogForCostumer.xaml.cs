using BLApi;
using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public event PropertyChangedEventHandler PropertyChanged;
    private ObservableCollection<ProductForList> listProduct;
    public ObservableCollection<ProductForList> ListProduct
    {
        get { return listProduct; }
        set
        {
            listProduct = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("CList"));
            }
        }
    }

    public ProductCatalogForCostumer(IBl BL, string ButtonName, BO.Cart cart)
    {
        InitializeComponent();
        bl = BL;
        this.cart = cart;
        ListProduct = new ObservableCollection<ProductForList>(bl.Product.GetProductList());

        switch (ButtonName)
        {
            case "Toys":
                ListProduct = new ObservableCollection<ProductForList>
                    (bl.Product.GetProductList(BO.Filters.filterByCategory, BO.Category.Toys));
                break;
            case "Carts":
                ListProduct = new ObservableCollection<ProductForList>
                    (bl.Product.GetProductList(BO.Filters.filterByCategory, BO.Category.Carts));
                break;
            case "Clothes":
                ListProduct = new ObservableCollection<ProductForList>
                    (bl.Product.GetProductList(BO.Filters.filterByCategory, BO.Category.Clothes));
                break;
            case "Diapers":
                ListProduct = new ObservableCollection<ProductForList>
                    (bl.Product.GetProductList(BO.Filters.filterByCategory, BO.Category.Diapers));
                break;
            case "Bottles":
                ListProduct = new ObservableCollection<ProductForList>
                    (bl.Product.GetProductList(BO.Filters.filterByCategory, BO.Category.Bottles));
                break;
            case "All":
            case "GoBackToCatalog":
                ListProduct = new ObservableCollection<ProductForList>(bl.Product.GetProductList());
                break;
            default:
                ListProduct = new ObservableCollection<ProductForList>
                    (bl.Product.GetProductList(BO.Filters.filterByName, ButtonName));
                break;
        }
        DataContext = ListProduct;
    }

    private void addProductToCart(object sender, RoutedEventArgs e)
    {
        var b = (Button)sender;
        cart= bl.Cart.AddProduct(cart, ((ProductForList)b.DataContext).ID);
    }
}
        