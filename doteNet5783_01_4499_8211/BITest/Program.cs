using BLApi;
using Dal;
using BlImplementation;
using DalApi;
using BlApi;

namespace BlTest;

class Program
{
    //תוכנית ראשית זמנית הבודקת את נכונות כל הקבצים בשכבת הנתונים שעשינו עד כה
    static void testOrder(IBl bOrder)
    {
        int id;
        Console.WriteLine(@"test order:
                Choose one of the following:
                a - TRACKING ORDER
                b - DISPLAY ORDER DETAILS
                c - DISPLAY ORDER LIST
                d - UPDATE SHIP DATE ORDER
                e - UPDATE DELIVERY DATE ORDER
                f - UPDATE ORDER");
        string option = Console.ReadLine();
        switch (option)
        {
            case "a":
                Console.WriteLine("enter the order ID");
                int.TryParse(Console.ReadLine(), out id);
                Console.WriteLine(bOrder.Order.Tracking(id));
                break;
            case "b":
                Console.WriteLine("enter the order ID");
                int.TryParse(Console.ReadLine(), out id);
                Console.WriteLine(bOrder.Order.getDetailsOrder(id));
                break;
            case "c":
                foreach (var item in bOrder.Order.getOrderList())
                {
                    Console.WriteLine(item);
                }
                /// מדפיסים את הכל
                break;
            case "d":
                Console.WriteLine("enter the order ID:");
                int.TryParse(Console.ReadLine(), out id);
                Console.WriteLine(bOrder.Order.UpdateShipDate(id));
                break;
            case "e":
                Console.WriteLine("enter the order ID:");
                int.TryParse(Console.ReadLine(), out id);
                Console.WriteLine(bOrder.Order.UpdateDeliveryDate(id));
                break;
            case "f":
                Console.WriteLine("enter the order ID:");
                int.TryParse(Console.ReadLine(), out id);
                int orderID = id;
                Console.WriteLine("enter the product ID:");
                int.TryParse(Console.ReadLine(), out id);
                int productID = id;
                Console.WriteLine("enter the new amount:");
                int amount;
                int.TryParse(Console.ReadLine(), out amount);
                Console.WriteLine(bOrder.Order.UpdateOrder(orderID, productID, amount));
                break;
        }
    }

    static void testCart(IBl bCart)
    {
        Console.WriteLine(@"test order item:
                Choose one of the following:
                a - ADD PRODUCT TO THE CART
                b - UPDATE AMOUNT OF PRODUCT
                c - APPROVE ORDER
                ");
        string option = Console.ReadLine();
        switch (option)
        {
            case "a":
               
                break;
            case "b":
             
                break;
            case "c":
               
                break;
        }
    }

    static void testProduct(IBl bProduct)
    {
        Console.WriteLine(@"test product:
                Choose one of the following:
                a - Get product list to the manager
                b - Get product details
                c - Add product
                d - Remove product
                e - Update product details
                f - Get catalog");
        string option = Console.ReadLine();
        switch (option)
        {
            case "c":
                
                break;
            case "b":
               
                break;
            case "a":
               
                break;
            case "d":
                
                break;
            case "e":
            
                break;
        }
    }

    static void Main(string[] args)
    {
        IBl bl = BlFactory.GetBl();
        IDal dal = DalFactory.GetDal();
        int num = 1;
        while (num != 0)
        {
            Console.WriteLine(@"welcome to our store!
                Choose one of the following:
                0-exit
                1-test Order
                2-test Cart
                3-test Product");
            string option = Console.ReadLine();
            bool b = int.TryParse(option, out num);
            try
            {
                if (b)
                {
                    switch (num)
                    {
                        case 1:
                            testOrder(bl);
                            break;
                        case 2:
                            testCart(bl);
                            break;
                        case 3:
                            testProduct(bl);
                            break;
                        default:
                            break;
                    }
                }
                else
                    Console.WriteLine("ERROR");
            }
            catch (BO.DontExistException ex)
            {
                Console.WriteLine(ex);
            }
            catch (BO.AlreadyExistsException ex)
            {
                Console.WriteLine(ex);
            }
            //, או לעשות קצ' כללי או להוסיף עוד קצ'ים
        }
    }
}

