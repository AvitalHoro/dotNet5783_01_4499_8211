using DalApi;
using DO;


namespace Dal;

public class DalOrder :IOrder
{
    DataSource ds= DataSource.s_instance;
    //יש פה שורה עם איסטנסט
   public int Add( Order? item)
    {
        ds.ListOrder.Add(item);
        return item.GetValueOrDefault().ID;//צריך להחזיר פה את התז של המוצר
    }
    public Order? GetById(int id)
    {
        return (ds.ListOrder.Find(item => item.GetValueOrDefault().ID == id));
    }
    public void Update(Order? item)
    {
        Order? order= ds.ListOrder.Find(found => found.GetValueOrDefault().ID == item.GetValueOrDefault().ID);
        if (order==null) 
            return;
       int i= ds.ListOrder.IndexOf(order);
       ds.ListOrder[i] = item;
    }
    public  void Delete(int id)
    {
        ds.ListOrder.RemoveAll(item => item.GetValueOrDefault().ID == id);
    }

    //IEnumerable<T?> GetAll(Func<T?, bool>? filter = null);
    public IEnumerable<Order?> GetAll()
    {
        List<Order?> temp = ds.ListOrder;
        return temp;
    }

}
