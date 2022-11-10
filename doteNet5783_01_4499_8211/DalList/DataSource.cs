using DO;

namespace Dal;

 internal class DataSource
{
   private DataSource()
    {
        s_Initialize();
    }

    static readonly Random CreateRandNum= new Random();
    internal static DataSource s_instance { get; } = new DataSource();
    internal List<Product?> ListProduct { get; } = new List<Product?>() { };
    internal List<Order?> ListOrder { get; } = new List<Order?>() { };
    internal List<OrderItem?> ListOrderItem { get; } = new List<OrderItem?>() { };


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

    private void CreateProduct()
    {
        Product product = new Product();
        product.Name = 
        ListProduct.Add(product); 
    }

    private void CreateOrder()
    {
        Order order = new Order();
        ListOrder.Add(order);
    }

    private void CreateOrderItem()
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
