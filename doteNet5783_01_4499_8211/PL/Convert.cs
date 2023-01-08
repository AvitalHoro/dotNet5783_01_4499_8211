using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace PL;
public class IntToVisibiltyConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        int listCountValue = (int)value;

        if (listCountValue == 0)
            return Visibility.Hidden;
        else
            return Visibility.Visible;

    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class IntToHiddenConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        int listCountValue = (int)value;

        if (listCountValue == 0)
            return Visibility.Visible;
        else
            return Visibility.Hidden;

    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}


public class StateToBoolConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
       BO.Status state = (BO.Status)value;

        if (state == BO.Status.approved)
            return true;
        else
            return false;
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class CategoryToHebrew : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        BO.Category category = (BO.Category)value;

        if (category == BO.Category.Bottles)
            return "בקבוקים ומוצצים";
        if (category == BO.Category.Carts)
            return "עגלות וטיולונים";
        if (category == BO.Category.Toys)
            return "צעצועים ומשחקים";
        if (category == BO.Category.Clothes)
            return "ביגוד והנעלה ";

            return "היגיינה והחתלה";
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}