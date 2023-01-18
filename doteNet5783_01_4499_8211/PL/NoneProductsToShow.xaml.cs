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

namespace PL;
/// <summary>
/// Interaction logic for NoneProductsToShow.xaml
/// </summary>
public partial class NoneProductsToShow : Page
{
    IBl bl;
    BO.Cart cartBo;
    Frame framePage;

    public NoneProductsToShow(IBl BL, string search, BO.Cart cart, Frame frame)
    {
        InitializeComponent();
        bl = BL;
        Search.Text = search;
        cartBo =  cart;
        framePage = frame;
    }

    private void GoToCatalog_Click(object sender, RoutedEventArgs e)
    {
        framePage.Content = new ProductCatalogForCostumer(bl, "All", cartBo, framePage);
    }
}
