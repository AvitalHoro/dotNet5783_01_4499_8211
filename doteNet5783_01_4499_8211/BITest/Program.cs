using BLApi;
using Dal;
using
using BlImplementation;
using DalApi;
using BlApi;

namespace BlTest;

class Program
{
    //תוכנית ראשית זמנית הבודקת את נכונות כל הקבצים בשכבת הנתונים שעשינו עד כה
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
                int id;
                int.TryParse(Console.ReadLine(), out id);
                tmpOrder.ID = id;
                Console.WriteLine("enter the costumer name");
                tmpOrder.CostumerName = Console.ReadLine();
                Console.WriteLine("enter the costumer email");
                tmpOrder.CostumerEmail = Console.ReadLine();
                Console.WriteLine("enter the costumer adress");
                tmpOrder.CostumerAdress = Console.ReadLine();
                tmpOrder.OrderDate = DateTime.Now;
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
                tmpOrder2.CostumerName = Console.ReadLine();
                Console.WriteLine("enter the costumer email");
                tmpOrder2.CostumerEmail = Console.ReadLine();
                Console.WriteLine("enter the costumer adress");
                tmpOrder2.CostumerAdress = Console.ReadLine();
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
        string option = Console.ReadLine();
        switch (option)
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

    static void testProduct(IBl product)
    {
        Console.WriteLine(@"test product:
                Choose one of the following:
                a - Get product list to the manager
                b - Get product details
                c - Add product
                d - Remove product
                e - Update product details");
        string option = Console.ReadLine();
        switch (option)
        {
            case "c":
                BO.Product tmpProduct = new BO.Product();
                Console.WriteLine("enter the new product ID");
                int id;
                int.TryParse(Console.ReadLine(), out id);
                tmpProduct.ID = id;
                Console.WriteLine("enter the new product name");
                tmpProduct.Name = Console.ReadLine();
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
                        tmpProduct.Category = BO.Category.Clothes;
                        break;
                    case 1:
                        tmpProduct.Category = BO.Category.Toys;
                        break;
                    case 2:
                        tmpProduct.Category = BO.Category.Carts;
                        break;
                    case 3:
                        tmpProduct.Category = BO.Category.Bottles;
                        break;
                    case 4:
                        tmpProduct.Category = BO.Category.Diapers;
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
                product.Product.AddProduct(tmpProduct);
                break;
            case "b":
                Console.WriteLine("enter the product ID");
                int myId;
                int.TryParse(Console.ReadLine(), out myId);
                Console.WriteLine(product.Product.GetProductDetails(myId));
                break;
            case "a":
                foreach (BO.ProductForList item in product.Product.GetProductList())
                {
                    Console.WriteLine(item);
                }

                    ;/// מדפיסים את הכל
                break;
            case "d":
                Console.WriteLine("enter the new product ID");
                int.TryParse(Console.ReadLine(), out id);
                tmpProduct.ID = id;
                Console.WriteLine("enter the new product name");
                tmpProduct.Name = Console.ReadLine();
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
                            testOrder(bl.Order);
                            break;
                        case 2:
                            testOrderItem(bl.Cart);
                            break;
                        case 3:
                            testProduct(bl.Product);
                            break;
                        default:
                            break;
                    }
                }
                else
                    Console.WriteLine("ERROR");
            }
            catch (BDontExistException ex)
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

