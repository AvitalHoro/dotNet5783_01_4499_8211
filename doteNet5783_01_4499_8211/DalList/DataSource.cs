using DalApi;
using DO;

namespace Dal;
public class DataSource
{
    private DataSource()
    {
        s_Initialize();
    }
    //internal static DataSource s_instance { get; }
    //  static DataSource() => s_Initialize();

    private void s_Initialize()
    //activates all the methods that initializes the lists of the products, orders and order items
    {
        CreateProduct();
        CreateOrder();
        CreateOrderItem();
    }


    static readonly Random randNum = new Random(); //enters a random number to "randNum"
    internal static DataSource s_instance { get; } = new DataSource(); //ctor
    internal  List<Product?> ListProduct { get; } = new List<Product?> { }; 
    internal  List<Order?> ListOrder { get; } = new List<Order?>{ };
    internal  List<OrderItem?> ListOrderItem { get; } = new List<OrderItem?>{ };

    internal static class Config
     //מנהל את כל המספרים הרצים בשביל האיתחול של הרשימות
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

    private void CreateProduct()
    //creates list of new products
    {
        string[] clothes = { "Frock", "Bottom", "Rompers", "Body Suits", "Sweat Shirt" };
        string[] toys = { "Mobile", "Play Tent - Playgro", "Play Tent - Fisher Price", "Play Tent - Chicco", "Caterpillar Book" };
        string[] carts = { "Royal", "RECARO", "Baby Jogger", "Chico", "Cybex", "Twigy", "Sport Line", "Phil&Teds", "Mountain Buggy", "Babyhome", "BEBECAR" };
        string[] bottles = { "Avent", "Chicco", "Tommee Tippee", "Lansinoh", "Twigy", "MAM", "Medela" };
        string[] diapers = { "Pampers", "Huggies", "Babysitter", "Titulim", "Life", "Luvs", "DYPER", "Hello Bello", "Coterie" };
        //מערכים שמהם נגריל מחרוזות של סוגים של מוצרים

        for (int i = 0; i < clothes.Length; i++)
        //initializes the list with new products from the clothes category
        {
            ListProduct.Add(new Product
            {
                ID = Config.NextProductNumber, //runing number
                Name = clothes[randNum.Next(clothes.Length)],  //selects a random name from the array of names of clothes
                Category = (Category)(0),//category=clothes
                Price = randNum.Next(50, 100), //selects a random price in range of 50-100
                InStock = randNum.Next(0, 30),//selects a random amount in stock in range of 0-30
                IsDeleted = false, //the new product isn't deleted
                Path=""
            });
        }
        for (int i = 0; i < toys.Length; i++)
        //initializes the list with new products from the toys category
        {
            ListProduct.Add(new Product
            {
                ID = Config.NextProductNumber,//runing number
                Name = toys[randNum.Next(clothes.Length)],//selects a random name from the array of names of toys
                Category = (Category)(1),//category=toys
                Price = randNum.Next(30, 300),//selects a random price in range of 50-100
                InStock = randNum.Next(0, 10),//selects a random amount in stock in range of 0-30
                IsDeleted = false,//the new product isn't deleted
                Path = ""
            });
        }
        for (int i = 0; i < carts.Length; i++)
        //initializes the list with new products from the carts category
        {
            ListProduct.Add(new Product
            {
                ID = Config.NextProductNumber,//runing number
                Name = carts[randNum.Next(clothes.Length)],//selects a random name from the array of names of carts
                Category = (Category)(2),//category=carts
                Price = randNum.Next(200, 400),//selects a random price in range of 50-100
                InStock = randNum.Next(0, 15),//selects a random amount in stock in range of 0-30
                IsDeleted = false,//the new product isn't deleted
                Path = ""
            });
        }
        for (int i = 0; i < bottles.Length; i++)
        //initializes the list with new products from the bottels category
        {
            ListProduct.Add(new Product
            {
                ID = Config.NextProductNumber,//runing number
                Name = bottles[randNum.Next(clothes.Length)],//selects a random name from the array of names of bottels
                Category = (Category)(3),//category=bottels
                Price = randNum.Next(20, 60),//selects a random price in range of 50-100
                InStock = randNum.Next(0, 40),//selects a random amount in stock in range of 0-30
                IsDeleted = false,//the new product isn't deleted
                Path = ""
            });
        }
        for (int i = 0; i < diapers.Length; i++)
        //initializes the list with new products from the diapers category
        {
            ListProduct.Add(new Product
            {
                ID = Config.NextProductNumber,//runing number
                Name = diapers[randNum.Next(clothes.Length)],//selects a random name from the array of names of diapers
                Category = (Category)(4),//category=diapers
                Price = randNum.Next(50, 80),//selects a random price in range of 50-100
                InStock = randNum.Next(0, 30),//selects a random amount in stock in range of 0-30
                IsDeleted = false,//the new product isn't deleted
                Path = ""
            });
        }
    }

    private void CreateOrder()
    //creates list of new orders
    {
        string[] costumerName = {"Reut cohen", "Avital Shalom", "Emuna Ben-Shimol","Rivka Adler","Sara Davidi", "Rachel Perel","Hadar Muchtar"
                                 ,"Yaakov Vinberg","Israel Levin","Matnya Chadad", "Dor Bar-Sheshet","Shmuel Emanuel","Moshe Ben-Yair","Shira Ben-Pazi"
                                 ,"Osnat Asher","Halel Paz", "Ava Kor","Ayala Lopez", "Shimon Harary","Harry Alexander","Shilo Horovitz"};
        string[] costumerEmail = {"reut16@gmail.com", "aviHoro@net.il.015", "shlomit@walla.co.il" , "Cohen@gmail.com", "benshlomi10@gmail.com",
                                    "workil@gmail.com", "RoiBen@gmail.com","Emuna344@walla.com", "saraAvi@gmail.com","israel@org.il",
                                    "Ava@simpson.org.il", "RahamimYa@gmail.com", "Ruby123@barak.net.il", "Shirfaz@jct.ac.il"};
        string[] costumerAdress = {"Neta", "Jerusalem","Tel-Aviv","Yafo","Dimona","Bet-El", "Bne-Dkalim", "Rehovot",
                                   "Kiryat-Gat","Kiryat-Shmona","Kiryat-Motzkin","Kiryat-Shmuel","Katserin","Haifa",
                                    "Bne-Brak","Ramat-Gan", "Givat-Shmuel"};
        //מערכים שמתוכם נגריל את שם הלקוח, האימייל והכתובת שלו
        //מוסיף 60% מההזמנות עם תאריך הזמנה,שליחה ומסירה 
        for (int i = 0; i < 24; i++)
        {
            ListOrder.Add(new Order
            {
                ID = Config.NextOrderNumber, //מספר רץ
                CostumerName = costumerName[randNum.Next(costumerName.Length)], //מגריל שם של לקוח רנדומלי מהמערך של השמות
                CostumerEmail = costumerEmail[randNum.Next(costumerEmail.Length)], //מגריל אימייל של לקוח רנדומלי מהמערך של האימיילים
                CostumerAdress = costumerAdress[randNum.Next(costumerAdress.Length)],  //מגריל כתובת של לקוח רנדומלי מהמערך של הכתובות
                OrderDate = DateTime.Now - new TimeSpan(randNum.NextInt64(10L * 1000L * 3600L * 24L * 100L)), 
                //לוקח את התאריך של היום, מוריד ממנו זמן מסוים, וכך מתקבל זמן הזמנה הגיוני
                ShipDate = DateTime.Now - new TimeSpan(randNum.NextInt64(10L * 1000L * 3600L * 24L * 20L)),
                //לוקח את התאריך של היום, מוריד ממנו זמן מסוים, וכך מתקבל זמן שליחה הגיוני
                DeliveryDate = DateTime.Now - new TimeSpan(randNum.NextInt64(10L * 1000L * 3600L * 24L * 3L)),
                //לוקח את התאריך של היום, מוריד ממנו זמן מסוים, וכך מתקבל זמן מסירה הגיוני
                IsDeleted = false,
            });
        }
        // מוסיף 20% מההזמנות עם תאריך הזמנה בלבד 
        for (int i = 0; i < 8; i++)
        {
            ListOrder.Add(new Order
            {
                ID = Config.NextOrderNumber,//מספר רץ
                CostumerName = costumerName[randNum.Next(costumerName.Length)],//מגריל שם של לקוח רנדומלי מהמערך של השמות
                CostumerEmail = costumerEmail[randNum.Next(costumerEmail.Length)], //מגריל אימייל של לקוח רנדומלי מהמערך של האימיילים
                CostumerAdress = costumerAdress[randNum.Next(costumerAdress.Length)],//מגריל כתובת של לקוח רנדומלי מהמערך של הכתובות
                OrderDate = DateTime.Now - new TimeSpan(randNum.NextInt64(10L * 1000L * 3600L * 24L * 100L)),
                //לוקח את התאריך של היום, מוריד ממנו זמן מסוים, וכך מתקבל זמן הזמנה הגיוני
                DeliveryDate =null, 
                ShipDate=null,
                IsDeleted = false,
            });
        }

        //מוסיף 20% עם תאריך הזמנה ושליחה
        for (int i = 0; i < 8; i++)
        {
            ListOrder.Add(new Order
            {
                ID = Config.NextOrderNumber,//מספר רץ
                CostumerName = costumerName[randNum.Next(costumerName.Length)],//מגריל שם של לקוח רנדומלי מהמערך של השמות
                CostumerEmail = costumerEmail[randNum.Next(costumerEmail.Length)],//מגריל אימייל של לקוח רנדומלי מהמערך של האימיילים
                CostumerAdress = costumerAdress[randNum.Next(costumerAdress.Length)],//מגריל כתובת של לקוח רנדומלי מהמערך של הכתובות
                OrderDate = DateTime.Now - new TimeSpan(randNum.NextInt64(10L * 1000L * 3600L * 24L * 100L)),
                //לוקח את התאריך של היום, מוריד ממנו זמן מסוים, וכך מתקבל זמן הזמנה הגיוני
                ShipDate = DateTime.Now - new TimeSpan(randNum.NextInt64(10L * 1000L * 3600L * 24L * 20L)),
                //לוקח את התאריך של היום, מוריד ממנו זמן מסוים, וכך מתקבל זמן שליחה הגיוני
                DeliveryDate = null,
                IsDeleted = false,
            });

        }
    }

    private void CreateOrderItem()
     // מאתחל את הרשימה של המוצרים שהוזמנו עם מוצרים חדשים
    {
        for (int i = 0; i < 30; i++)
        {
            Product? product = ListProduct[randNum.Next(ListProduct.Count)]; // מגריל מוצר רנדומלי מהרשימה של המוצרים
            ListOrderItem.Add(new OrderItem
            {
                ID = Config.NextOrderItemNumber, //מספר רץ
                ProductID = product.GetValueOrDefault().ID, //לוקח את הת"ז מהמוצר שהוגרל
                Price = product.GetValueOrDefault().Price, //לוקח את המחיר של המוצר שהוגרל
                OrderID = randNum.Next(Config.s_startOrderNumber, Config.s_startOrderNumber + ListOrder.Count),
                //מגריל ת"ז רנדומלית של הזמנה , הבאנו לו טווח של מינימום ומקסימום
                Amount = randNum.Next(1, 5) // מגריל כמות שהוזמנה עד 5 פריטים
            });
        }
    }

}

