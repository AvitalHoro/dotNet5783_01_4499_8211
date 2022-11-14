using System;
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
  //      case 'n':cout << "enter the discussion title (with no space) "; cin >> val; tl.addNewTree(val); break;
		//case 's':cout << "enter the discussion title (with no space) "; cin >> title;
  //      cout << "enter the last message (with no space) "; cin >> father;
  //      cout << "enter the new respond "; cin >> son;
  //      if (tl.addResponse(title, father, son)) cout << "success\n"; else cout << "ERROR\n"; break;
		//case 'd':cout << "enter the discussion title (with no space) "; cin >> title;
  //      cout << "enter string of subtree to delete (with no space) "; cin >> val;
  //      if (tl.delResponse(title, val)) cout << "success\n"; else cout << "ERROR\n"; break;
		//case 'p':tl.printAllTrees(); break;
		//case 'r':
		//	cout << "enter the discussion title (with no space) "; cin >> title;
  //      cout << "enter the last message (with no space) "; cin >> val;
  //      tl.printSubTree(title, val); cout << endl; break;
		//case 'w':cout << "enter a string (with no space) "; cin >> val;
  //      tl.searchAndPrint(val); cout << endl; break;
		//case 'e':cout << "bye "; break;
  //      default: cout << "ERROR\n"; break;
        switch (option)
        {
            case "a":
                Product prdct = new Product();
                Console.WriteLine("enter the new product ID");
                prdct.ID = Console.Read();
                Console.WriteLine("enter the new product name");
                prdct.Name = Console.ReadLine();
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
                        prdct.Category = Category.Clothes;
                        break;
                    case 1:
                        prdct.Category = Category.Toys;
                        break;
                    case 2:
                        prdct.Category = Category.Carts;
                        break;
                    case 3:
                        prdct.Category = Category.Bottles;
                        break;
                    case 4:
                        prdct.Category = Category.Diapers;
                        break;
                    default:
                        Console.WriteLine("ERROR");
                        break;
                }
                Console.WriteLine("enter the new product price");
                prdct.Price = Console.Read();
                Console.WriteLine("enter the new product amount");
                prdct.InStock = Console.Read();
                product.Add(prdct);
                break;
            case "b":
                Console.WriteLine("enter the product ID");
                int myId = Console.Read();
                Console.WriteLine(product.GetById(myId));
                break;
            case "c":
                Console.WriteLine(product.GetAll());//איך מדפיסים את הכל?
                break;
            case "d":
                Product prdct2 = new Product();
                Console.WriteLine("enter the new product ID");
                prdct2.ID = Console.Read();
                Console.WriteLine("enter the new product name");
                prdct2.Name = Console.ReadLine();
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
                        prdct2.Category = Category.Clothes;
                        break;
                    case 1:
                        prdct2.Category = Category.Toys;
                        break;
                    case 2:
                        prdct2.Category = Category.Carts;
                        break;
                    case 3:
                        prdct2.Category = Category.Bottles;
                        break;
                    case 4:
                        prdct2.Category = Category.Diapers;
                        break;
                    default:
                        Console.WriteLine("ERROR");
                        break;
                }
                Console.WriteLine("enter the new product price");
                prdct2.Price = Console.Read();
                Console.WriteLine("enter the new product amount");
                prdct2.InStock = Console.Read();
                product.Update(prdct2);
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




}
