using BLApi;
using PL.Order;
using PL.Product;
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

namespace PL;
/// <summary>
/// Interaction logic for AdminPage.xaml
/// </summary>
public partial class AdminPage : Page
{
    IBl bl;
    public AdminPage(IBl BL)
    {
        InitializeComponent();
        bl= BL;
        SelectCategory.Items.Add("הכל");
        SelectCategory.Items.Add("עגלות וטיולונים");
        SelectCategory.Items.Add("צעצועים ומשחקים");
        SelectCategory.Items.Add("ביגוד והנעלה");
        SelectCategory.Items.Add("היגיינה והחתלה");
        SelectCategory.Items.Add("בקבוקים ומוצצים");
        SelectCategoryForOrder.Items.Add("הכל");
        SelectCategoryForOrder.Items.Add("הזמנות שאושרו");
        SelectCategoryForOrder.Items.Add("הזמנות שנשלחו");
        SelectCategoryForOrder.Items.Add("הזמנות שנמסרו");
        ProductsListAdmin.ItemsSource=bl.Product.GetProductList();  
        OrdersListAdmin.ItemsSource=bl.Order.getOrderList();    
    }

    private void ProductsListAdmin_MouseDoubleClick(object sender, MouseEventArgs e)
        => new AddOrUpdateProduct(bl, (BO.ProductForList)((DataGrid)sender).SelectedItem).ShowDialog();

    private void SelectCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        //אנחנו רוצות לסנן את המוצרים ברשימה לפי מה שהמנהל בחר
    }

    private void AddProduct_Click(object sender, RoutedEventArgs e)
        => new AddOrUpdateProduct(bl).ShowDialog();

  

    private void OrdersListAdmin_MouseDoubleClick(object sender, MouseEventArgs e)
        => new TrackingOrder(bl, (BO.OrderForList)((DataGrid)sender).SelectedItem).ShowDialog();

    private void SelectCategoryForOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
    //אנחנו רוצות לסנן את המוצרים ברשימה לפי מה שהמנהל בחר
    }

    private void OrderssListAdmin_MouseDoubleClick(Object sender, MouseEventArgs e)
    {

    }

}
