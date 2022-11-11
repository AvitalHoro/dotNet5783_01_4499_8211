﻿using DalApi;
using DO;
using System.Net.Http.Headers;

namespace Dal;

 internal class DataSource
{
    //size אולי להוסיף למחלקה של המוצר?
    //color
    //חברה מייצרת

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

        internal const int s_startProductNumber = 100000;
        private static int s_nextProductNumber = s_startProductNumber;
        internal static int NextProductNumber { get => ++s_nextProductNumber; }

        internal const int s_startOrderItemNumber = 1000;
        private static int s_nextOrderItemNumber = s_startOrderItemNumber;
        internal static int NextOrderItemNumber { get => ++s_nextOrderItemNumber; }
    }

    private void CreateProduct()
    {
        string[] clothes = { "Frock", "Bottom", "Rompers", "Body Suits" ,"Sweat Shirt" };
        string[] toys = {"Mobile" , "Play Tent - Playgro", "Play Tent - Fisher Price", "Play Tent - Chicco", "Caterpillar Book" };
        string[] carts = {"Royal","RECARO","Baby Jogger", "Chico","Cybex","Twigy","Sport Line", "Phil&Teds","Mountain Buggy","Babyhome","BEBECAR"};
        string[] bottles = {"Avent", "Chicco", "Tommee Tippee", "Lansinoh","Twigy", "MAM","Medela" };
        string[] diapers={"Pampers", "Huggies","Babysitter", "Titulim","Life","Luvs","DYPER","Hello Bello","Coterie" };
        for(int i=0;i<clothes.Length;i++)
        {
            ListProduct.Add(new Product
            { 
                ID=  Config.NextProductNumber,
                Name=clothes[randNum.Next(clothes.Length)],  
                Category= (Category)(0),
                Price= randNum.Next(50,100),
                InStock= randNum.Next(0,30),
                isDeleted=false,
            });
        }
        for (int i = 0; i < toys.Length; i++)
        {
            ListProduct.Add(new Product
            {
                ID = Config.NextProductNumber,
                Name = toys[randNum.Next(clothes.Length)],
                Category = (Category)(1),
                Price = randNum.Next(30, 300),
                InStock = randNum.Next(0, 10),
                isDeleted = false,
            });
        }
        for (int i = 0; i < carts.Length; i++)
        {
            ListProduct.Add(new Product
            {
                ID = Config.NextProductNumber,
                Name = carts[randNum.Next(clothes.Length)],
                Category = (Category)(2),
                Price = randNum.Next(200, 400),
                InStock = randNum.Next(0, 15),
                isDeleted = false,
            });
        }
        for (int i = 0; i < bottles.Length; i++)
        {
            ListProduct.Add(new Product
            {
                ID = Config.NextProductNumber,
                Name = bottles[randNum.Next(clothes.Length)],
                Category = (Category)(3),
                Price = randNum.Next(20, 60),
                InStock = randNum.Next(0, 40),
                isDeleted = false,
            });
        }
        for (int i = 0; i < diapers.Length; i++)
        {
            ListProduct.Add(new Product
            {
                ID = Config.NextProductNumber,
                Name = diapers[randNum.Next(clothes.Length)],
                Category = (Category)(4),
                Price = randNum.Next(50, 80),
                InStock = randNum.Next(0, 30),
                isDeleted = false,
            });
        }
    }

    private void CreateOrder()
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

        ListOrder.Add(new Order
        {
         ID=Config.NextOrderNumber, 
         CostumerName= costumerName[randNum.Next(costumerName.Length)],
         CostumerEmail= costumerEmail[randNum.Next(costumerEmail.Length)],
         CostumerAdress= costumerAdress[randNum.Next(costumerAdress.Length)],
         OrderDate= DateTime.Now- new TimeSpan(randNum.NextInt64(10L*1000L*3600L*24L*100L)), //לחזור לבדוק עם המצגת שקופית 40
         ShipDate= DateTime.Now - new TimeSpan(randNum.NextInt64(10L * 1000L * 3600L * 24L * 7L)), //לשאול את נורית איך זה בדיוק עובד
         DeliveryDate = DateTime.Now - new TimeSpan(randNum.NextInt64(10L * 1000L * 3600L * 24L * 7L)),//,צריך שיהיה אחרי הזתאריך של המשלוח לסדר
         isDeleted= false,
        });
    }

    private void CreateOrderItem()
    {
        Product product = ListProduct[randNum.Next(ListProduct.Count)];
        ListOrderItem.Add(new OrderItem
        {
            ID= Config.NextOrderItemNumber,
            ProductID= product.ID,
            Price= product.Price,
            OrderID= randNum.Next(Config.s_startOrderNumber,Config.s_startOrderNumber+10),
            Amount=randNum.Next(5,30)
        });
    }

    private void s_Initialize()
    {
        CreateProduct();
        CreateOrder(); 
        CreateOrderItem(); 
    }
}