using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BlApi;
using BLApi;

namespace PL.Product;
/// <summary>
/// Interaction logic for UpdateProduct.xaml
/// </summary>
/// 
//חלון להוספה או עדכון של מוצר חדש, נשלח מחלון המנהל
public partial class UpdateProduct : Window
{
    private IBl bl = BlFactory.GetBl();
    public UpdateProduct() // בנאי לחלון הוספה
    {
        InitializeComponent();
        UpdateCategory.ItemsSource = Enum.GetValues(typeof(BO.Category));
        //מכניס לתיבת בחירה את כל הקטגוריות האפשריות לבחירה
        UpdateOrAdd.Content = "Add";
        //אם הגענו לבנאי הריק, סימן שבאנו לחלון של הוספה
        UpdateID.IsEnabled = true;
        //נותנים אפשרות להכניס את המזהה של המוצר
    }

    //התיבת טקסט של הכנסת מזהה מוצר שולחת לפונקציה שמאפשרת להכניס רק ספרות לתיבת טקסט
    private void AddIdValidation(object sender, KeyEventArgs e) => Tools.EnterNumbersOnly(sender, e);

    //התיבת טקסט של הכנסת מחיר שולחת לפונקציה שמאפשרת להכניס רק ספרות לתיבת טקסט
    private void AddPriceValidation(object sender, KeyEventArgs e) => Tools.EnterNumbersOnly(sender, e);

    //התיבת טקסט של הכנסת כמות במלאי שולחת לפונקציה שמאפשרת להכניס רק ספרות לתיבת טקסט
    private void AddInStockValidation(object sender, KeyEventArgs e) => Tools.EnterNumbersOnly(sender, e);

    
    public UpdateProduct(BO.ProductForList product)//בנאי לחלון עידכון 
    {
        //מקבל את המוצר אותו אני רוצה לעדכן וכותב את הערך של השדות שלו בתיבות
        InitializeComponent();
        UpdateID.IsEnabled = false; //אין אפשרות לשנות את המזהה של המוצר
        UpdateOrAdd.Content = "Update";
        UpdateCategory.ItemsSource = Enum.GetValues(typeof(BO.Category));
        UpdateID.Text = product.ID.ToString();
        UpdateCategory.SelectedItem = product.Category;
        UpdateName.Text = product.Name;
        UpdatePrice.Text = product.Price.ToString();
        //UpdateInStock.Text = product.InStock.ToString();
    }

    //פונקציה שמופעלת ברגע שלוחצים עם העכבר על כפתור ההוספה
    private void UpdateOrAdd_Click(object sender, RoutedEventArgs e)
    {
        //אם לא סיימו להכניס ערכים לכל השדות נפתחת תיבת מסר עם אזהרה
        if (UpdateID.Text.Length == 0  || UpdateCategory.SelectedItem == null || UpdateName.Text.Length == 0
            || UpdatePrice.Text.Length == 0 || UpdateInStock.Text.Length == 0)
        {
            MessageBox.Show("Please check all fields are complete","Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            return;
        }
        try
        {
            if ((string)UpdateOrAdd.Content == "Add") //אם אנחנו במקרה של הוספה
                bl.Product.AddProduct(new()
                {
                    ID = int.Parse(UpdateID.Text),
                    Name = UpdateName.Text,
                    Category = (BO.Category)UpdateCategory.SelectedItem,
                    Price = int.Parse(UpdatePrice.Text),
                    InStock = int.Parse(UpdateInStock.Text),
                    IsDeleted = false,
                });
            else
                bl.Product.UpdateProductDetails(new() //אם אנחנו במקרה של עידכון
                {
                    ID = int.Parse(UpdateID.Text),
                    Name = UpdateName.Text,
                    Category = (BO.Category)UpdateCategory.SelectedItem,
                    Price = int.Parse(UpdatePrice.Text),
                    InStock = int.Parse(UpdateInStock.Text),
                    IsDeleted = false,
                });
        }
        catch (BO.InvalidIDException ex)//תפיסת החריגות האפשריות
        {
            MessageBox.Show("אופס, מספר המוצר שלך בעייתי בשבילנו", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            return;
        }
        catch (BO.NoNameException ex)
        {
            MessageBox.Show("אם לא החלטת עדיין על שם לבייבי שלך, כדאי לך לחשוב על זה", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            return;
        }
        catch (BO.InvalidPriceException ex)
        {
            MessageBox.Show("(;משהו מוזר לנו במחיר של המוצר, אולי זה בגלל שהוא יקר מדי", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            return;
        }
        catch (BO.OutOfStockException ex)
        {
            MessageBox.Show("!המוצרים שלנו הם כמו שוקולד: נגמרים בלי ששמים לב" +
                "המוצר הזה אזל במלאי", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            return;
        }
        catch (BO.AlreadyExistsException ex)
        {
            MessageBox.Show("?אנחנו חושבים שהמוצר כבר קיים במערכת, אולי מדובר בתאומים זהים", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            return;
        }
        Close();
    }

}
