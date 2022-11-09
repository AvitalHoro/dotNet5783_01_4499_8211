
using System.Diagnostics;
using System.Xml.Linq;

namespace DalFacade.DO;

public struct OrderItem
//{
//    public override string ToString() => $@"
//	Item ID={ID}: {Name}, 
//	category - {Category}
//    	Price: {Price}
//    	Amount in stock: {InStock}
//	";
    public int ID { set; get; }
    public int ProductID { set; get; }
    public int OrderID { set; get; }
    public double Price { set; get; }
    public int Amount { set; get; }
}

