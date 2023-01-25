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
using static System.Net.Mime.MediaTypeNames;
using PO;

namespace PL.Product
{
    /// <summary>
    /// Interaction logic for AddOrUpdateProduct.xaml
    /// </summary>
    public partial class AddOrUpdateProduct : Window
    {
        IBl bl;
        string? path;
        private ProductPO productPo = new();

        //עידכון מוצר
        public AddOrUpdateProduct(IBl BL, BO.ProductForList product)
        {
            InitializeComponent();
            bl = BL;
            Tools.CopyPropTo(bl.Product.GetProductDetails(product.ID), productPo);
            UpdateID.IsEnabled = false; //אין אפשרות לשנות את המזהה של המוצר
            UpdateOrAdd.Content = "עדכן";
            UpdateCategory.ItemsSource = Enum.GetValues(typeof(PL.Category));
            productAddOrUp.DataContext = productPo;
            Title.Text = "עדכון מוצר";
            path = product.Path;
        }

        //הוספת מוצר
        public AddOrUpdateProduct(IBl BL)
        {
            InitializeComponent();
            bl = BL;
            UpdateCategory.ItemsSource = Enum.GetValues(typeof(PL.Category));
            UpdateOrAdd.Content = "הוסף";
            //אם הגענו לבנאי הריק, סימן שבאנו לחלון של הוספה
            UpdateID.IsEnabled = true;
            //נותנים אפשרות להכניס את המזהה של המוצר
            Title.Text = "הוספת מוצר";
            path = "/Images/logo.png";
        }
        private void changeImageButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog f = new Microsoft.Win32.OpenFileDialog();
            f.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            if (f.ShowDialog() == true)
            {
                ProductImage.Source = new BitmapImage(new Uri(f.FileName));
                string[] strArr = ((ProductImage.Source).ToString()).Split("PL", 2, StringSplitOptions.RemoveEmptyEntries);
                path = strArr[1];
            }
        }
        private void UpdateOrAdd_Click(object sender, RoutedEventArgs e)
        {
            //אם לא סיימו להכניס ערכים לכל השדות נפתחת תיבת מסר עם אזהרה
            if (UpdateID.Text.Length == 0 || UpdateCategory.SelectedItem == null || UpdateName.Text.Length == 0
                || UpdatePrice.Text.Length == 0 || UpdateInStock.Text.Length == 0)
            {
                MessageBox.Show("בבקשה למלא את כל שדות החובה", "", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                return;
            }
            try
            {
                if ((string)UpdateOrAdd.Content == "הוסף") //אם אנחנו במקרה של הוספה
                    bl.Product.AddProduct(new()
                    {
                        ID = int.Parse(UpdateID.Text),
                        Name = UpdateName.Text,
                        Category = (BO.Category)(Tools.PLCategoryToBL((PL.Category)UpdateCategory.SelectedItem))!,
                        Price = double.Parse(UpdatePrice.Text),
                        InStock = int.Parse(UpdateInStock.Text),
                        IsDeleted = false,
                        Path= path,
                    });
                else
                    bl.Product.UpdateProductDetails(new() //אם אנחנו במקרה של עידכון
                    {
                        ID = int.Parse(UpdateID.Text),
                        Name = UpdateName.Text,
                        Category = (BO.Category)(Tools.PLCategoryToBL((PL.Category)UpdateCategory.SelectedItem))!,
                        Price = double.Parse(UpdatePrice.Text),
                        InStock = int.Parse(UpdateInStock.Text),
                        IsDeleted = false,
                        Path = path,
                    });
            }
            catch (BO.InvalidIDException ex)//תפיסת החריגות האפשריות
            {
                MessageBox.Show("אופס, מספר המוצר שלך בעייתי בשבילנו", "שגיאה", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                return;
            }
            catch (BO.NoNameException ex)
            {
                MessageBox.Show("אם לא החלטת עדיין על שם לבייבי שלך, כדאי לך לחשוב על זה", "שגיאה", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                return;
            }
            catch (BO.InvalidPriceException ex)
            {
                MessageBox.Show("(;משהו מוזר לנו במחיר של המוצר, אולי בגלל שהוא יקר מדי", "שגיאה", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                return;
            }
            catch (BO.OutOfStockException ex)
            {
                MessageBox.Show("!המוצרים שלנו הם כמו שוקולד: נגמרים בלי ששמים לב" , "המוצר אזל במלאי", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                return;
            }
            catch (BO.AlreadyExistsException ex)
            {
                MessageBox.Show("?אנחנו חושבים שהמוצר כבר קיים במערכת, אולי מדובר בתאומים זהים", "שגיאה", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                return;
            }
            Close();
        }

        private void EntersOnlyNumbers(object sender, KeyEventArgs e) => Tools.EnterNumbersOnly(sender, e);

         private void EntersPrice(object sender, KeyEventArgs e) => Tools.EnterOnlyNumbersAndPoint(sender, e);
       

        private void UpdateCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UpdateID.Text.Length == 0 || UpdateCategory.SelectedItem == null || UpdateName.Text.Length == 0
                  || UpdatePrice.Text.Length == 0 || UpdateInStock.Text.Length == 0)
                return;
            UpdateOrAdd.IsEnabled = true;
        }
    }
}
