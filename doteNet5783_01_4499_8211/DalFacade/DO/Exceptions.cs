using System;
using System.Runtime.Serialization;

// Exceptions of all objects

namespace DO;

[Serializable]
public class DoesNotExistException : Exception
    //חריגה שנזרקת כאשר מנסים לגשת לאיבר שלא נמצא בחנות
{
    public int ID { get; private set; } //ת"ז של ישות, בכדי שנוכל לכתוב למשתמש לאיזו ת"ז אנו מתכוונים
    public DoesNotExistException(int id) : base() { ID = id; }
    //זריקה שנזרקת עם ת"ז של ישות ספציפית
    public DoesNotExistException(int id, string message) : base(message) { ID = id; } 
    //זריקה שנזרקת עם ת"ז והודעה ספציפית שנרצה לזרוק
    public DoesNotExistException(int id, string message, Exception inner) : base(message, inner) { ID = id; }
    //
    protected DoesNotExistException(int id, SerializationInfo info, StreamingContext context) : base(info, context) { ID = id; }
    override public string ToString() => "DontExistException: The ID " + ID + " does not exist in the system.";
    //הדפסה של השגיאה לפי הנתונים שקיבלנו
}

[Serializable]
public class AlreadyExistsException : Exception
    //הערה זו נזרקת כאשר מנסים להוסיף איבר שכבר נמצא במערך
{
    public int ID { get; private set; } //ת"ז של ישות, בכדי שנוכל לכתוב למשתמש לאיזו ת"ז אנו מתכוונים
    public AlreadyExistsException(int id) : base() { ID = id; }
    //זריקה שנזרקת עם ת"ז של ישות ספציפית
    public AlreadyExistsException(int id, string message) : base(message) { ID = id; }
    //זריקה שנזרקת עם ת"ז והודעה ספציפית שנרצה לזרוק
    public AlreadyExistsException(int id, string message, Exception inner) : base(message, inner) { ID = id; }
    protected AlreadyExistsException(int id, SerializationInfo info, StreamingContext context) : base(info, context) { ID = id; }
    override public string ToString() => "AlreadyExistsException: The ID " + ID + " already exist in the system.";
    //הדפסה של השגיאה לפי הנתונים שקיבלנו
}


[Serializable]
public class RuningNumberDoesNotExistException : Exception
//חריגה שנזרקת כאשר מנסים לגשת לאיבר שלא נמצא בחנות
{
    public string TypeNumber { get;  private set; } 
    public RuningNumberDoesNotExistException(string type) : base() { TypeNumber = type; }
    //זריקה שנזרקת עם ת"ז של ישות ספציפית
    public RuningNumberDoesNotExistException(string type, string message) : base(message) { TypeNumber = type; }
    //זריקה שנזרקת עם ת"ז והודעה ספציפית שנרצה לזרוק
    public RuningNumberDoesNotExistException(string type, string message, Exception inner) : base(message, inner) { TypeNumber = type; }
    //
    protected RuningNumberDoesNotExistException(string type, SerializationInfo info, StreamingContext context) : base(info, context) { TypeNumber = type; }
    override public string ToString() => "RuningNumberDoesNotExistException: The " + TypeNumber + " does not exist in the system.";
    //הדפסה של השגיאה לפי הנתונים שקיבלנו
}


[Serializable]
public class LoadingException : Exception
{
    string filePath;
    public LoadingException() : base() { filePath = ""; }
    public LoadingException(string message) : base(message) { filePath = ""; }
    public LoadingException(string message, Exception inner) : base(message, inner) { filePath = ""; }

    public LoadingException(string path, string messege, Exception inner) => filePath = path;
    protected LoadingException(SerializationInfo info, StreamingContext context) : base(info, context) { filePath = ""; }
}

[Serializable]
public class DalConfigException : Exception
{
    public DalConfigException(string msg) : base(msg) { }
    public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
}

