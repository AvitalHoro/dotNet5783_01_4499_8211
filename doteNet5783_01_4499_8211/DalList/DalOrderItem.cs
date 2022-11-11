
using DalApi;
using DO;

namespace Dal;

public class DalOrderItem : IOrderItem
{
    public int Add(OrderItem item)
    {
        DataSource.ListOrderItem.Add(item);
        return 0;
    }
    public OrderItem GetById(int id)
    {
        return (DataSource.ListOrderItem.Find(item => item.ID == id));
    }
    public void Update(OrderItem item)
    {
        OrderItem temp = DataSource.ListOrderItem.Find(found => found.ID == item.ID);
        //if (order==null) 
        //    return; //לשאול את נורית
        DataSource.ListOrderItem.Remove(temp);
        DataSource.ListOrderItem.Add(item);
    }
    public void Delete(int id)
    {
        DataSource.ListOrderItem.RemoveAll(item => item.ID == id);
    }

    //IEnumerable<T?> GetAll(Func<T?, bool>? filter = null)
 
    public IEnumerable<OrderItem> GetAll()
    {
        return DataSource.ListOrderItem;
    }

    public IEnumerable<OrderItem> GetAll(int IdOrder)
    {
       return  DataSource.ListOrderItem.FindAll(item=>item.ID==IdOrder);
    }

    public OrderItem getItem(int IdOrder, int IdItem)
    {
        return DataSource.ListOrderItem.Find(item => item.ID == IdOrder);
    }
}
