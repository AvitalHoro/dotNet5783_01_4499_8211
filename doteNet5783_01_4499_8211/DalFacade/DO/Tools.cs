using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DO;

public static class Tools
{
    public static void PrintProperty<T>(T t)
    {
        Type Ttype = t.GetType();
        PropertyInfo[] info = Ttype.GetProperties();
        foreach (PropertyInfo item in info)
        {
            Console.WriteLine
            ("name: {0,-15} value: {1,-15}"
                , item.Name, item.GetValue(t, null));
        }
    }
}
