
namespace DO;

public struct OrderItem
{
    public override string ToString() => $@"
    	Item ID: {ID} 
        Product ID: {ProductID} 
        Order ID: {OrderID}
    	Price: {Price}
        Amount in stock: {Amount}
    	";
    public int ID { set; get; }
    public int ProductID { set; get; }
    public int OrderID { set; get; }
    public double Price { set; get; }
    public int Amount { set; get; }
    public bool isDeleted { set; get; }
}

