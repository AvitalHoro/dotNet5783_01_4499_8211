using DO;

namespace DalApi;

public interface IOrderItem : ICrud<OrderItem>
{
    OrderItem getItem(int IdOrder, int IdProduct);
    public IEnumerable<OrderItem?> GetAll(int IdOrder);
    public IEnumerable<OrderItem?> GetAllProduct(int IdProduct);
}