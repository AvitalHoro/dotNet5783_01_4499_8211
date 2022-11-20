using DalApi;
using DO;


namespace Dal;

public class DalOrder :IOrder
//realizes all the methods of the orders
{
    DataSource ds= DataSource.s_instance;
    
   public int Add(Order? item)
    {
        if (ds.ListOrder.Find(o => item.GetValueOrDefault().ID == o.GetValueOrDefault().ID) != null) //checks if the order is already in the system
            throw new AlreadyExistsException(item.GetValueOrDefault().ID);
        ds.ListOrder.Add(item);
        return item.GetValueOrDefault().ID;//צריך להחזיר פה את התז של המוצר
    }
    public Order? GetById(int id)
    {
        if (ds.ListProduct.Find(order => order.GetValueOrDefault().ID == id) == null) //checks if the product is already in the store
            throw new DontExistException(id);
        return (ds.ListOrder.Find(item => item.GetValueOrDefault().ID == id));
    }
    public void Update(Order? item)
    {
        Order? order= ds.ListOrder.Find(found => found.GetValueOrDefault().ID == item.GetValueOrDefault().ID);
        if (order==null)
            throw new DontExistException(item.GetValueOrDefault().ID);
        int i= ds.ListOrder.IndexOf(order);
       ds.ListOrder[i] = item;
    }
    public  void Delete(int id)
    {
        if (ds.ListProduct.Find(order => order.GetValueOrDefault().ID == id) == null) //checks if the product is already in the store
            throw new DontExistException(id);
        ds.ListOrder.RemoveAll(item => item.GetValueOrDefault().ID == id);
    }

    //IEnumerable<T?> GetAll(Func<T?, bool>? filter = null);
    public IEnumerable<Order?> GetAll()
    {
        List<Order?> temp = ds.ListOrder;
        return temp;
    }

}
