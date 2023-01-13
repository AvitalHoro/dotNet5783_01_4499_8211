namespace Dal;
using DalApi;
using DO;
using System.Diagnostics;

public struct RuningNumber
{
    public double numberSaved { get; set; }
    public string typeOfnumber { get; set; }
}

sealed class DalXml : IDal
{
    public static IDal Instance { get; } = new DalXml();
    private DalXml() { }
    public IProduct Product { get; } = new Dal.Product();
    public IOrder Order { get; } = new Dal.Order();
    public IOrderItem OrderItem { get; } = new Dal.OrderItem();
}