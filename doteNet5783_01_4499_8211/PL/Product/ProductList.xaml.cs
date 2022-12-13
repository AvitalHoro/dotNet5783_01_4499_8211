using BlApi;
using BLApi;
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
using System.Xml.Linq;

namespace PL.Product;
/// <summary>
/// Interaction logic for ProductList.xaml
/// </summary>
public partial class ProductList : Window
{
    private IBl bl = BlFactory.GetBl();
    public ProductList()
    {
        InitializeComponent();
        ProductListview.ItemsSource = bl.Product.GetProductList();
        AttributeSelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
    }

    private void AttributeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if((BO.Category)AttributeSelector.SelectedItem == BO.Category.All)
            ProductListview.ItemsSource = bl.Product.GetProductList();
       else
            ProductListview.ItemsSource = bl.Product.GetProductListOfSpecificCategory((BO.Category)AttributeSelector.SelectedItem);
    }

    private void Button_Click(object sender, RoutedEventArgs e) => new AddProduct().Show();

   private void GoUpdateProduct(object sender, RoutedEventArgs e)=> new UpdateProduct((BO.ProductForList)ProductListview.SelectedItem).Show();
}
