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
    }

    private void ToTheProductsList_Click(object sender, RoutedEventArgs e)
    {

    }

    private void ToTheOrdersList_Click(object sender, RoutedEventArgs e)
    {

    }
}
