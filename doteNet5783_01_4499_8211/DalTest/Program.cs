﻿
using DalApi;
using DO;
using System.Data.Common;
using System.Diagnostics;

namespace Dal;

//תוכנית ראשית זמנית הבודקת את נכונות כל הקבצים בשכבת הנתונים שעשינו עד כה

class Program
{
    static void testOrder(DalOrder order)
    {
        Console.WriteLine(@"test order:
                Choose one of the following:
                a - ADD ORDER
                b - DISPLAY ORDER
                c - DISPLAY ORDER LIST
                d - UPDATE ORDER
                e - DELETE ORDER");
        switch (Console.ReadLine())
        {
            case "a":
                Order tmpOrder = new Order();
                //Console.WriteLine("enter the new order ID");
                int id;
                //int.TryParse(Console.ReadLine(),out id);
                //tmpOrder.ID = id;
                Console.WriteLine("enter the costumer name");
                tmpOrder.CostumerName = Console.ReadLine()!;
                Console.WriteLine("enter the costumer email");
                tmpOrder.CostumerEmail = Console.ReadLine()!;
                Console.WriteLine("enter the costumer adress");
                tmpOrder.CostumerAdress = Console.ReadLine()!;
                tmpOrder.OrderDate=DateTime.Now;
                order.Add(tmpOrder);
                break;
            case "b":
                Console.WriteLine("enter the order ID");
                int.TryParse(Console.ReadLine(), out id);
                int myId = id;
                Console.WriteLine(order.GetById(myId));
                break;
            case "c":
                foreach (Order? item in order.GetAll())
                {
                    Console.WriteLine(item);
                }
                /// מדפיסים את הכל
                break;
            case "d":
                Order tmpOrder2 = new Order();
                Console.WriteLine("enter the new order ID");
                int.TryParse(Console.ReadLine(), out id);
                tmpOrder2.ID = id;
                Console.WriteLine("enter the costumer name");
                tmpOrder2.CostumerName = Console.ReadLine()!;
                Console.WriteLine("enter the costumer email");
                tmpOrder2.CostumerEmail = Console.ReadLine()!;
                Console.WriteLine("enter the costumer adress");
                tmpOrder2.CostumerAdress = Console.ReadLine()!;
                order.Update(tmpOrder2);
                break;
            case "e":
                Console.WriteLine("enter the product ID");
                int.TryParse(Console.ReadLine(), out id);
                myId = id;
                order.Delete(myId);
                break;
        }
    }

    static void testOrderItem(DalOrderItem item)
    {
        Console.WriteLine(@"test order item:
                Choose one of the following:
                a - ADD ORDER ITEM
                b - DISPLAY ORDER ITEM
                c - DISPLAY ORDER ITEM LIST
                d - UPDATE ORDER ITEM
                e - DELETE ORDER ITEM");
        switch (Console.ReadLine())
        {
            case "a":
                OrderItem tmpItem = new OrderItem();
                Console.WriteLine("enter the new item ID");
                int id;
                int.TryParse(Console.ReadLine(), out id);
                tmpItem.ID = id;

                Console.WriteLine("enter the new product ID");
                int.TryParse(Console.ReadLine(), out id);
                tmpItem.ProductID = id;
                Console.WriteLine("enter the new Order ID");
                int.TryParse(Console.ReadLine(), out id);
                tmpItem.OrderID = id;
                Console.WriteLine("enter the new order item price");
                int.TryParse(Console.ReadLine(), out id);
                tmpItem.Price = id;
                Console.WriteLine("enter the new order item amount");
                int.TryParse(Console.ReadLine(), out id);
                tmpItem.Amount = id;
                item.Add(tmpItem);
                break;
            case "b":
                Console.WriteLine("enter the order item ID");
                int myId;
                int.TryParse(Console.ReadLine(), out myId);
                Console.WriteLine(item.GetById(myId));
                break;
            case "c":
                foreach (OrderItem oItem in item.GetAll())
                {
                    Console.WriteLine(oItem);
                }
                /// מדפיסים את הכל
                break;
            case "d":
                OrderItem tmpItem2 = new OrderItem();
                Console.WriteLine("enter the new item ID");
                int.TryParse(Console.ReadLine(), out id);
                tmpItem2.ID = id;
                Console.WriteLine("enter the new product ID");
                int.TryParse(Console.ReadLine(), out id);
                tmpItem2.ProductID = id;
                Console.WriteLine("enter the new Order ID");
                int.TryParse(Console.ReadLine(), out id);
                tmpItem2.OrderID = id;
                Console.WriteLine("enter the new order item price");
                int.TryParse(Console.ReadLine(), out id);
                tmpItem2.Price = id;
                Console.WriteLine("enter the new order item amount");
                int.TryParse(Console.ReadLine(), out id);
                tmpItem2.Amount = id;
                item.Update(tmpItem2);
                break;
            case "e":
                Console.WriteLine("enter the product ID");
                int.TryParse(Console.ReadLine(), out myId);
                item.Delete(myId);
                break;
        }
    }

    static bool isSixNumbers(int id)
    {
        if (id>100000&&id<1000000)
            return true;
        return false;
    }


    static void testProduct(DalProduct product)
    {
        Console.WriteLine(@"test product:
                Choose one of the following:
                a - ADD PRODUCT
                b - DISPLAY PRODUCT
                c - DISPLAY PRODUCT LIST
                d - UPDATE PRODUCT
                e - DELETE PRODUCT");
        switch (Console.ReadLine())
        {
            case "a":
                Product tmpProduct = new Product();
                Console.WriteLine("enter the new product ID");
                int id;
                int.TryParse(Console.ReadLine(), out id);
                while (!isSixNumbers(id))
                {
                    Console.WriteLine("Invalid ID. enter secondly the new product ID");
                    int.TryParse(Console.ReadLine(), out id);
                }
                tmpProduct.ID = id;
                Console.WriteLine("enter the new product name");
                tmpProduct.Name = Console.ReadLine()??"";
                Console.WriteLine(@"enter the new product catgory: 
                                        Clothes-0, 
                                        Toys-1, 
                                        Carts-2, 
                                        Bottles-3, 
                                        Diapers-4");
                int.TryParse(Console.ReadLine(), out id);
                int ctg = id;
                switch (ctg)
                {
                    case 0:
                        tmpProduct.Category = Category.Clothes;
                        break;
                    case 1:
                        tmpProduct.Category = Category.Toys;
                        break;
                    case 2:
                        tmpProduct.Category = Category.Carts;
                        break;
                    case 3:
                        tmpProduct.Category = Category.Bottles;
                        break;
                    case 4:
                        tmpProduct.Category = Category.Diapers;
                        break;
                    default:
                        Console.WriteLine("ERROR");
                        break;
                }
                Console.WriteLine("enter the new product price");
                int.TryParse(Console.ReadLine(), out id);
                tmpProduct.Price = id;
                Console.WriteLine("enter the new product amount");
                int.TryParse(Console.ReadLine(), out id);
                tmpProduct.InStock = id;
                product.Add(tmpProduct);
                break;
            case "b":
                Console.WriteLine("enter the product ID");
                int myId;
                int.TryParse(Console.ReadLine(), out myId);
                Console.WriteLine(product.GetById(myId));
                break;
            case "c":
                foreach (Product item in product.GetAll())
                {
                    Console.WriteLine(item);
                }

                    ;/// מדפיסים את הכל
                break;
            case "d":
                Product tmpProduct2 = new Product();
                Console.WriteLine("enter the new product ID");
                int.TryParse(Console.ReadLine(), out id);
                tmpProduct2.ID = id;
                Console.WriteLine("enter the new product name");
                tmpProduct2.Name = Console.ReadLine()!;
                Console.WriteLine(@"enter the new product catgory: 
                                        Clothes-0, 
                                        Toys-1, 
                                        Carts-2, 
                                        Bottles-3, 
                                        Diapers-4");
                int.TryParse(Console.ReadLine(), out ctg);
                switch (ctg)
                {
                    case 0:
                        tmpProduct2.Category = Category.Clothes;
                        break;
                    case 1:
                        tmpProduct2.Category = Category.Toys;
                        break;
                    case 2:
                        tmpProduct2.Category = Category.Carts;
                        break;
                    case 3:
                        tmpProduct2.Category = Category.Bottles;
                        break;
                    case 4:
                        tmpProduct2.Category = Category.Diapers;
                        break;
                    default:
                        Console.WriteLine("ERROR");
                        break;
                }
                Console.WriteLine("enter the new product price");
                int.TryParse(Console.ReadLine(), out id);
                tmpProduct2.Price = id;
                Console.WriteLine("enter the new product amount");
                int.TryParse(Console.ReadLine(), out id);
                tmpProduct2.InStock = id;
                product.Update(tmpProduct2);
                break;
            case "e":
                Console.WriteLine("enter the product ID");
                int.TryParse(Console.ReadLine(), out myId);
                product.Delete(myId);
                break;
        }
    }

    static void Main(string[] args)
    {
        IDal dal = DalFactory.GetDal() ?? throw new NullReferenceException("Missing Dal"); ;
        DalProduct product = new DalProduct();
        DalOrder order = new DalOrder();
        DalOrderItem item = new DalOrderItem();
        int num = 1;
        while (num != 0)
        {
            Console.WriteLine(@"welcome to our store!
                Choose one of the following:
                0-exit
                1-test Order
                2-test OrderItem
                3-test Product");
            bool b = int.TryParse(Console.ReadLine(), out num);
            if (!b)
            {
                Console.WriteLine("ERROR");
                break;
            }
            try
            {
                switch (num)
                {
                    case 1:
                        testOrder(order);
                        break;
                    case 2:
                        testOrderItem(item);
                        break;
                    case 3:
                        testProduct(product);
                        break;
                    default:
                        break;
                }
            }

            catch(DoesNotExistException ex)
            {
                Console.WriteLine(ex);
            }
            catch (AlreadyExistsException ex)
            {
                Console.WriteLine(ex);
            }

        }
    }
}
