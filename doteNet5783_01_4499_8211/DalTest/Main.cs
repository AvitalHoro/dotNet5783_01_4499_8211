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
                Console.WriteLine("enter the new product ID")
                break;
            case "b":

                break;
            case "c":
                break;
            case "d":

                break;
            case "e":

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
