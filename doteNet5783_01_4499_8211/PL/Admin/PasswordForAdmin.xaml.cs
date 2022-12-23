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

namespace PL.Admin;
/// <summary>
/// Interaction logic for PasswordForAdmin.xaml
/// </summary>
public partial class PasswordForAdmin : Page
{ 
    IBl bl;
    Frame frame;

    public PasswordForAdmin(IBl BL, Frame frameHome)
    {
        InitializeComponent();
        bl=BL;
        frame = frameHome;
    }

    private void ManagerlogInWithPassword_Click(object sender, RoutedEventArgs e)
    {
        EnterPassword();
    }

    private void EnterPressed_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter) EnterPassword();
    }

    private void EnterPassword()
    {
        if (PasswordBox.Password == "1234")
        {
            PasswordBox.Password = "";
            frame.Content = new AdminPage(bl);
        }
    }
}

