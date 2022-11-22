
using DO;

namespace BO;

public class Order
{
    //מדפיס את כל פרטי ההזמנה
    public int ID { set; get; }
    public string CostumerName { set; get; }
    public string CostumerEmail { set; get; }
    public string CostumerAdress { set; get; }
    public Status State { get; set; }
    public DateTime? OrderDate { set; get; }
    public DateTime? ShipDate { set; get; }
    public DateTime? DeliveryDate { set; get; }
    public bool isDeleted { set; get; }
    public List <OrderItem> Items { set; get; }
    public double TotalPrice { get; set; }
}
