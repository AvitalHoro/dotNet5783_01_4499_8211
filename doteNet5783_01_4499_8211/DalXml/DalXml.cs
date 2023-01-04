namespace Dal;
using DalApi;
using DO;

sealed class DalXml : IDal
{
    public IProduct Product { get; } = new Dal.Product();
    public IOrder Order { get; } = new Dal.Order();
    public IOrderItem OrderItem { get; } = new Dal.OrderItem();
} 