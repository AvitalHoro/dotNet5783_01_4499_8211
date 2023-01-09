using DO;

namespace DalApi;

public interface IOrderItem : ICrud<OrderItem>
{
    OrderItem GetItem(int IdOrder, int IdProduct);
}