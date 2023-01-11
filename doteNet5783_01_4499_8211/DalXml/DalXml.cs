namespace Dal;
using DalApi;
using DO;
using System.Diagnostics;

sealed class DalXml : IDal
{
    public static IDal Instance { get; } = new DalXml();
    private DalXml() { }
    public IProduct Product { get; } = new Dal.Product();
    public IOrder Order { get; } = new Dal.Order();
    public IOrderItem OrderItem { get; } = new Dal.OrderItem();
} 