
using DalApi;
using DO;

namespace Dal;

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
        string option = Console.ReadLine();
        switch (option)
        {
            case "a":
                Order tmpOrder = new Order();
                Console.WriteLine("enter the new order ID");
                tmpOrder.ID = Console.Read();
                Console.WriteLine("enter the costumer name");
                tmpOrder.CostumerName = Console.ReadLine();
                Console.WriteLine("enter the costumer email");
                tmpOrder.CostumerEmail = Console.ReadLine();
                Console.WriteLine("enter the costumer adress");
                tmpOrder.CostumerAdress = Console.ReadLine();
                order.Add(tmpOrder);
                break;
            case "b":
                Console.WriteLine("enter the order ID");
                int myId = Console.Read();
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
                tmpOrder2.ID = Console.Read();
                Console.WriteLine("enter the costumer name");
                tmpOrder2.CostumerName = Console.ReadLine();
                Console.WriteLine("enter the costumer email");
                tmpOrder2.CostumerEmail = Console.ReadLine();
                Console.WriteLine("enter the costumer adress");
                tmpOrder2.CostumerAdress = Console.ReadLine();
                order.Update(tmpOrder2);
                break;
            case "e":
                Console.WriteLine("enter the product ID");
                myId = Console.Read();
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
        string option = Console.ReadLine();
        switch (option)
        {
            case "a":
                OrderItem tmpItem = new OrderItem();
                Console.WriteLine("enter the new item ID");
                tmpItem.ID = Console.Read();
                Console.WriteLine("enter the new product ID");
                tmpItem.ProductID = Console.Read();
                Console.WriteLine("enter the new Order ID");
                tmpItem.OrderID = Console.Read();
                Console.WriteLine("enter the new order item price");
                tmpItem.Price = Console.Read();
                Console.WriteLine("enter the new order item amount");
                tmpItem.Amount = Console.Read();
                item.Add(tmpItem);
                break;
            case "b":
                Console.WriteLine("enter the order item ID");
                int myId = Console.Read();
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
                OrderItem tmpItem2 = new OrderItem();
                Console.WriteLine("enter the new item ID");
                tmpItem2.ID = Console.Read();
                Console.WriteLine("enter the new product ID");
                tmpItem2.ProductID = Console.Read();
                Console.WriteLine("enter the new Order ID");
                tmpItem2.OrderID = Console.Read();
                Console.WriteLine("enter the new order item price");
                tmpItem2.Price = Console.Read();
                Console.WriteLine("enter the new order item amount");
                tmpItem2.Amount = Console.Read();
                item.Update(tmpItem2);
                break;
            case "e":
                Console.WriteLine("enter the product ID");
                myId = Console.Read();
                item.Delete(myId);
                break;
        }
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
        string option = Console.ReadLine();
        switch (option)
        {
            case "a":
                Product tmpProduct = new Product();
                Console.WriteLine("enter the new product ID");
                tmpProduct.ID = Console.Read();
                Console.WriteLine("enter the new product name");
                tmpProduct.Name = Console.ReadLine();
                Console.WriteLine(@"enter the new product catgory: 
                                        Clothes-0, 
                                        Toys-1, 
                                        Carts-2, 
                                        Bottles-3, 
                                        Diapers-4");
                int ctg = Console.Read();
                switch(ctg)
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
                tmpProduct.Price = Console.Read();
                Console.WriteLine("enter the new product amount");
                tmpProduct.InStock = Console.Read();
                product.Add(tmpProduct);
                break;
            case "b":
                Console.WriteLine("enter the product ID");
                int myId = Console.Read();
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
                tmpProduct2.ID = Console.Read();
                Console.WriteLine("enter the new product name");
                tmpProduct2.Name = Console.ReadLine();
                Console.WriteLine(@"enter the new product catgory: 
                                        Clothes-0, 
                                        Toys-1, 
                                        Carts-2, 
                                        Bottles-3, 
                                        Diapers-4");
                ctg = Console.Read();
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
                tmpProduct2.Price = Console.Read();
                Console.WriteLine("enter the new product amount");
                tmpProduct2.InStock = Console.Read();
                product.Update(tmpProduct2);
                break;
            case "e":
                Console.WriteLine("enter the product ID");
                myId = Console.Read();
                product.Delete(myId);
                break;
        }
    }

    static void Main(string[] args)
    {
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
    }


    //static void Main(string[] args, int x, double f)
    //{

    //    Days d;//= Days.Fri;

    //    d = (Days)Console.Read();

    //    switch (d)
    //    {
    //        case Days.Sat:
    //            break;
    //        case Days.Sun:
    //            break;
    //        case Days.Mon:
    //            break;
    //        case Days.Tue:
    //            break;
    //        case Days.Wed:
    //            break;
    //        case Days.Thu:
    //            break;
    //        case Days.Fri:
    //            break;
    //        default:
    //            break;
    //    }
    //    Console.ReadLine();
    //}


}
