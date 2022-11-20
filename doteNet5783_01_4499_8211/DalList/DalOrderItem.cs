
using DalApi;
using DO;

namespace Dal;

public class DalOrderItem : IOrderItem
//מממשת את כל הפונקציות שב"IORDER" 
{
    DataSource ds = DataSource.s_instance;

    public int Add(OrderItem? item)
        //מוסיפה הזמנה חדשה לרשימה
    {
        ds.ListOrderItem.Add(item);
        return ds.ListOrderItem.Count();//צריך להחזיר פה את התז
    }
    public OrderItem? GetById(int id)
        // מקבלת ת"ז ומחזירה את ההזמנה שז הת"ז שלה
    {
        return (ds.ListOrderItem.Find(item => item.GetValueOrDefault().ID == id));
    }
    public void Update(OrderItem? item)
        //מעדכנת הזמנה קיימת
    {
        OrderItem? temp = ds.ListOrderItem.Find(found => found.GetValueOrDefault().ID == item.GetValueOrDefault().ID);
        if (temp==null) 
             return;
        ds.ListOrderItem.Remove(temp);
        ds.ListOrderItem.Add(item);
    }
    public void Delete(int id)
    {
        ds.ListOrderItem.RemoveAll(item => item.GetValueOrDefault().ID == id);
    }

    //IEnumerable<T?> GetAll(Func<T?, bool>? filter = null)
 
    public IEnumerable<OrderItem?> GetAll()
    {
        List < OrderItem?> temp = ds.ListOrderItem;
        return temp;
    }

    public IEnumerable<OrderItem?> GetAll(int IdOrder)
    {
        List<OrderItem?> temp = ds.ListOrderItem.FindAll(item => item.GetValueOrDefault().ID == IdOrder);
        return temp;
    }

    public OrderItem? getItem(int IdOrder, int IdItem)
    {
        return ds.ListOrderItem.Find(item => item.GetValueOrDefault().ID == IdOrder);
    }
}
