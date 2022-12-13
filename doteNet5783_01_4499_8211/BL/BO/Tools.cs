using System.Collections;
using System.Reflection;
using DO;

namespace BO;

public static class Tools
{
    //פונקציה שיוצרת מחרוזת להדפסה של ישויות לפי הפרופרטיז שלהם
    //בודקת לכל פרופרטי מה השם שלו ומה הערך שבתוכו, מכניסה הכל למחרוזת ומחזיר את המחרוזת
    public static string ToStringProperty<T>(this T t, string suffix = "")
    {
        string str = "";
        foreach (PropertyInfo prop in t!.GetType().GetProperties())
        {
            var value = prop.GetValue(t, null);
            if (value is IEnumerable && value is not string)
                //אם קיבלנו אוסף צריך לעבור על כל עצם באוסף ולהפעיל עליו גם את הפונקציה הזאת
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

    //פונקצית הרחבה שמעתיקה שדות עם שם דומה מעצם מקור לעצם אחר  
    public static Target CopyPropTo<Source, Target>(this Source source, Target target)
    {

        if (source is not null && target is not null) //אם שני העצמים לא ריקים
        {
            Dictionary<string, PropertyInfo> propertiesInfoTarget = target.GetType().GetProperties()
                .ToDictionary(p => p.Name, p => p); //יוצר מילון של צמדים עם שם של שדה והערך בו

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

    //הפונקציה מקבלת עצם מקור שממנו אנו רוצים להעתיק ואת הסוג של העצם שאליו אנחנו רוצים להעתיק
     public static object CopyPropToStruct<S>(this S from, Type type)//get the typy we want to copy to 
     {
            object? to = Activator.CreateInstance(type); // new object of the Type
            from.CopyPropTo(to);//copy all value of properties with the same name to the new object 
            return to!; //מחזירה את העצם אובגקט החדש
     }
}

