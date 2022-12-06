
using DalApi;
using DO;
using System.Data;

namespace Dal;

public class DalOrderItem : IOrderItem
//מממשת את כל הפונקציות שב"IORDER" 
{
    readonly DataSource ds = DataSource.s_instance;

    public int Add(OrderItem item)
    //מוסיפה הזמנה חדשה לרשימה
    {
        OrderItem? it = ds.ListOrderItem.FirstOrDefault(i => item.ID == i?.ID);
        if (it != null && !item.isDeleted) //checks if the order is already in the system
            throw new AlreadyExistsException(item.ID);
        ds.ListOrderItem.Add(item);
        return item.ID;//צריך להחזיר פה את התז
    }
    public OrderItem GetById(int id)
    // מקבלת ת"ז ומחזירה את הפריט שז הת"ז שלו
    {
        OrderItem item = ds.ListOrderItem.FirstOrDefault(item => item?.ID == id)
            ?? throw new DoesNotExistException(id);
        if (item.isDeleted) //checks if the item is already in the store
            throw new DoesNotExistException(id);
        return item;
    }
    public void Update(OrderItem item)
    //מעדכנת הזמנה קיימת
    {
        OrderItem temp = ds.ListOrderItem.FirstOrDefault(found => found?.ID == item.ID)
            ?? throw new DoesNotExistException(item.ID);
        if (item.isDeleted)
            throw new DoesNotExistException(item.ID);
        ds.ListOrderItem.Remove(temp);
        ds.ListOrderItem.Add(item);
    }
    public void Delete(int id)
    //מוחקת הזמנה מהרשימה לפי הת"ז שהיא מקבלת
    {
        OrderItem found = ds.ListOrderItem.FirstOrDefault(item => item?.ID == id)
            ?? throw new DoesNotExistException(id); ;
        if (found.isDeleted)
            //בודק אם ההזמנה לא נמצאת ברשימה, ואם לא נמצאת זורק חריגה
            throw new DoesNotExistException(id);

        OrderItem item = new()
        {
            ID = id,
            ProductID = found.ProductID,
            OrderID = found.OrderID,
            Price = found.Price,
            Amount = found.Amount,
            isDeleted = true
        };
        Update(item);
    }

    public IEnumerable<OrderItem?> GetAll()
    // מחזירה את כל הרשימה, גם את האיברים שכביכול נמחקו
    {
        return ds.ListOrderItem;
    }

    public IEnumerable<OrderItem?> GetAll(int IdOrder)
    //מחזירה את כל הרשימה של המוצרים בהעתקה עמוקה, אי אפשר לשנות דרכה את הרשימה
    {
        return (from OrderItem? order in ds.ListOrderItem where order.GetValueOrDefault().OrderID == IdOrder select order).ToList();
    }

    public OrderItem? getItem(int IdOrder, int IdProduct)
    {
        return ds.ListOrderItem.Find(item => (item.GetValueOrDefault().OrderID == IdOrder) && (item.GetValueOrDefault().ProductID == IdProduct));
    }

    public IEnumerable<OrderItem?> GetAllProduct(int IdProduct)
    //מקבלת מזהה מוצר ולכל פריט שהוזמן בודקת האם הוא זהה למוצר שהתקבל ואם כן, מחזירה אותו
    {
        return (from OrderItem? order in ds.ListOrderItem where order.GetValueOrDefault().ProductID == IdProduct select order).ToList();
    }

    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? filter = null)
    {
        return (from OrderItem? orderItem in ds.ListOrderItem where filter!(orderItem) select orderItem).ToList();
    }
}
