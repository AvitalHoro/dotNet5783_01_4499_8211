using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

// Entity that displays the relevant details for tracking the order
public class OrderTracking
{
    public int ID { get; set; }
    public Status State { get; set; }
    public List<Tuple<DateTime?, string>>? Tracking { set; get; } // Order tracking - a list of pairs containing a date and an explanation of what happened on that date
    public override string ToString() { return this.ToStringProperty(); }
}
