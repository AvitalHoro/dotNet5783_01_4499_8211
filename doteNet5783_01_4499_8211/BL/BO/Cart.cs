
using DO;

namespace BO;

public class Cart
{
    public string CostumerName { set; get; }
    public string CostumerEmail { set; get; }
    public string CostumerAdress { set; get; }
    public List <OrderItem?> orderItems { set; get; }
    public double TotalPrice { get; set; }
}
