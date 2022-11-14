
using DalApi;
using DO;

namespace Dal;

public class DalOrderItem : IOrderItem
{
    public int Add(OrderItem? item)
    {
        DataSource.ListOrderItem.Add(item);
        return DataSource.ListOrderItem.Count();//צריך להחזיר פה את התז
    }
    public OrderItem? GetById(int id)
    {
        return (DataSource.ListOrderItem.Find(item => item.GetValueOrDefault().ID == id));
    }
    public void Update(OrderItem? item)
    {
        OrderItem? temp = DataSource.ListOrderItem.Find(found => found.GetValueOrDefault().ID == item.ID);
        if (temp==null) 
             return;
        DataSource.ListOrderItem.Remove(temp);
        DataSource.ListOrderItem.Add(item);
    }
    public void Delete(int id)
    {
        DataSource.ListOrderItem.RemoveAll(item => item.GetValueOrDefault().ID == id);
    }

    //IEnumerable<T?> GetAll(Func<T?, bool>? filter = null)
 
    public IEnumerable<OrderItem?> GetAll()
    {
        List < OrderItem?> temp = DataSource.ListOrderItem;
        return temp;
    }

    public IEnumerable<OrderItem?> GetAll(int IdOrder)
    {
        List<OrderItem?> temp = DataSource.ListOrderItem.FindAll(item => item.GetValueOrDefault().ID == IdOrder);
        return temp;
    }

    public OrderItem? getItem(int IdOrder, int IdItem)
    {
        return DataSource.ListOrderItem.Find(item => item.GetValueOrDefault().ID == IdOrder);
    }
}
