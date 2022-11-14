using DalApi;
using DO;


namespace Dal;

public class DalOrder :IOrder
{
   public int Add(Order? item)
    {
        DataSource.ListOrder.Add(item);
        return item.GetValueOrDefault().ID;//צריך להחזיר פה את התז של המוצר
    }
    public Order? GetById(int id)
    {
        return (DataSource.ListOrder.Find(item => item.GetValueOrDefault().ID == id));
    }
    public void Update(Order? item)
    {
        Order? order= DataSource.ListOrder.Find(found => found.GetValueOrDefault().ID == item.GetValueOrDefault().ID);
        if (order==null) 
            return;
       int i= DataSource.ListOrder.IndexOf(order);
       DataSource.ListOrder[i] = item;
    }
    public  void Delete(int id)
    {
        DataSource.ListOrder.RemoveAll(item => item.GetValueOrDefault().ID == id);
    }

    //IEnumerable<T?> GetAll(Func<T?, bool>? filter = null);
    public IEnumerable<Order?> GetAll()
    {
        List<Order?> temp = DataSource.ListOrder;
        return temp;
    }

}
