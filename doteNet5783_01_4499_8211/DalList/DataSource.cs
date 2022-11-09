using DalFacade.DalApi;
using DalFacade.DO;

namespace Dal;

 public class DataSource
{
    DataSource()
    {
        s_Initialize();
    }

    static readonly Random CreateRandNum= new Random();
    internal static DataSource s_instance { get; } = new DataSource();
    internal List<Product> ListProduct { get; } = new List<Product>() { };
    internal List<Order> ListOrder { get; } = new List<Order>() { };
    internal List<OrderItem> ListOrderItem { get; } = new List<OrderItem>() { };


    internal static class Config
    {
        internal const int s_startOrderNumber = 1000;
        private static int s_nextOrderNumber = s_startOrderNumber;
        internal static int NextOrderNumber { get => ++s_nextOrderNumber; }

        internal const int s_startProductNumber = 1000;
        private static int s_nextProductNumber = s_startProductNumber;
        internal static int NextProductNumber { get => ++s_nextProductNumber; }

        internal const int s_startOrderItemNumber = 1000;
        private static int s_nextOrderItemNumber = s_startOrderItemNumber;
        internal static int NextOrderItemNumber { get => ++s_nextOrderItemNumber; }
    }

    private void AddProduct()
    {
        Product product = new Product();
        ListProduct.Add(product); 
    }

    private void AddOrder()
    {
        Order order = new Order();
        ListOrder.Add(order);
    }

    private void AddOrderItem()
    {
        OrderItem item = new OrderItem();
        ListOrderItem.Add(item);
    }

    private void s_Initialize()
    {
        AddProduct();
        AddOrder(); 
        AddOrderItem(); 
    }


}
