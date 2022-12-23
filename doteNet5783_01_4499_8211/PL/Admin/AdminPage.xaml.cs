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
        switch(SelectCategoryForOrder.SelectedItem)
        {
            case "הכל":
                ProductsListAdmin.ItemsSource = bl.Product.GetProductList();
                break;
            case "עגלות וטיולונים":
                ProductsListAdmin.ItemsSource = bl.Product.GetProductList(BO.Filters.filterByCategory, BO.Category.Carts);
                break;
            case "צעצועים ומשחקים":
                ProductsListAdmin.ItemsSource = bl.Product.GetProductList(BO.Filters.filterByCategory, BO.Category.Toys);
                break;
            case "ביגוד והנעלה":
                ProductsListAdmin.ItemsSource = bl.Product.GetProductList(BO.Filters.filterByCategory, BO.Category.Clothes);
                break;
            case "היגיינה והחתלה":
                ProductsListAdmin.ItemsSource = bl.Product.GetProductList(BO.Filters.filterByCategory, BO.Category.Diapers);
                break;
            case "בקבוקים ומוצצים":
                ProductsListAdmin.ItemsSource = bl.Product.GetProductList(BO.Filters.filterByCategory, BO.Category.Bottles);
                break;
        }
    }

    private void AddProduct_Click(object sender, RoutedEventArgs e)
        => new AddOrUpdateProduct(bl).ShowDialog();

  

    private void OrdersListAdmin_MouseDoubleClick(object sender, MouseEventArgs e)
        => new TrackingOrder(bl, (BO.OrderForList)((DataGrid)sender).SelectedItem).ShowDialog();

    private void SelectCategoryForOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        switch(SelectCategoryForOrder.SelectedItem)
        {
            case "הכל":
                OrdersListAdmin.ItemsSource = bl.Order.getOrderList();
                break;
            case "הזמנות שאושרו":
                OrdersListAdmin.ItemsSource = bl.Order.getOrderList(order => order?.ShipDate == null && order?.DeliveryDate == null);
                break;
            case "הזמנות שנשלחו":
                OrdersListAdmin.ItemsSource = bl.Order.getOrderList(order => order?.ShipDate != null && order?.DeliveryDate == null);
                break;
            case "הזמנות שנמסרו":
                OrdersListAdmin.ItemsSource = bl.Order.getOrderList(order => order?.ShipDate != null && order?.DeliveryDate != null);
                break;
        }
    }

    private void OrderssListAdmin_MouseDoubleClick(Object sender, MouseEventArgs e)
    {

    }

}
