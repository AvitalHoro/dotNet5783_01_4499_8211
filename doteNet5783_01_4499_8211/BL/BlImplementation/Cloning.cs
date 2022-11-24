using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation;

static class Cloning
{
    internal static K  Clone<T, K>(this T original, K copyToObject) where T : new()
    {
        foreach(PropertyInfo propertyInfo in typeof(T).GetProperties())
        propertyInfo.SetValue(copyToObject,propertyInfo.GetValue(original, null), null);   
        return copyToObject;    
    }
}
