using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BO;


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
public class InvalidIDException : Exception
//חריגה שנזרקת כאשר מנסים לגשת לאיבר שלא נמצא בחנות
{
    public int ID { get; private set; } //ת"ז של ישות, בכדי שנוכל לכתוב למשתמש לאיזו ת"ז אנו מתכוונים
    public InvalidIDException(int id) : base() { ID = id; }
    //זריקה שנזרקת עם ת"ז של ישות ספציפית
    public InvalidIDException(int id, string message) : base(message) { ID = id; }
    //זריקה שנזרקת עם ת"ז והודעה ספציפית שנרצה לזרוק
    public InvalidIDException(int id, string message, Exception inner) : base(message, inner) { ID = id; }
    //
    protected InvalidIDException(int id, SerializationInfo info, StreamingContext context) : base(info, context) { ID = id; }
    override public string ToString() => "InvalidIDException: The ID " + ID + " invalid";
    //הדפסה של השגיאה לפי הנתונים שקיבלנו
}

[Serializable]
public class InvalidPriceException : Exception
//חריגה שנזרקת כאשר מנסים לגשת לאיבר שלא נמצא בחנות
{
    public int ID { get; private set; } //ת"ז של ישות, בכדי שנוכל לכתוב למשתמש לאיזו ת"ז אנו מתכוונים
    public InvalidPriceException(int id) : base() { ID = id; }
    //זריקה שנזרקת עם ת"ז של ישות ספציפית
    public InvalidPriceException(int id, string message) : base(message) { ID = id; }
    //זריקה שנזרקת עם ת"ז והודעה ספציפית שנרצה לזרוק
    public InvalidPriceException(int id, string message, Exception inner) : base(message, inner) { ID = id; }
    //
    protected InvalidPriceException(int id, SerializationInfo info, StreamingContext context) : base(info, context) { ID = id; }
    override public string ToString() => "InvalidPriceException: The price of the product with the ID " + ID + " invalid";
    //הדפסה של השגיאה לפי הנתונים שקיבלנו
}

[Serializable]
public class OutOfStockException : Exception
//חריגה שנזרקת כאשר מנסים לגשת לאיבר שלא נמצא בחנות
{
    public int ID { get; private set; } //ת"ז של ישות, בכדי שנוכל לכתוב למשתמש לאיזו ת"ז אנו מתכוונים
    public OutOfStockException(int id) : base() { ID = id; }
    //זריקה שנזרקת עם ת"ז של ישות ספציפית
    public OutOfStockException(int id, string message) : base(message) { ID = id; }
    //זריקה שנזרקת עם ת"ז והודעה ספציפית שנרצה לזרוק
    public OutOfStockException(int id, string message, Exception inner) : base(message, inner) { ID = id; }
    //
    protected OutOfStockException(int id, SerializationInfo info, StreamingContext context) : base(info, context) { ID = id; }
    override public string ToString() => "OutOfStockException: The product with the ID " + ID + " out of stock";
    //הדפסה של השגיאה לפי הנתונים שקיבלנו
}

[Serializable]
public class NoNameException : Exception
//חריגה שנזרקת כאשר מנסים לגשת לאיבר שלא נמצא בחנות
{
    public int ID { get; private set; } //ת"ז של ישות, בכדי שנוכל לכתוב למשתמש לאיזו ת"ז אנו מתכוונים
    public NoNameException(int id) : base() { ID = id; }
    //זריקה שנזרקת עם ת"ז של ישות ספציפית
    public NoNameException(int id, string message) : base(message) { ID = id; }
    //זריקה שנזרקת עם ת"ז והודעה ספציפית שנרצה לזרוק
    public NoNameException(int id, string message, Exception inner) : base(message, inner) { ID = id; }
    //
    protected NoNameException(int id, SerializationInfo info, StreamingContext context) : base(info, context) { ID = id; }
    override public string ToString() => "NoNameException: The name of the product with the ID " + ID + " doesn't exist";
    //הדפסה של השגיאה לפי הנתונים שקיבלנו
}

[Serializable]
public class ProductExistInOrderException : Exception
//חריגה שנזרקת כאשר מנסים לגשת לאיבר שלא נמצא בחנות
{
    public int ID { get; private set; } //ת"ז של ישות, בכדי שנוכל לכתוב למשתמש לאיזו ת"ז אנו מתכוונים
    public ProductExistInOrderException(int id) : base() { ID = id; }
    //זריקה שנזרקת עם ת"ז של ישות ספציפית
    public ProductExistInOrderException(int id, string message) : base(message) { ID = id; }
    //זריקה שנזרקת עם ת"ז והודעה ספציפית שנרצה לזרוק
    public ProductExistInOrderException(int id, string message, Exception inner) : base(message, inner) { ID = id; }
    //
    protected ProductExistInOrderException(int id, SerializationInfo info, StreamingContext context) : base(info, context) { ID = id; }
    override public string ToString() => "ProductExistInOrderException: The product with the ID " + ID + " cannot be deleted beacuse it has been ordered";
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
    override public string ToString() => "AlreadyExistsException: The ID " + ID + " already exists in the system.";
    //הדפסה של השגיאה לפי הנתונים שקיבלנו
}

[Serializable]
public class ProductNotExistInCartException : Exception
//הערה זו נזרקת כאשר מנסים להוסיף איבר שכבר נמצא במערך
{
    public int ID { get; private set; } //ת"ז של ישות, בכדי שנוכל לכתוב למשתמש לאיזו ת"ז אנו מתכוונים
    public ProductNotExistInCartException(int id) : base() { ID = id; }
    //זריקה שנזרקת עם ת"ז של ישות ספציפית
    public ProductNotExistInCartException(int id, string message) : base(message) { ID = id; }
    //זריקה שנזרקת עם ת"ז והודעה ספציפית שנרצה לזרוק
    public ProductNotExistInCartException(int id, string message, Exception inner) : base(message, inner) { ID = id; }
    protected ProductNotExistInCartException(int id, SerializationInfo info, StreamingContext context) : base(info, context) { ID = id; }
    override public string ToString() => "ProductNotExistInCartException: The product with the ID " + ID + " does not exist in your cart.";
    //הדפסה של השגיאה לפי הנתונים שקיבלנו
}

[Serializable]
public class NoCostumerNameException : Exception
//הערה זו נזרקת כאשר מנסים להוסיף איבר שכבר נמצא במערך
{
    public NoCostumerNameException() : base() {}
    //זריקה שנזרקת עם ת"ז של ישות ספציפית
    public NoCostumerNameException(int id, string message) : base(message) {}
    //זריקה שנזרקת עם ת"ז והודעה ספציפית שנרצה לזרוק
    public NoCostumerNameException(int id, string message, Exception inner) : base(message, inner) {}
    protected NoCostumerNameException(int id, SerializationInfo info, StreamingContext context) : base(info, context) {}
    override public string ToString() => "NoCostumerNameException: We dont have your name";
    //הדפסה של השגיאה לפי הנתונים שקיבלנו
}

[Serializable]
public class NoCostumerAdressException : Exception
//הערה זו נזרקת כאשר מנסים להוסיף איבר שכבר נמצא במערך
{
    public NoCostumerAdressException() : base() { }
    //זריקה שנזרקת עם ת"ז של ישות ספציפית
    public NoCostumerAdressException(int id, string message) : base(message) { }
    //זריקה שנזרקת עם ת"ז והודעה ספציפית שנרצה לזרוק
    public NoCostumerAdressException(int id, string message, Exception inner) : base(message, inner) { }
    protected NoCostumerAdressException(int id, SerializationInfo info, StreamingContext context) : base(info, context) { }
    override public string ToString() => "NoCostumerAdressException: We don't have your adress";
    //הדפסה של השגיאה לפי הנתונים שקיבלנו
}

[Serializable]
public class NoCostumerEmailException : Exception
//הערה זו נזרקת כאשר מנסים להוסיף איבר שכבר נמצא במערך
{
    public NoCostumerEmailException() : base() { }
    //זריקה שנזרקת עם ת"ז של ישות ספציפית
    public NoCostumerEmailException(int id, string message) : base(message) { }
    //זריקה שנזרקת עם ת"ז והודעה ספציפית שנרצה לזרוק
    public NoCostumerEmailException(int id, string message, Exception inner) : base(message, inner) { }
    protected NoCostumerEmailException(int id, SerializationInfo info, StreamingContext context) : base(info, context) { }
    override public string ToString() => "NoCostumerEmailException: We don't have your email";
    //הדפסה של השגיאה לפי הנתונים שקיבלנו
}

[Serializable]
public class AmountException : Exception
//הערה זו נזרקת כאשר מנסים להוסיף איבר שכבר נמצא במערך
{
    public AmountException() : base() { }
    //זריקה שנזרקת עם ת"ז של ישות ספציפית
    public AmountException(int id, string message) : base(message) { }
    //זריקה שנזרקת עם ת"ז והודעה ספציפית שנרצה לזרוק
    public AmountException(int id, string message, Exception inner) : base(message, inner) {}
    protected AmountException(int id, SerializationInfo info, StreamingContext context) : base(info, context) {}
    override public string ToString() => "AmountException: Negative or incorrect amount";
    //הדפסה של השגיאה לפי הנתונים שקיבלנו
}