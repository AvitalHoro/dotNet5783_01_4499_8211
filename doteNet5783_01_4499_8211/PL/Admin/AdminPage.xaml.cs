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

public partial class AdminPage : Page
{
    IBl bl;
    ObservableCollection<ProductForList> listProducts = new();
    ObservableCollection<PO.OrderPO> listOrders = new();
    Frame frame;

    public AdminPage(IBl BL, Frame frame)
    {
        InitializeComponent();
        bl = BL;
        listProducts = Tools.IEnumerableToObservable(listProducts, bl.Product.GetProductList());
        var list = (bl.Order.GetOrderList()).Select(order => Tools.CopyPropTo(order, new PO.OrderPO()));
        Tools.IEnumerableToObservable(listOrders, list);
        ProductsListAdmin.DataContext = listProducts;
        OrdersListAdmin.DataContext = listOrders;
        SelectCategory.ItemsSource = Tools.CategoryToHebrew();
        SelectCategoryForOrder.ItemsSource = Tools.StateToHebrew();
        this.frame = frame;
        RestoreProduct.Visibility = Visibility.Collapsed;
    }

    private void SelectCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        string select = (string)SelectCategory.SelectedItem;
       // DeleteProduct.Visibility = Visibility.Visible;

        if(select == "הכל")
            Tools.IEnumerableToObservable(listProducts, bl.Product.GetProductList());
        else
            Tools.IEnumerableToObservable(listProducts, bl.Product.GetProductList
                (BO.Filters.filterByCategory, Tools.HebrewToCategory(select)));
    }

    private void SelectCategoryForOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        string select = (string)SelectCategoryForOrder.SelectedItem;
        var list = (bl.Order.GetOrderList(Tools.stringToState(select))).Select(order => Tools.CopyPropTo(order, new PO.OrderPO()));
        Tools.IEnumerableToObservable(listOrders, list);
    }

    private void AddProduct_Click(object sender, RoutedEventArgs e)
    {
        new AddOrUpdateProduct(bl).ShowDialog();
        if(TitleProducts.Text == "מוצרים בחנות")
        Tools.IEnumerableToObservable(listProducts, bl.Product.GetProductList());
        else
            Tools.IEnumerableToObservable(listProducts,
                  bl.Product.GetProductList(BO.Filters.deleted));
    }

    private void ProductsListAdmin_MouseDoubleClick(object sender, MouseEventArgs e)
    {
        new AddOrUpdateProduct(bl, (BO.ProductForList)((DataGrid)sender).SelectedItem).ShowDialog();
        Tools.IEnumerableToObservable(listProducts, bl.Product.GetProductList());
    }

    private void OrdersListAdmin_MouseDoubleClick(object sender, MouseEventArgs e)
    {
        PO.OrderPO order= (PO.OrderPO)((DataGrid)sender).SelectedItem;
        frame.Content = new PL.Order.OrderTracking(bl, order, frame, true);
        var orderPo = listOrders.FirstOrDefault(o => o.ID == order.ID);
        BO.Order orderBo = bl.Order.GetDetailsOrder(order.ID);
        orderPo.ShipDate = orderBo.ShipDate;    
        orderPo.State= orderBo.State;   
    }

    private void DeleteProduct_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var b = (Button)sender;
            int id = ((ProductForList)b.DataContext).ID;
            bl.Product.RemoveProduct(id);
            Tools.IEnumerableToObservable(listProducts, bl.Product.GetProductList());
        }
        catch(BO.ProductExistInOrderException)
        {
            MessageBox.Show("לא ניתן למחוק את המוצר, הוא הוזמן בהזמנות שעוד לא נשלחו", "מחיקת מוצר", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void RestoreProduct_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var b = (Button)sender;
            var product = (ProductForList)b.DataContext;
            bl.Product.RestoreProduct(product.ID);
              new AddOrUpdateProduct(bl, product).ShowDialog();
            Tools.IEnumerableToObservable(listProducts,
                    bl.Product.GetProductList(BO.Filters.deleted));
        }
        catch (BO.ProductExistInOrderException)
        {
            MessageBox.Show("המוצר לא נמצא");
        }
    }

    private void Archives_Click(object sender, RoutedEventArgs e)
    {
        if((string)Archives.Content == "מוצרים בארכיון")
        {
            Archives.Content = "כל המוצרים";
            Tools.IEnumerableToObservable(listProducts,
                   bl.Product.GetProductList(BO.Filters.deleted));
            DeleteProduct.Visibility = Visibility.Collapsed;
            RestoreProduct.Visibility = Visibility.Visible;
        }
        else
        {
            Archives.Content = "מוצרים בארכיון";
            Tools.IEnumerableToObservable(listProducts, bl.Product.GetProductList());
            DeleteProduct.Visibility = Visibility.Visible;
            RestoreProduct.Visibility = Visibility.Collapsed;
        }
     
    }

    private void CancelOrder_Click(object sender, RoutedEventArgs e)
    {
        var b = sender as Button;
        int id = ((PO.OrderPO)b.DataContext).ID;
        var order = listOrders.FirstOrDefault(x => x.ID == id);
        listOrders.Remove(order);
        bl.Order.CancelOrder(id);
    }

    private void OpenSimulator_Click(object sender, RoutedEventArgs e) => new SimulatorWindow(bl, listOrders).Show();
    
}
