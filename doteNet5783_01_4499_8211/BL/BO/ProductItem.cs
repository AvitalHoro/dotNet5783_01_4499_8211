
using DO;

namespace BO;

public class ProductItem
{
    public int ID { get; set; }
    public string Name { get; set; }
    public Category Category { get; set; }
    public double Price { get; set; }

    public bool isInStock { get; set; } 

    public int AmountInCart { get; set; } 
}
