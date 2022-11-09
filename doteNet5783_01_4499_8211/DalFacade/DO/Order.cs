using System.Diagnostics;
using System.Xml.Linq;

namespace DalFacade.DO;

public struct Order
{
 //   public override string ToString() => $@"
	//Product ID={ID}: {Name}, 
	//category - {Category}
 //   	Price: {Price}
 //   	Amount in stock: {InStock}
	//";

    public int ID { set; get; }
    public string CostumerName { set; get; }
    public string CostumerEmail { set; get; }
    public string CostumerAdress { set; get; }
    public DateTime OrderDate { set; get; }
    public DateTime ShipDate { set; get; }
    public DateTime DeliveryDate { set; get; }
    public bool isDeleted { set; get; }
}
