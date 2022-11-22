
namespace BO;

public class OrderItem
{
    //מדפיסה את כל פרטי הפריט
    public override string ToString() => $@"
    	Item ID: {ID} 
        Product ID: {ProductID} 
    	Price: {Price}
        Amount in stock: {Amount}
    	";
    public int ID { set; get; }
    public int ProductID { set; get; }
    public string NameProduct { set; get; }
    public double Price { set; get; }
    public double TotalPrice { set; get; }
    public int Amount { set; get; }
    public bool isDeleted { set; get; }
}
