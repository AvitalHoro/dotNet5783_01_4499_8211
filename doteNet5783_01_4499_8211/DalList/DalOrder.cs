using DalApi;
using DO;


namespace Dal;

public class DalOrder :IOrder
//מממשת את כל ההמתודות של ההזמנות
{
    DataSource ds= DataSource.s_instance;
    
   public int Add(Order? item)
        //מוסיפה הזמנה חדשה לרשימת ההזמנות
    {
        if (ds.ListOrder.Find(o => item.GetValueOrDefault().ID == o.GetValueOrDefault().ID) != null) 
            //בודק אם ההזמנה כבר נמצאת ברשימה, ואם היא כבר נמצאת זורק חריגה
            throw new AlreadyExistsException(item.GetValueOrDefault().ID); 
        ds.ListOrder.Add(item); //מוסיף את ההזמנה
        return item.GetValueOrDefault().ID;//צריך להחזיר פה את התז של המוצר
    }
    public Order? GetById(int id)
        //מחזיר את ההזמנה שהת"ז שלה זו הת"ז שהפונקציה קיבלה
    {
        if (ds.ListProduct.Find(order => order.GetValueOrDefault().ID == id) == null) //אם ההזמנה לא נמצאת ברשימה, זורק חריגה
            throw new DontExistException(id);
        return (ds.ListOrder.Find(item => item.GetValueOrDefault().ID == id));//מחזיר את ההזמנה
    }
    public void Update(Order? item)
        //מעדכן הזמנה קיימת, מזהה את ההזמנה עפ"י הת"ז
    {
        Order? order= ds.ListOrder.Find(found => found.GetValueOrDefault().ID == item.GetValueOrDefault().ID); //מוציא את ההזמנה מתוך הרשימה 
        if (order==null) //אם ההזמנה שווה לנל זה אומר שההיא לא נמצאת ברשימה, זורקים חריגה
            throw new DontExistException(item.GetValueOrDefault().ID);
        int i= ds.ListOrder.IndexOf(order); //מוצאים את האינדקס של ההזמנה הקודמת
       ds.ListOrder[i] = item; //מכניסים את ההזמנה המעודכנת לאותו המקום של ההזמנה הישנה
    }
    public  void Delete(int id)
        //מוחקת את ההזמנה שהת"ז שלה היא זאת שקיבלנו
    {
        Order? found = ds.ListOrder.Find(item => item.GetValueOrDefault().ID == id);
        if (found == null)
            //בודק אם ההזמנה לא נמצאת ברשימה, ואם לא נמצאת זורק חריגה
            throw new DontExistException(id);

        Order order = new Order
        {
            ID = id,
            CostumerName=found.GetValueOrDefault().CostumerName,
            CostumerEmail=found.GetValueOrDefault().CostumerEmail,  
            CostumerAdress=found.GetValueOrDefault().CostumerAdress,
            OrderDate=found.GetValueOrDefault().OrderDate,  
            DeliveryDate=found.GetValueOrDefault().DeliveryDate,    
            ShipDate=found.GetValueOrDefault().ShipDate,    
            isDeleted = true
        };
        Update(order);
    }

    //IEnumerable<T?> GetAll(Func<T?, bool>? filter = null);
    public IEnumerable<Order?> GetAll()
    //מחזירה את כל הרשימה של המוצרים בהעתקה עמוקה, אי אפשר לשנות דרכה את הרשימה
    {
        List<Order?> newListOrder = new List<Order?> { };
        foreach (Order order in ds.ListOrder) { newListOrder.Add(order); };
        return newListOrder;
    }
}
