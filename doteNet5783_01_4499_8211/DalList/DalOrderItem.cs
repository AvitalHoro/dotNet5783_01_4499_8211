using DalApi;
using DO;
using System.Data;

namespace Dal;

public class DalOrderItem : IOrderItem
//מממשת את כל הפונקציות שב"IORDER" 
{
    readonly DataSource ds = DataSource.s_instance;

    #region Add
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
    #endregion

    #region GetById
    /// <exception cref="DoesNotExistException"></exception>
    public OrderItem GetById(int id)
    // מקבלת ת"ז ומחזירה את הפריט שז הת"ז שלו
    {
        OrderItem item = ds.ListOrderItem.FirstOrDefault(item => item?.ID == id)
            ?? throw new DoesNotExistException(id);
        if (item.IsDeleted) //checks if the item is already in the store
            throw new DoesNotExistException(id);
        return item;
    }
    #endregion

    #region Update
    /// <exception cref="DoesNotExistException"></exception>
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
    #endregion

    #region Delete
    /// <exception cref="DoesNotExistException"></exception>
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
    #endregion

    #region GetAll
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? filter = null)
    {
        if (filter == null)
            return ds.ListOrderItem;
        return (from OrderItem? orderItem in ds.ListOrderItem 
                where filter!(orderItem) 
                select orderItem)
                .ToList();
    }
    #endregion

    #region GetItem
    /// <exception cref="DoesNotExistException"></exception>
    public OrderItem GetItem(int IdOrder, int IdProduct)
    {
        return ds.ListOrderItem.FirstOrDefault(item => (item?.OrderID == IdOrder) && 
                                               (item?.ProductID == IdProduct))
            ?? throw new DoesNotExistException(IdOrder);
    }
    #endregion
}
