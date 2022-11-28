﻿
using System.Reflection.Metadata.Ecma335;

namespace DO;

public struct Order
//ממשק של הזמנה
{
    //מדפיס את כל פרטי ההזמנה
    public override string ToString() { return this.ToStringProperty(); }
    public int ID { set; get; }
    public string CostumerName { set; get; }
    public string CostumerEmail { set; get; }
    public string CostumerAdress { set; get; }
    public DateTime? OrderDate { set; get; }
    public DateTime? ShipDate { set; get; }
    public DateTime? DeliveryDate { set; get; }
    public bool isDeleted { set; get; }
}
