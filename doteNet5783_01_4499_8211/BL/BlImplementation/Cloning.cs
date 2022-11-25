using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation;

static class Cloning
{
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
    //internal static K  Clone<T, K>(this T original, K copyToObject) where T : new()
    //{
    //    foreach(PropertyInfo propertyInfo in typeof(T).GetProperties())
    //    propertyInfo.SetValue(copyToObject,propertyInfo.GetValue(original, null), null);   
    //    return copyToObject;    
    //}
}

//אולי פשוט לכתוב הרבה פונקציות של המרה לכל סוג של המרה?