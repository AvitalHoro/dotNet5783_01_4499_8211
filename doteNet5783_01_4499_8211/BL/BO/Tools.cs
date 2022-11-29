using DO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public static class Tools
{
    public static string ToStringProperty<T>(this T t)
    {
        string str = "";
        foreach (PropertyInfo property in t.GetType().GetProperties())
        {
            var value = property.GetValue(t, null);
            if (value is IEnumerable)
                foreach (var item in (IEnumerable)value)
                    str += item.ToStringProperty();
            else
                str += "\n" + property.Name +": " + property.GetValue(t, null);
        }

        return str;
    }

    public static void CopyPropTo<Source, Target>(this Source source, Target target)
    {

        if (source is not null && target is not null)
        {
            Dictionary<string, PropertyInfo> propertiesInfoTarget = target.GetType().GetProperties()
                .ToDictionary(p => p.Name, p => p);

            IEnumerable<PropertyInfo> propertiesInfoSource = source.GetType().GetProperties();

            foreach (var propertyInfo in propertiesInfoSource)
            {
                if (propertiesInfoTarget.ContainsKey(propertyInfo.Name)
                    && (propertyInfo.PropertyType == typeof(string) || !propertyInfo.PropertyType.IsClass))
                {
                    propertiesInfoTarget[propertyInfo.Name].SetValue(target, propertyInfo.GetValue(source));
                }
            }
        }
    }

    public static Target CopyPropToStruct<Source, Target>(this Source source, Target target) where Target : struct
    {
        object obj = target;

        source.CopyPropTo(obj);

        return (Target)obj;
    }
}

//public static string ToStringProperty<T>(this T t, string suffix = "")
//{
//    string str = "";
//    foreach (PropertyInfo prop in t.GetType().GetProperties())
//    {
//        var value = prop.GetValue(t, null);
//        if (value is IEnumerable)
//            foreach (var item in (IEnumerable)value)
//                str += item.ToStringProperty("   ");
//        else
//            str += "\n" + suffix + prop.Name + ": " + value;
//    }
//    return str;
//}

