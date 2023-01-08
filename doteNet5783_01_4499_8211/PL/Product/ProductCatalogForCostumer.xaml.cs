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
    string content;
    Frame frame;

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
                PropertyChanged(this, new PropertyChangedEventArgs("ListProduct"));
            }
        }
    }

    public ProductCatalogForCostumer(IBl BL, string ButtonName, BO.Cart cart , Frame frame)
    {
        InitializeComponent();
        bl = BL;
        this.cart = cart;
        ListProduct = new ObservableCollection<ProductForList>(bl.Product.GetProductList(isInStock: true));
        this.frame = frame;
        content = ButtonName;

        switch (ButtonName)
        {
            case "Toys":
                ListProduct = new ObservableCollection<ProductForList>
                    (bl.Product.GetProductList(BO.Filters.filterByCategory, BO.Category.Toys, true));
                break;
            case "Carts":
                ListProduct = new ObservableCollection<ProductForList>
                    (bl.Product.GetProductList(BO.Filters.filterByCategory, BO.Category.Carts, true));
                break;
            case "Clothes":
                ListProduct = new ObservableCollection<ProductForList>
                    (bl.Product.GetProductList(BO.Filters.filterByCategory, BO.Category.Clothes, true));
                break;
            case "Diapers":
                ListProduct = new ObservableCollection<ProductForList>
                    (bl.Product.GetProductList(BO.Filters.filterByCategory, BO.Category.Diapers, true));
                break;
            case "Bottles":
                ListProduct = new ObservableCollection<ProductForList>
                    (bl.Product.GetProductList(BO.Filters.filterByCategory, BO.Category.Bottles, true));
                break;
            case "All":
            case "GoBackToCatalog":
                ProductListview.ItemsSource = new ObservableCollection<ProductForList>(bl.Product.GetProductList(isInStock: true));
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ProductListview.ItemsSource);
                PropertyGroupDescription groupDescription = new PropertyGroupDescription("Category");
                view.GroupDescriptions.Add(groupDescription);
               
                break;
            default:
                ListProduct = new ObservableCollection<ProductForList>
                    (bl.Product.GetProductList(BO.Filters.filterByName, ButtonName, true));
                break;
        }
        DataContext = ListProduct;
    }

    private void addProductToCart(object sender, RoutedEventArgs e)
    {
        var b = (Button)sender;
        int id = ((ProductForList)b.DataContext).ID;
        try
        {
            cart = bl.Cart.AddProduct(cart, id);
        }
        catch (BO.OutOfStockException ex)
        {
            MessageBox.Show("!המוצרים שלנו הם כמו שוקולד: נגמרים בלי ששמים לב" +
                "המוצר הזה אזל במלאי", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            return;
        }
        BO.Product product = bl.Product.GetProductDetails(id);
    }

    private void ProductListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        frame.Content = new SingleProductPage(bl, cart, ((BO.ProductForList)ProductListview.SelectedItem).ID, frame, content);
    }
}
