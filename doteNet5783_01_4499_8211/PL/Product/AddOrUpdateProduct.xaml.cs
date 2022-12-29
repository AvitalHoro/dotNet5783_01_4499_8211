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

namespace PL.Product
{
    /// <summary>
    /// Interaction logic for AddOrUpdateProduct.xaml
    /// </summary>
    public partial class AddOrUpdateProduct : Window
    {
        IBl bl;
        string path;
        public AddOrUpdateProduct(IBl BL, BO.ProductForList product)
        {
            InitializeComponent();
            bl = BL;
            UpdateID.IsEnabled = false; //אין אפשרות לשנות את המזהה של המוצר
            UpdateOrAdd.Content = "עדכן";
            UpdateCategory.ItemsSource = Enum.GetValues(typeof(BO.Category));

            //UpdateID.Text = product.ID.ToString();
            //UpdateCategory.SelectedItem = product.Category;
            //UpdateName.Text = product.Name;
            //UpdatePrice.Text = product.Price.ToString();
            //UpdateOrAdd.IsEnabled= false;
            productAddOrUp.DataContext = product;
        }

        public AddOrUpdateProduct(IBl BL)
        {
            InitializeComponent();
            bl = BL;
            UpdateCategory.ItemsSource = Enum.GetValues(typeof(BO.Category));
            UpdateOrAdd.Content = "הוסף";
            //אם הגענו לבנאי הריק, סימן שבאנו לחלון של הוספה
            UpdateID.IsEnabled = true;
            //נותנים אפשרות להכניס את המזהה של המוצר
            //UpdateOrAdd.IsEnabled = false;
        }
        private void changeImageButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog f = new Microsoft.Win32.OpenFileDialog();
            f.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            if (f.ShowDialog() == true)
            {
                ProductImage.Source = new BitmapImage(new Uri(f.FileName));
                path = (ProductImage.Source).ToString();
            }
        }
        private void UpdateOrAdd_Click(object sender, RoutedEventArgs e)
        {
            //אם לא סיימו להכניס ערכים לכל השדות נפתחת תיבת מסר עם אזהרה
            if (UpdateID.Text.Length == 0 || UpdateCategory.SelectedItem == null || UpdateName.Text.Length == 0
                || UpdatePrice.Text.Length == 0 || UpdateInStock.Text.Length == 0)
            {
                MessageBox.Show("Please check all fields are complete", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                return;
            }
            try
            {
                if ((string)UpdateOrAdd.Content == "הוסף") //אם אנחנו במקרה של הוספה
                    bl.Product.AddProduct(new()
                    {
                        ID = int.Parse(UpdateID.Text),
                        Name = UpdateName.Text,
                        Category = (BO.Category)UpdateCategory.SelectedItem,
                        Price = int.Parse(UpdatePrice.Text),
                        InStock = int.Parse(UpdateInStock.Text),
                        IsDeleted = false,
                        Path= path,
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

        private void IsEnabled_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (UpdateID.Text.Length == 0 || UpdateCategory.SelectedItem == null || UpdateName.Text.Length == 0
                || UpdatePrice.Text.Length == 0 || UpdateInStock.Text.Length == 0)
                return;
            UpdateOrAdd.IsEnabled = true;   
        }

        private void EntersOnlyNumbers(object sender, KeyEventArgs e) => Tools.EnterNumbersOnly(sender, e);

        private void UpdateCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UpdateID.Text.Length == 0 || UpdateCategory.SelectedItem == null || UpdateName.Text.Length == 0
                  || UpdatePrice.Text.Length == 0 || UpdateInStock.Text.Length == 0)
                return;
            UpdateOrAdd.IsEnabled = true;
        }
    }
}
