using System.Collections;
using System.Reflection;
using DO;

namespace BO;

public static class Tools
{

    public static string ToStringProperty<T>(this T t, string suffix = "")
    {
        string str = "";
        foreach (PropertyInfo prop in t!.GetType().GetProperties())
        {
            var value = prop.GetValue(t, null);
            if (value is IEnumerable && value is not string)
            {
                str+= "\n"+ prop.Name + ":";
                foreach (var item in (IEnumerable)value)
                    str += item.ToStringProperty("   ");
            }
            else
                str += "\n" + suffix + prop.Name + ": " + value;
        }
        str += "\n";
        return str;
    }

    public static Target CopyPropTo<Source, Target>(this Source source, Target target)
    {

        if (source is not null && target is not null)
        {
            Dictionary<string, PropertyInfo> propertiesInfoTarget = target.GetType().GetProperties()
                .ToDictionary(p => p.Name, p => p);

            IEnumerable<PropertyInfo> propertiesInfoSource = source.GetType().GetProperties();

            foreach (var propertyInfo in propertiesInfoSource)
            {
                if (propertiesInfoTarget.ContainsKey(propertyInfo.Name)
                    && (propertyInfo.PropertyType == typeof(string) || !(propertyInfo.PropertyType.IsClass)))
                {
                    propertiesInfoTarget[propertyInfo.Name].SetValue(target, propertyInfo.GetValue(source));
                }
            }
        }
        return target;
    }

     public static object CopyPropToStruct<S>(this S from, Type type)//get the typy we want to copy to 
        {
            object? to = Activator.CreateInstance(type); // new object of the Type
            from.CopyPropTo(to);//copy all value of properties with the same name to the new object
            return to!;
        }
}

