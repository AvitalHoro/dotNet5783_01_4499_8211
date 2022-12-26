using BLApi;
using BO;
using PL.Order;
using PL.Product;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    ObservableCollection<ProductForList> listProducts = new();
    ObservableCollection<OrderForList> listOrders = new();
    Frame frame;

    public AdminPage(IBl BL, Frame frame)
    {
        InitializeComponent();
        bl = BL;
        listProducts = Tools.IEnumerableToObservable(listProducts, bl.Product.GetProductList());
        listOrders = Tools.IEnumerableToObservable(listOrders, bl.Order.getOrderList());
        ProductsListAdmin.DataContext = listProducts;
        OrdersListAdmin.DataContext = listOrders;
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
        this.frame = frame; 
    }

    private void SelectCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        switch (SelectCategoryForOrder.SelectedItem)
        {
            case "הכל":
                Tools.IEnumerableToObservable(listProducts, bl.Product.GetProductList());
                break;
            case "עגלות וטיולונים":
                Tools.IEnumerableToObservable(listProducts,
                    bl.Product.GetProductList(BO.Filters.filterByCategory, BO.Category.Carts));
                break;
            case "צעצועים ומשחקים":
                Tools.IEnumerableToObservable(listProducts,
                    bl.Product.GetProductList(BO.Filters.filterByCategory, BO.Category.Toys));
                break;
            case "ביגוד והנעלה":
                Tools.IEnumerableToObservable(listProducts,
                    bl.Product.GetProductList(BO.Filters.filterByCategory, BO.Category.Clothes));
                break;
            case "היגיינה והחתלה":
                Tools.IEnumerableToObservable(listProducts,
                    bl.Product.GetProductList(BO.Filters.filterByCategory, BO.Category.Diapers));
                break;
            case "בקבוקים ומוצצים":
                Tools.IEnumerableToObservable(listProducts,
                    bl.Product.GetProductList(BO.Filters.filterByCategory, BO.Category.Bottles));
                break;
        }
    }

    private void SelectCategoryForOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        switch (SelectCategoryForOrder.SelectedItem)
        {
            case "הכל":
                Tools.IEnumerableToObservable(listOrders, bl.Order.getOrderList());
                break;
            case "הזמנות שאושרו":
                Tools.IEnumerableToObservable(listOrders,
                    bl.Order.getOrderList(order => order?.ShipDate == null && order?.DeliveryDate == null));
                break;
            case "הזמנות שנשלחו":
                Tools.IEnumerableToObservable(listOrders,
                    bl.Order.getOrderList(order => order?.ShipDate != null && order?.DeliveryDate == null));
                break;
            case "הזמנות שנמסרו":
                Tools.IEnumerableToObservable(listOrders,
                    bl.Order.getOrderList(order => order?.ShipDate != null && order?.DeliveryDate != null));
                break;
        }
    }

    private void AddProduct_Click(object sender, RoutedEventArgs e)
    {
        new AddOrUpdateProduct(bl).ShowDialog();
        Tools.IEnumerableToObservable(listProducts, bl.Product.GetProductList());
    }

    private void ProductsListAdmin_MouseDoubleClick(object sender, MouseEventArgs e)
    {
        new AddOrUpdateProduct(bl, (BO.ProductForList)((DataGrid)sender).SelectedItem).ShowDialog();
        Tools.IEnumerableToObservable(listProducts, bl.Product.GetProductList());
    }

    private void OrdersListAdmin_MouseDoubleClick(object sender, MouseEventArgs e)
    {
        BO.OrderForList order= (BO.OrderForList)((DataGrid)sender).SelectedItem;
        frame.Content = new PL.Order.OrderTracking(bl, bl.Order.getDetailsOrder(order.ID), frame);
        Tools.IEnumerableToObservable(listOrders, bl.Order.getOrderList());
    }
}
