﻿
namespace BO;

public class OrderItem
{
    public int ID { set; get; }
    public int ProductID { set; get; }
    public string? NameProduct { set; get; }
    public double Price { set; get; }
    public double TotalPrice { set; get; }
    public int Amount { set; get; }
    public bool IsDeleted { set; get; }
    public string? Path { set; get; }
    public override string ToString() { return this.ToStringProperty(); }
}
