using PL.Order;
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

namespace PL.Product;
/// <summary>
/// Interaction logic for AdminView.xaml
/// </summary>
public partial class AdminView : Window
{
    public AdminView() //חלון המנהל
    {
        InitializeComponent();
    }

    //לחיצה על הכפתור פותחתת הרשימה של כל המוצרים בחנות
    private void ListProduct_Click(object sender, RoutedEventArgs e) => new ProductList().Show();

    //לחיצה על הכפתור פותחתת הרשימה של כל ההזמנות בחנות
    private void ListOrders_Click(object sender, RoutedEventArgs e) => new OrderList().Show();
}
