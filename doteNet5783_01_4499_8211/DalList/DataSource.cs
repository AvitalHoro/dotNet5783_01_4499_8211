using DalApi;
using DO;

namespace Dal;

 internal class DataSource
{
   private DataSource()
    {
        s_Initialize();
    }

    static readonly Random randNum= new Random();
    internal static DataSource s_instance { get; } = new DataSource();
    internal static List<Product> ListProduct { get; } = new List<Product>() { };
    internal static List<Order> ListOrder { get; } = new List<Order>() { };
    internal static List<OrderItem> ListOrderItem { get; } = new List<OrderItem>() { };


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
        string[] clothes = { "shirt", "pants", "overall", ""  };
        string[] toys = {"University" };
        string[] carts = {"Royal"};
        string[] bottles = {"Avent" };
        string[] diapers={"Pampers" };
        for(int i=0;i<clothes.Length;i++)
        {
            ListProduct.Add(new Product
            { 
                ID=  Config.NextOrderItemNumber,
                Name=clothes[randNum.Next(clothes.Length)],  
                Category= (Category)(0),
                Price= randNum.Next(50,100),
                InStock= randNum.Next(0,30),
                isDeleted=false,
            });
        }
        for (int i = 0; i < clothes.Length; i++)
        {
            ListProduct.Add(new Product
            {
                ID = Config.NextOrderItemNumber,
                Name = clothes[randNum.Next(clothes.Length)],
                Category = (Category)(0),
                Price = randNum.Next(50, 100),
                InStock = randNum.Next(0, 30),
                isDeleted = false,
            });
        }
        for (int i = 0; i < clothes.Length; i++)
        {
            ListProduct.Add(new Product
            {
                ID = Config.NextOrderItemNumber,
                Name = clothes[randNum.Next(clothes.Length)],
                Category = (Category)(0),
                Price = randNum.Next(50, 100),
                InStock = randNum.Next(0, 30),
                isDeleted = false,
            });
        }
        for (int i = 0; i < clothes.Length; i++)
        {
            ListProduct.Add(new Product
            {
                ID = Config.NextOrderItemNumber,
                Name = clothes[randNum.Next(clothes.Length)],
                Category = (Category)(0),
                Price = randNum.Next(50, 100),
                InStock = randNum.Next(0, 30),
                isDeleted = false,
            });
        }
        for (int i = 0; i < clothes.Length; i++)
        {
            ListProduct.Add(new Product
            {
                ID = Config.NextOrderItemNumber,
                Name = clothes[randNum.Next(clothes.Length)],
                Category = (Category)(0),
                Price = randNum.Next(50, 100),
                InStock = randNum.Next(0, 30),
                isDeleted = false,
            });
        }
    }

    private void CreateOrder()
    {
        ListOrder.Add(new Order
        {

        });
    }

    private void CreateOrderItem()
    {
        ListOrderItem.Add(new OrderItem
        {

        });
    }

    private void s_Initialize()
    {
        CreateProduct();
        CreateOrder(); 
        CreateOrderItem(); 
    }
}
