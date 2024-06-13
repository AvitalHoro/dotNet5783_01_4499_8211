using System.Collections;
using System.Reflection;
using DO;

namespace BO;

public static class Tools
{
    // Function that creates a string for printing entities based on their properties
    // Checks for each property its name and value, adds everything to a string, and returns the string
    public static string ToStringProperty<T>(this T t, string suffix = "")
    {
        string str = "";
        foreach (PropertyInfo prop in t!.GetType().GetProperties())
        {
            var value = prop.GetValue(t, null);
            if (value is IEnumerable && value is not string)
                // If we received a collection, we need to iterate over each object in the collection and apply this function to it as well
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

    // Extension function that copies fields with similar names from a source object to another object
    public static Target CopyPropTo<Source, Target>(this Source source, Target target)
    {

        if (source is not null && target is not null) // If both objects are not null
        {
            Dictionary<string, PropertyInfo> propertiesInfoTarget = target.GetType().GetProperties()
                .ToDictionary(p => p.Name, p => p); // Creates a dictionary of pairs with the field name and its value

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

    // The function takes a source object from which we want to copy and the type of the object we want to copy to
    public static object CopyPropToStruct<S>(this S from, Type type)// Get the type we want to copy to
    {
        object? to = Activator.CreateInstance(type); // New object of the Type
        from.CopyPropTo(to); // Copy all values of properties with the same name to the new object
        return to!; // Returns the new object
    }
}