﻿using BlApi;
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

    private void EnterNumbersOnly(object sender, KeyEventArgs e)
    {
        TextBox text = sender as TextBox;
        if (text == null) return;
        if (e == null) return;

        //allow get out of the text box
        if (e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Tab)
            return;

        //allow list of system keys (add other key here if you want to allow)
        if (e.Key == Key.Escape || e.Key == Key.Back || e.Key == Key.Delete ||
            e.Key == Key.CapsLock || e.Key == Key.LeftShift || e.Key == Key.Home
         || e.Key == Key.End || e.Key == Key.Insert || e.Key == Key.Down || e.Key == Key.Right)
            return;

        char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);

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
}

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
    private void Button_Click(object sender, RoutedEventArgs e)
    {
        bl.Product.AddProduct(new()
        {
            Name = AddName.Text,
            ID = AddID.Text,
            Category = (BO.Category)SelectCategory.SelectedItem,

        }) ;
    }
}
