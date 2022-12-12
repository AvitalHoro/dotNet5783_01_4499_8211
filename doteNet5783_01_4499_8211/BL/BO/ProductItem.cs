
using DO;

namespace BO;

public class ProductItem
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public Category Category { get; set; }
    public double Price { get; set; }
    public bool IsInStock { get; set; } 
    public int AmountInCart { get; set; }
    public string? Path { set; get; }
    public override string ToString() { return this.ToStringProperty(); }
}
