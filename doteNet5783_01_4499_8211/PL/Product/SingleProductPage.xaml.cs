using BLApi;
using BO;
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

namespace PL.Product
{
    /// <summary>
    /// Interaction logic for SingleProductPage.xaml
    /// </summary>
    public partial class SingleProductPage : Page
    {
        IBl bl;
        BO.Cart cart;
        Frame mainFrame;
        BO.ProductItem product;

        public SingleProductPage(IBl BL, BO.Cart _cart, int productId, Frame frame)
        {
            InitializeComponent();
            bl= BL; 
            this.cart = _cart;
            product = bl.Product.GetProductDetails(productId, cart);
            DataContext= product;
            frame = mainFrame;
            for (int i = 1; i <= product.InStock; i++)
                SelectedAmount.Items.Add(i);
            SelectedAmount.SelectedItem = 1;
        }

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            var b = (Button)sender;
            int id = ((ProductItem)b.DataContext).ID;
            try
            {
                cart = bl.Cart.AddProduct(cart, id);
                bl.Cart.UpdateAmountProduct(cart, id, (int)SelectedAmount.SelectedItem);
            }
            catch (BO.OutOfStockException ex)
            {
                MessageBox.Show("!המוצרים שלנו הם כמו שוקולד: נגמרים בלי ששמים לב" +
                    "המוצר הזה אזל במלאי", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                return;
            }
        }
    }
}
