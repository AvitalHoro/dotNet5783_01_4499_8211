using System;
using System.Collections.Specialized;
using DalApi;
using DO;

namespace Dal;

class Program
{
    static void testOrder()
    {
        Console.WriteLine(@"test order:
                Choose one of the following:
                a - ADD ORDER
                b - DISPLAY ORDER
                c - DISPLAY ORDER LIST
                d - UPDATE ORDER
                e - DELETE ORDER");
    }

    static void testOrderItem()
    {
        Console.WriteLine(@"test order item:
                Choose one of the following:
                a - ADD ORDER ITEM
                b - DISPLAY ORDER ITEM
                c - DISPLAY ORDER ITEM LIST
                d - UPDATE ORDER ITEM
                e - DELETE ORDER ITEM");
    }

    static void testProduct()
    {
        Console.WriteLine(@"test product:
                Choose one of the following:
                a - ADD PRODUCT
                b - DISPLAY PRODUCT
                c - DISPLAY ORDER LIST
                d - UPDATE ORDER
                e - DELETE ORDER");
        string option = Console.ReadLine();
        switch(option)
        {
            case "a":

                break;

        }
    }

    static void Main(string[] args)
    {
        //private IOrder order = new Order;
        //private IProduct product=new Product;
        //private IOrderItem order_item=new OrderItem;
        int num = 1;
        while (num != 0)
        {
            Console.WriteLine(@"welcome to our store!
                Choose one of the following:
                0-exit
                1-test Order
                2-test OrderItem
                3-test Product");
            string option = Console.ReadLine();
            bool b = int.TryParse(option, out num);
            if (!b)
            {
                Console.WriteLine("ERROR");
                break;
            }
            switch (num)
            {
                case 1:
                    testOrder();
                    break;
                case 2:
                    testOrderItem();
                    break;
                case 3:
                    testProduct();
                    break;
                default:
                    break;
            }

        }
    }




}
