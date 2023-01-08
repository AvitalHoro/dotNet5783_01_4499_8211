﻿
namespace DO;

//ממשק של מוצר
public struct Product
{
    //מדפיסה את כל פרטי המוצר
    public override string ToString() { return this.ToStringProperty(); }
    public int ID { get; set; }
    public string Name { get; set; }
    public Category? Category { get; set; }
    public double Price { get; set; }
    public int InStock { get; set; }
    public bool IsDeleted { set; get; }
    public string Path { set; get; }
}
