﻿using System;
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
public partial class UpdateProduct : Window
{
    private IBl bl = BlFactory.GetBl();
    public UpdateProduct()
    {
        InitializeComponent();
        UpdateCategory.ItemsSource = Enum.GetValues(typeof(BO.Category));
        UpdateOrAdd.Content = "Add";
        UpdateID.IsEnabled = true;
    }

    private void AddIdValidation(object sender, KeyEventArgs e) => Tools.EnterNumbersOnly(sender, e);

    private void AddPriceValidation(object sender, KeyEventArgs e) => Tools.EnterNumbersOnly(sender, e);

    private void AddInStockValidation(object sender, KeyEventArgs e) => Tools.EnterNumbersOnly(sender, e);

    public UpdateProduct(BO.ProductForList product)
    {
        InitializeComponent();
        UpdateID.IsEnabled = false;
        UpdateOrAdd.Content = "Update";
        UpdateCategory.ItemsSource = Enum.GetValues(typeof(BO.Category));
        UpdateID.Text = product.ID.ToString();
        UpdateCategory.SelectedItem = product.Category;
        UpdateName.Text = product.Name;
        UpdatePrice.Text = product.Price.ToString();
        //UpdateInStock.Text = product.InStock.ToString();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        if ((string)UpdateOrAdd.Content == "Add")
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
            bl.Product.UpdateProductDetails(new()
            {
                ID = int.Parse(UpdateID.Text),
                Name = UpdateName.Text,
                Category = (BO.Category)UpdateCategory.SelectedItem,
                Price = int.Parse(UpdatePrice.Text),
                InStock = int.Parse(UpdateInStock.Text),
                IsDeleted = false,
            });
        Close();
    }

}
