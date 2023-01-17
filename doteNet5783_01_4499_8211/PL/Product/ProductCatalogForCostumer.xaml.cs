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

    //public event PropertyChangedEventHandler PropertyChanged;
    private ObservableCollection<BO.ProductItem> listProduct = new();


    public ProductCatalogForCostumer(IBl BL, string ButtonName, BO.Cart cart , Frame frame , bool search = false)
    {
        InitializeComponent();
        bl = BL;
        this.cart = cart;
        this.frame = frame;
        if (!search)
        {
            content = ButtonName;
            if (ButtonName == "All")
            {
                listProduct = Tools.IEnumerableToObservable(listProduct, bl.Product.GetCatalog(cart, isInStock: true));
                ProductListview.ItemsSource = listProduct;
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ProductListview.ItemsSource);
                PropertyGroupDescription groupDescription = new PropertyGroupDescription("Category");
                view.GroupDescriptions.Add(groupDescription);
            }
            else
                listProduct = Tools.IEnumerableToObservable(listProduct, bl.Product.GetCatalog(cart, BO.Filters.filterByCategory, Tools.StringToCategory(ButtonName), true));
        }
        if(search)
           listProduct = Tools.IEnumerableToObservable(listProduct, bl.Product.GetCatalog(cart, BO.Filters.filterByName, ButtonName, true));
        DataContext = listProduct;
    }

    private void addProductToCart(object sender, RoutedEventArgs e)
    {
        var b = (Button)sender;
        int id = ((ProductItem)b.DataContext).ID;
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
        frame.Content = new SingleProductPage(bl, cart, ((BO.ProductItem)ProductListview.SelectedItem).ID, frame, content);
    }
}
 