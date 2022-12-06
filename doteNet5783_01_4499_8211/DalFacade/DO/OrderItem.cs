
namespace DO;

public struct OrderItem
    //ממשק של פריט שהוזמן
{
    //מדפיסה את כל פרטי הפריט
    public override string ToString() { return this.ToStringProperty(); }
    public int ID { set; get; }
    public int ProductID { set; get; }
    public int OrderID { set; get; }
    public double Price { set; get; }
    public int Amount { set; get; }
    public bool IsDeleted { set; get; }
}

