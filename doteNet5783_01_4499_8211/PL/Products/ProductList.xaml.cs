using BlApi;
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
using System.Windows.Shapes;
using System.Xml.Linq;

namespace PL.Product;
/// <summary>
/// Interaction logic for ProductList.xaml
/// </summary>
public partial class ProductList : Window
{
    private IBl bl = BlApi.BlFactory.GetBl();

    //בנאי לחלון של הרשימה של המוצרים למנהל
    public ProductList()
    {
        InitializeComponent();
        ProductListview.ItemsSource = bl.Product.GetProductList(); 
        //מקבל את רשימת המוצרים משכבת הלוגיקה
        AttributeSelector.ItemsSource = Enum.GetValues(typeof(BO.Category)); 
        //מכניס לתיבת בחירה את כל הקטגוריות האפשריות לבחירה
        AttributeSelector.SelectedItem = BO.Category.All;
        //ברירת המחדל זה להציג את כל המוצרים מכל הקטגוריות
    }

    //מעדכן את רשימת המוצרים לפי הקטגוריה הנבחרת בתיבת הבחירה
    private void AttributeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if ((BO.Category)AttributeSelector.SelectedItem == BO.Category.All)
            //אם נבחרה האפשרות של כל המוצרים אז צריך להראות את כל המוצרים ברשימה
            ProductListview.ItemsSource = bl.Product.GetProductList();
        else
            ProductListview.ItemsSource = bl.Product.GetProductList(BO.Filters.filterByCategory, (BO.Category)AttributeSelector.SelectedItem);
    }

    //כשלוחצים על כפתור של הוספת מוצר, נפתח חלון של הוספת מוצר
    private void Button_Click(object sender, RoutedEventArgs e)=> new UpdateProduct().Show();

    //אם מתבצעת לחיצה כפולה על מוצר מהרשימה, נפתח חלון של עידכון של אותו מוצר
    private void GoUpdateProduct(object sender, RoutedEventArgs e)=> new UpdateProduct((BO.ProductForList)ProductListview.SelectedItem).Show();

    private void Label_TextInput(object sender, TextCompositionEventArgs e)
    {

    }
}
