
using DalApi;
using DO;

namespace Dal;

public class DalOrderItem : IOrderItem
//realizes all the methods of the order items
{
    DataSource ds = DataSource.s_instance;

    public int Add(OrderItem? item)
    {
        if (ds.ListOrder.Find(i => item.GetValueOrDefault().ID == i.GetValueOrDefault().ID) != null) //checks if the order is already in the system
            throw new AlreadyExistsException(item.GetValueOrDefault().ID);
        ds.ListOrderItem.Add(item);
        return ds.ListOrderItem.Count();//צריך להחזיר פה את התז
    }
    public OrderItem? GetById(int id)
    {
        if (ds.ListProduct.Find(item => item.GetValueOrDefault().ID == id) == null) //checks if the product is already in the store
            throw new DontExitException(id);
        return (ds.ListOrderItem.Find(item => item.GetValueOrDefault().ID == id));
    }
    public void Update(OrderItem? item)
    {
        OrderItem? temp = ds.ListOrderItem.Find(found => found.GetValueOrDefault().ID == item.GetValueOrDefault().ID);
        if (temp==null)
            throw new DontExitException(item.GetValueOrDefault().ID);
        ds.ListOrderItem.Remove(temp);
        ds.ListOrderItem.Add(item);
    }
    public void Delete(int id)
    {
        if (ds.ListProduct.Find(item => item.GetValueOrDefault().ID == id) == null) //checks if the product is already in the store
            throw new DontExitException(id);
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
