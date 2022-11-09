
using System.Diagnostics;
using System.Xml.Linq;

namespace DalFacade.DO;

public struct Product
{
    public override string ToString() => $@"
	Product ID={ID}: {Name}, 
	category - {Category}
    	Price: {Price}
    	Amount in stock: {InStock}
	";
    public int ID { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public double Price { get; set; }
    public int InStock { get; set; }
    public bool isDeleted { set; get; }
}
