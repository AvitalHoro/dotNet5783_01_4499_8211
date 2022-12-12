using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class ProductForList
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public Category Category { get; set; }
    public double Price { get; set; }
    public bool IsDeleted { set; get; }
    public string? Path { set; get; }
    public override string ToString() { return this.ToStringProperty(); }
}
