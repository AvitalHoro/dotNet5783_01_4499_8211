using DO;

namespace DalApi;

public interface IOrderItem : ICrud<OrderItem>
{
    int Add(OrderItem? item);
    OrderItem? GetById(int id);
    void Update(OrderItem? item);
    void Delete(int id);

    //IEnumerable<T?> GetAll(Func<T?, bool>? filter = null);
    IEnumerable<OrderItem?> GetAll();

    IEnumerable<OrderItem?> GetAll(int IdOrder);

    OrderItem? getItem(int IdOrder, int IdItem);
}