using DalApi;
using DO;
using static System.Net.Mime.MediaTypeNames;

namespace Dal
{
    public class DataSource
    {
        private DataSource()
        {
            s_Initialize();
        }

        private void s_Initialize()
        // Activates all methods that initialize the lists of products, orders, and order items.
        {
            CreateProduct();
            CreateOrder();
            CreateOrderItem();
        }

        static readonly Random randNum = new Random(); // Initializes a random number generator.
        internal static DataSource s_instance { get; } = new DataSource(); // Constructor.
        internal List<Product?> ListProduct { get; } = new List<Product?> { }; 
        internal List<Order?> ListOrder { get; } = new List<Order?> { };
        internal List<OrderItem?> ListOrderItem { get; } = new List<OrderItem?> { };

        #region Config
        internal static class Config
        // Manages all the numbers used for initializing the lists.
        {
            internal const int s_startOrderNumber = 1000;
            private static int s_nextOrderNumber = s_startOrderNumber;
            internal static int NextOrderNumber { get => ++s_nextOrderNumber; }

            internal const int s_startProductNumber = 100000;
            private static int s_nextProductNumber = s_startProductNumber;
            internal static int NextProductNumber { get => ++s_nextProductNumber; }

            internal const int s_startOrderItemNumber = 1000;
            private static int s_nextOrderItemNumber = s_startOrderItemNumber;
            internal static int NextOrderItemNumber { get => ++s_nextOrderItemNumber; }
        }
        #endregion

        #region CreateProduct
        private void CreateProduct()
        // Creates a list of new products.
        {
            string[] clothes = { "Frock", "Bottom", "Rompers", "Body Suits", "Sweat Shirt" };
            string[] toys = { "Mobile", "Play Tent - Playgro", "Play Tent - Fisher Price", "Play Tent - Chicco", "Caterpillar Book" };
            string[] carts = { "Royal", "RECARO", "Baby Jogger", "Chico", "Cybex", "Twigy", "Sport Line", "Phil&Teds", "Mountain Buggy", "Babyhome", "BEBECAR" };
            string[] bottles = { "Avent", "Chicco", "Tommee Tippee", "Lansinoh", "Twigy", "MAM", "Medela" };
            string[] diapers = { "Pampers", "Huggies", "Babysitter", "Titulim", "Life", "Luvs", "DYPER", "Hello Bello", "Coterie" };
            // Arrays from which strings of product types will be randomly selected.

            for (int i = 0; i < clothes.Length; i++)
            // Initializes the list with new products from the clothes category.
            {
                ListProduct.Add(new Product
                {
                    ID = Config.NextProductNumber, // Running number.
                    Name = clothes[randNum.Next(clothes.Length)],  // Selects a random name from the array of clothes names.
                    Category = (Category)(0), // Category = clothes.
                    Price = randNum.Next(50, 100), // Selects a random price in the range of 50-100.
                    InStock = randNum.Next(0, 30), // Selects a random amount in stock in the range of 0-30.
                    IsDeleted = false, // The new product isn't deleted.
                    Path = "/Images/5.png"
                });
            }
            // Repeat similar blocks for other product categories (toys, carts, bottles, diapers).
        }
        #endregion

        #region CreateOrder
        private void CreateOrder()
        // Creates a list of new orders.
        {
            DateTime date = DateTime.Now - new TimeSpan(randNum.Next(10 * 1000 * 3600 * 24 * 10000));
            string[] customerName = { "Reut Cohen", "Avital Shalom", "Emuna Ben-Shimol", /* other names */ };
            string[] customerEmail = { "reut16@gmail.com", "aviHoro@net.il.015", "shlomit@walla.co.il", /* other emails */ };
            string[] customerAddress = { "Neta", "Jerusalem", "Tel-Aviv", /* other addresses */ };
            // Arrays from which customer names, emails, and addresses will be randomly selected.

            // Adds 60% of orders with order, shipping, and delivery dates.
            for (int i = 0; i < 24; i++)
            {
                ListOrder.Add(new Order
                {
                    ID = Config.NextOrderNumber, // Running number.
                    TotalPrice = 0,
                    CustomerName = customerName[randNum.Next(customerName.Length)], // Randomly selects a customer name from the array of names.
                    CustomerEmail = customerEmail[randNum.Next(customerEmail.Length)], // Randomly selects a customer email from the array of emails.
                    CustomerAddress = customerAddress[randNum.Next(customerAddress.Length)], // Randomly selects a customer address from the array of addresses.
                    OrderDate = date + new TimeSpan(randNum.NextInt64(10L * 1000L * 3600L * 24L * 500L)), 
                    ShipDate = date + new TimeSpan(randNum.NextInt64(10L * 1000L * 3600L * 24L * 1000L , 10L * 1000L * 3600L * 24L * 4000L)),
                    DeliveryDate = date + new TimeSpan(randNum.NextInt64(10L * 1000L * 3600L * 24L * 4000L, 10L * 1000L * 3600L * 24L * 9500L)),
                    IsDeleted = false,
                });
            }
            // Repeat for other scenarios of orders without shipping/delivery dates and with only order date.
        }
        #endregion

        #region CreateOrderItem
        private void CreateOrderItem()
        // Initializes the list of ordered items with new products.
        {
            for (int i = 0; i < ListOrder.Count; i++)
            {
                Order order = ListOrder[i] ?? new Order();
                int max = randNum.Next(1, 4);
                for (int j = 0; j < max; j++)
                {
                    Product product = ListProduct[randNum.Next(0, ListProduct.Count)] ?? new Product(); // Randomly selects a product from the product list.
                    ListOrderItem.Add(new OrderItem
                    {
                        ID = Config.NextOrderItemNumber, // Running number.
                        ProductID = product.ID, // Takes the ID from the randomly selected product.
                        Price = product.Price, // Takes the price from the randomly selected product.
                        OrderID = order.ID, // Takes the ID from the current order.
                        Path = product.Path,
                        Amount = randNum.Next(1, 5) // Randomly generates an amount ordered (1 to 5 items).
                    });
                }
            }
        }
        #endregion
    }
}
