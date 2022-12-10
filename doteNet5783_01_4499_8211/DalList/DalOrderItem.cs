
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
        if (it != null) //checks if the order is already in the system
            if ((bool)it?.IsDeleted!)
                ds.ListOrderItem.RemoveAll(i => item.ID == i?.ID);
        item.ID = DataSource.Config.NextOrderItemNumber;
        ds.ListOrderItem.Add(item);
        return item.ID;//צריך להחזיר פה את התז
    }
    public OrderItem GetById(int id)
    // מקבלת ת"ז ומחזירה את הפריט שז הת"ז שלו
    {
        OrderItem item = ds.ListOrderItem.FirstOrDefault(item => item?.ID == id)
            ?? throw new DoesNotExistException(id);
        if (item.IsDeleted) //checks if the item is already in the store
            throw new DoesNotExistException(id);
        return item;
    }
    public void Update(OrderItem item)
    //מעדכנת הזמנה קיימת
    {
        OrderItem temp = ds.ListOrderItem.FirstOrDefault(found => found?.ID == item.ID)
            ?? throw new DoesNotExistException(item.ID);
        if (item.IsDeleted)
            throw new DoesNotExistException(item.ID);
        ds.ListOrderItem.Remove(temp);
        ds.ListOrderItem.Add(item);
    }
    public void Delete(int id)
    //מוחקת הזמנה מהרשימה לפי הת"ז שהיא מקבלת
    {
        OrderItem found = ds.ListOrderItem.FirstOrDefault(item => item?.ID == id)
            ?? throw new DoesNotExistException(id);
        if (found.IsDeleted)
            //בודק אם ההזמנה לא נמצאת ברשימה, ואם לא נמצאת זורק חריגה
            throw new DoesNotExistException(id);

        OrderItem item = new()
        {
            ID = id,
            ProductID = found.ProductID,
            OrderID = found.OrderID,
            Price = found.Price,
            Amount = found.Amount,
            IsDeleted = true
        };
        Update(item);
    }

    public IEnumerable<OrderItem> GetAll()
    // מחזירה את כל הרשימה, גם את האיברים שכביכול נמחקו
    {
        return (from OrderItem? item in ds.ListOrderItem where item != null select (OrderItem)item).ToList();//???
    }

    public IEnumerable<OrderItem> GetAll(int IdOrder)
    //מחזירה את כל הרשימה של המוצרים בהעתקה עמוקה, אי אפשר לשנות דרכה את הרשימה
    {
        return (from OrderItem? item in ds.ListOrderItem 
                where item?.OrderID == IdOrder 
                select (OrderItem)item)
                .ToList();//ופה?
    }

    public OrderItem getItem(int IdOrder, int IdProduct)
    {
        return ds.ListOrderItem.FirstOrDefault(item => (item?.OrderID == IdOrder) && (item?.ProductID == IdProduct))
            ?? throw new DoesNotExistException(IdOrder);
    }

    public IEnumerable<OrderItem> GetAllProduct(int IdProduct)
    //מקבלת מזהה מוצר ולכל פריט שהוזמן בודקת האם הוא זהה למוצר שהתקבל ואם כן, מחזירה אותו
    {
        return (from OrderItem? item in ds.ListOrderItem 
                where item?.ProductID == IdProduct 
                select (OrderItem)item)
                .ToList();//???
    }

    public IEnumerable<OrderItem> GetAll(Func<OrderItem?, bool>? filter = null)
    {
        return (from OrderItem? orderItem in ds.ListOrderItem where filter!(orderItem) select (OrderItem)orderItem).ToList();//???
    }
}
