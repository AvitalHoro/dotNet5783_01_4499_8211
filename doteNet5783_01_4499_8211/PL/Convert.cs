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

public class CategoryToHebrewConverter : IValueConverter
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

public class StateToHebrewConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        BO.Status status = (BO.Status)value;
        if (status == BO.Status.approved)
            return "אושרה";
        if (status == BO.Status.sent)
            return "נשלחה";

        return "נמסרה";
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class ApprovedToVisibileConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        BO.Status state = (BO.Status)value;

        if (state == BO.Status.approved || state == BO.Status.sent || state == BO.Status.delivered)
            return Visibility.Visible;
        else
            return Visibility.Hidden;
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class ApprovedToHiddenConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        BO.Status state = (BO.Status)value;

        if (state == BO.Status.approved || state == BO.Status.sent || state == BO.Status.delivered)
            return Visibility.Hidden;
        else
            return Visibility.Visible;
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class SentToVisibileConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        BO.Status state = (BO.Status)value;

        if (state == BO.Status.sent || state == BO.Status.delivered)
            return Visibility.Visible;
        else
            return Visibility.Hidden;
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class SentToHiddenConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        BO.Status state = (BO.Status)value;
        string isAdmin = (string)parameter;

        if (isAdmin == "true" && state == BO.Status.approved)
            return Visibility.Visible;
        else
            return Visibility.Hidden;
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class DeliveredToVisibileConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        BO.Status state = (BO.Status)value;

        if (state == BO.Status.delivered)
            return Visibility.Visible;
        else
            return Visibility.Hidden;
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
public class DeliveredToHiddenConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        BO.Status state = (BO.Status)value;
        string isAdmin = (string)parameter;

        if (isAdmin == "true" && state != BO.Status.delivered)
            return Visibility.Visible;
        else
            return Visibility.Hidden;
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class StateToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        BO.Status state = (BO.Status)value;

        if (state == BO.Status.approved)
            return "#FFBCCCEA";
        if (state == BO.Status.sent)
            return "#FF5F82C5";
        return "#FF294F99";
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class TruckConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        BO.Status state = (BO.Status)value;

        if (state == BO.Status.approved)
            return "60,0,0,0";
        if (state == BO.Status.sent)
            return "0,0,0,0";
        return "-60,0,0,0";
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class IntToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        int num = (int)value;

        return num.ToString();
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class StringToVisibleConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string str = (string)value;
        if(str == "מוצרים בארכיון")
            return Visibility.Collapsed;
        else
            return Visibility.Visible;
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class StringToCollapsedConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string str = (string)value;
        if (str == "מוצרים בארכיון")
            return Visibility.Visible;
        else
            return Visibility.Collapsed;
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class UpdateContentToHiddenConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string str = (string)value;
        if (str == "הוספת מוצר")
            return Visibility.Visible;
        else
            return Visibility.Collapsed;
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class StringContentToTitleConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string str = (string)value;
        if (str == "מוצרים בארכיון")
            return "מוצרים בחנות";
        else
            return "ארכיון מוצרים";
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class  PrecentToThicknessConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        BO.Status state = (BO.Status)value;
        Random num = new Random();
        double precent = 0;

        if (state == BO.Status.approved)
            precent = num.Next(10, 30);
        if (state == BO.Status.sent)
            precent = num.Next(50, 70);
        if (state == BO.Status.delivered)
            precent = 100;
        return precent; 
        //return new Thickness(precent, 0, 0, 0);
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class AllFieldsAreFullToEnabled : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        bool flag = true;
        foreach(var val in values)  
        {
            if(string.IsNullOrEmpty(val as string))
                flag = false;
        }
        return flag;
    }
    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
public class CouponToDiscountConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string cupon = (string)value;
        if (cupon == "AVITAL")
            return "15%";
        if (cupon == "REUT")
            return "25%";
        return "";
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class SumToFreightCostConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string val = (string)value;
        double sum = double.Parse(val);

        if (sum < 299)
            return ("₪"+ (sum +29).ToString());
        else return ("₪"+ sum.ToString());
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class SumToFreightConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string val = (string)value;
        double sum = double.Parse(val);

        if (sum < 299)
            return "₪29";
        else return "₪0";
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
