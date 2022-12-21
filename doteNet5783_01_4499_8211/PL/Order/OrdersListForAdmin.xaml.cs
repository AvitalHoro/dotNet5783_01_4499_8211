using BLApi;
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

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for OrdersListForAdmin.xaml
    /// </summary>
    public partial class OrdersListForAdmin : Page
    {
        IBl bl;
        public OrdersListForAdmin(IBl BL)
        {
            InitializeComponent();
            bl = BL;
            OrdersListAdmin.DataContext = bl.Order.getOrderList();

            SelectCategory.Items.Add("הכל");
            SelectCategory.Items.Add("הזמנות שאושרו");
            SelectCategory.Items.Add("הזמנות שנשלחו");
            SelectCategory.Items.Add("הזמנות שנמסרו");
        }

        private void ProductsListAdmin_MouseDoubleClick(object sender, MouseEventArgs e)
                => new TrackingOrder(bl, (BO.OrderForList)((DataGrid)sender).SelectedItem).ShowDialog();

        private void SelectCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //אנחנו רוצות לסנן את המוצרים ברשימה לפי מה שהמנהל בחר
        }
    }
}
