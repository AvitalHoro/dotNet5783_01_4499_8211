using BLApi;
using PO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PL.Order;
/// <summary>
/// Interaction logic for ApprovedOrder.xaml
/// </summary>
public partial class ApprovedOrder : Page
{
    int _postage = 29;
    private CartPO cart;
    private IBl bl;
    MainWindow mainWindow;
    private ObservableCollection<PO.OrderPO> listOrders = new();

    public ApprovedOrder(IBl _bl, CartPO _cart, MainWindow _mainWindow)
    {
        InitializeComponent();
        mainWindow = _mainWindow;
        bl = _bl;
        cart = _cart;
        DataContext = cart;
        sale.Text = "0";
        if(cart.TotalPrice< 299)
        {
            postage.Text = _postage.ToString();
            TotalPriceShow.Text = (cart.TotalPrice + _postage).ToString();
        }
        else        
        {
            postage.Text = "0";
            TotalPriceShow.Text = cart.TotalPrice.ToString();
        }
    }

    private void Exist_Click(object sender, RoutedEventArgs e)
    {
        mainWindow.fullFrame.Content = null;
    }

    ///   BO.EmptyCartException"></exception>
    /// <exception cref="BO.NoCostumerNameException"></exception>
    /// <exception cref="BO.NoCostumerEmailException"></exception>
    /// <exception cref="BO.NoCostumerAdressException"></exception>
    /// <exception cref="DO.DoesNotExistException"></exception>
    /// <exception cref="DO.AlreadyExistsException"></exception>
    /// <exception cref="BO.AmountException"></exception>
    /// <exception cref="BO.OutOfStockException"
    private void approve_Click(object sender, RoutedEventArgs e)
    {
        int idOrder=0;
        BO.Cart boCart = new();
        int paymentMethod = 0;
        if (payPal.IsChecked==true)
            paymentMethod = 1;
        else if (googlePay.IsChecked == true)
            paymentMethod=2; 
        Tools.PoCartToBoCart(cart, boCart);
        boCart.CostumerName = UpdateName.Text;
        boCart.CostumerAdress = UpdateAdress.Text;
        boCart.CostumerEmail = UpdateEmail.Text;
        try
        {
            idOrder = bl.Cart.MakeOrder(boCart);
            BO.Order order=bl.Order.GetDetailsOrder(idOrder);
            mainWindow.fullFrame.Content = null;
            mainWindow.framePage.Content = new FinishOrder(bl, order, mainWindow.framePage, paymentMethod);
            mainWindow.cart = new();
            mainWindow.cart.OrderItems = new();
        }
      
        catch (BO.EmptyCartException)
        {
            MessageBox.Show("העגלה שלך ריקה", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            return;
        }
        catch (BO.NoCostumerNameException)
        {
            MessageBox.Show("פרטי הלקוח לא מלאים", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            return;
        }
        catch (BO.NoCostumerEmailException)
        {
            MessageBox.Show("פרטי הלקוח לא מלאים", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            return;
        }
        catch (BO.NoCostumerAdressException)
        {
            MessageBox.Show("פרטי הלקוח לא מלאים", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            return;
        }
        catch (BO.AmountException)
        {
            MessageBox.Show("המוצר שבחרת אינו קיים בכמות המבוקשת. מחק אותו מהסל ובצע הזמנה מחדש", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            return;
        }
        catch (BO.AlreadyExistsException ex)
        {
            MessageBox.Show("?אנחנו חושבים שהמוצר כבר קיים במערכת, אולי מדובר בתאומים זהים", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            return;
        }
        catch (BO.DoesNotExistException ex)
        {
            MessageBox.Show("אחד מהמוצרים שבחרת לא קיים במערכת", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            return;
        }
    }
}
