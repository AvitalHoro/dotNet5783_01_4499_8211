
using DO;

namespace BO;

public class Product
{
    //מדפיסה את כל פרטי המוצר
    public int ID { get; set; }
    public string Name { get; set; }
    public Category Category { get; set; }
    public double Price { get; set; }
    public int InStock { get; set; }
    public bool isDeleted { set; get; }
    public string Path { set; get; }
    public override string ToString() { return this.ToStringProperty(); }
}
