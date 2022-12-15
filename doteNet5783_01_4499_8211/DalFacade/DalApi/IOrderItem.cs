using DO;

namespace DalApi;

public interface IOrderItem : ICrud<OrderItem>
{
    OrderItem getItem(int IdOrder, int IdProduct);
}