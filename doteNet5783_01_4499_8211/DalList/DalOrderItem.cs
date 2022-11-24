
using DalApi;
using DO;
using System.Data;

namespace Dal;

public class DalOrderItem : IOrderItem
//מממשת את כל הפונקציות שב"IORDER" 
{
    DataSource ds = DataSource.s_instance;

    public int Add(OrderItem? item)
        //מוסיפה הזמנה חדשה לרשימה
    {
        if (ds.ListOrder.Find(i => item.GetValueOrDefault().ID == i.GetValueOrDefault().ID) != null) //checks if the order is already in the system
            throw new AlreadyExistsException(item.GetValueOrDefault().ID);
        ds.ListOrderItem.Add(item);
        return item.GetValueOrDefault().ID;//צריך להחזיר פה את התז
    }
    public OrderItem? GetById(int id)
        // מקבלת ת"ז ומחזירה את ההזמנה שז הת"ז שלה
    {
        if (ds.ListProduct.Find(item => item.GetValueOrDefault().ID == id) == null) //checks if the product is already in the store
            throw new DontExistException(id);
        return (ds.ListOrderItem.Find(item => item.GetValueOrDefault().ID == id));
    }
    public void Update(OrderItem? item)
        //מעדכנת הזמנה קיימת
    {
        OrderItem? temp = ds.ListOrderItem.Find(found => found.GetValueOrDefault().ID == item.GetValueOrDefault().ID);
        if (temp==null)
            throw new DontExistException(item.GetValueOrDefault().ID);
        ds.ListOrderItem.Remove(temp);
        ds.ListOrderItem.Add(item);
    }
    public void Delete(int id)
        //מוחקת הזמנה מהרשימה לפי הת"ז שהיא מקבלת
    {
        OrderItem? found = ds.ListOrderItem.Find(item => item.GetValueOrDefault().ID == id);
        if (found == null)
            //בודק אם ההזמנה לא נמצאת ברשימה, ואם לא נמצאת זורק חריגה
            throw new DontExistException(id);

        OrderItem item = new OrderItem
        {
            ID = id,
            ProductID=found.GetValueOrDefault().ProductID,
            OrderID= found.GetValueOrDefault().OrderID,
            Price = found.GetValueOrDefault().Price,
            Amount = found.GetValueOrDefault().Amount,  
            isDeleted = true
        };
        Update(item);
    }

    //IEnumerable<T?> GetAll(Func<T?, bool>? filter = null)
 
    public IEnumerable<OrderItem?> GetAll()
      // מחזירה את כל הרשימה, גם את האיברים שכביכול נמחקו
    {
        List<OrderItem?> newListOrderItem = new List<OrderItem?> { };
        foreach (OrderItem item in ds.ListOrderItem) { newListOrderItem.Add(item); };
        return newListOrderItem;
    }

    public IEnumerable<OrderItem?> GetAll(int IdOrder)
    //מחזירה את כל הרשימה של המוצרים בהעתקה עמוקה, אי אפשר לשנות דרכה את הרשימה
    {
        return (from OrderItem? order in ds.ListOrderItem where order.GetValueOrDefault().OrderID == IdOrder select order).ToList();
    }

    public OrderItem? getItem(int IdOrder, int IdProduct)
    { 
        return ds.ListOrderItem.Find(item => (item.GetValueOrDefault().OrderID == IdOrder)&& (item.GetValueOrDefault().ProductID== IdProduct));
    }

    public IEnumerable<OrderItem?> GetAllProduct (int IdProduct)
     //מקבלת מזהה מוצר ולכל פריט שהוזמן בודקת האם הוא זהה למוצר שהתקבל ואם כן, מחזירה אותו
    {
        return (from OrderItem? order in ds.ListOrderItem where order.GetValueOrDefault().ProductID == IdProduct select order).ToList();
    }
}
