using DalApi;
using DO;


namespace Dal;

public class DalOrder :IOrder
{
   public int Add(Order item)
    {
        DataSource.ListOrder.Add(item);
        return item.ID;//צריך להחזיר פה את התז של המוצר
    }
    public Order GetById(int id)
    {
        return (DataSource.ListOrder.Find(item => item.ID == id));
    }
    public void Update(Order item)
    {
        Order order= DataSource.ListOrder.Find(found => found.ID == item.ID);
        //if (order==null) 
        //    return; //לשאול את נורית
        DataSource.ListOrder.update(order);
        DataSource.ListOrder.Add(item);    
    }
    public  void Delete(int id)
    {
        DataSource.ListOrder.RemoveAll(item => item.ID == id);
    }

    //IEnumerable<T?> GetAll(Func<T?, bool>? filter = null);
    public IEnumerable<Order> GetAll()
    {
        return DataSource.ListOrder;
    }

}
