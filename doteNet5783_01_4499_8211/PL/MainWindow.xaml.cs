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
using BlApi;
using BLApi;
using BO;
using PL.Product;

namespace PL;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private IBl bl= BlFactory.GetBl();
    private BO.Cart cart = new();

    public MainWindow() //החלון הראשי של החנות
    {
        InitializeComponent();
        cart.orderItems = new();
        SelectCategory.ItemsSource = Enum.GetValues(typeof(BO.Category));
        //מכניס לתיבת בחירה את כל הקטגוריות האפשריות לבחירה
        SelectCategory.Text= "קטגוריות";
    }

    //מעביר למסך מנהל
    private void Button_Click(object sender, RoutedEventArgs e) => new AdminView().Show();

    //בלחיצה כפולה על התיבת טקסט היא מתרוקנת
    private void SearchClear(object sender, MouseButtonEventArgs e)=> SearchWrite.Clear();

    //מעביר לחלון עגלה
    private void SelectCart_Click(object sender, RoutedEventArgs e) => new Cart.customerDetails(cart).Show();
}
