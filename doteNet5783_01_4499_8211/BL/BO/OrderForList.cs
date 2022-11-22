
using DO;

namespace BO;

public class OrderForList
{
    public int ID { get; set; }
    public string CostumerName { get; set; }
    public Status State { get; set; }

    public int ItemsAmount { get; set; }    
    public int TotalPrice { get; set; } 
}
