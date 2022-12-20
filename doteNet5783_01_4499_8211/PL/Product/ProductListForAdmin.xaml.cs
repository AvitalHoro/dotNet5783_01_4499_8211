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

namespace PL.Product;

/// <summary>
/// Interaction logic for ProductListForAdmin.xaml
/// </summary>
public partial class ProductListForAdmin : Page
{
    IBl bl;
    public ProductListForAdmin(IBl BL)
    {
        InitializeComponent();
        bl = BL;
        SelectCategory.Items.Add("הכל");
        SelectCategory.Items.Add("עגלות וטיולונים");
        SelectCategory.Items.Add("צעצועים ומשחקים");
        SelectCategory.Items.Add("ביגוד והנעלה");
        SelectCategory.Items.Add("היגיינה והחתלה");
        SelectCategory.Items.Add("בקבוקים ומוצצים");
    }

    private void ProductsListAdmin_MouseDoubleClick(object sender, MouseEventArgs e)
        => new AddOrUpdateProduct(bl,(BO.ProductForList)((DataGrid)sender).SelectedItem).ShowDialog();

    private void SelectCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      //אנחנו רוצות לסנן את המוצרים ברשימה לפי מה שהמנהל בחר
    }

    private void AddProduct_Click(object sender, RoutedEventArgs e)
        => new AddOrUpdateProduct(bl).ShowDialog();

}
