
namespace DO;

public struct OrderItem
{
    public override string ToString() { return this.ToStringProperty(); }
    public int ID { set; get; }
    public int ProductID { set; get; }
    public int OrderID { set; get; }
    public double Price { set; get; }
    public int Amount { set; get; }
    public string Path { set; get; }
}

