using BlApi;
using BLApi;
using System;
using System.Collections.Generic;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.Product;
/// <summary>
/// Interaction logic for AddProduct.xaml
/// </summary>
public partial class AddProduct : Window
{
    private IBl bl = BlFactory.GetBl();

    public AddProduct()
    {
        InitializeComponent();
        SelectCategory.ItemsSource = Enum.GetValues(typeof(BO.Category));
    }

    private void AddIdValidation(object sender, KeyEventArgs e) => Tools.EnterNumbersOnly(sender, e);

    private void AddPriceValidation(object sender, KeyEventArgs e) => Tools.EnterNumbersOnly(sender, e);

    private void AddInStockValidation(object sender, KeyEventArgs e) => Tools.EnterNumbersOnly(sender, e);

        //allow control system keys
        if (Char.IsControl(c)) return;

        //allow digits (without Shift or Alt)
        if (Char.IsDigit(c))
            if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))
                return; //let this key be written inside the textbox

        //forbid letters and signs (#,$, %, ...)
        e.Handled = true; //ignore this key. mark event as handled, will not be routed to other controls
        return;
    }

    private void ValidationID(object sender, KeyEventArgs e) => EnterNumbersOnly(sender, e);

    private void AddPrice_TextChanged(object sender, KeyEventArgs e) => EnterNumbersOnly(sender, e);

    private void AddInStock_TextChanged(object sender, KeyEventArgs e) => EnterNumbersOnly(sender, e);

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {

        bl.Product.AddProduct(new()
        {
            ID = int.Parse(AddID.Text),
            Name = AddName.Text,
            Category = (BO.Category)SelectCategory.SelectedItem,
            Price = int.Parse(AddPrice.Text),
            InStock = int.Parse(AddInStock.Text),
            IsDeleted = false,
        }) ;
    }
}

