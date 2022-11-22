using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class OrderTracking
{
    public int ID { get; set; }
    public Status State { get; set; }

    public List<Tuple<DateTime, string>>? Tracking { set; get; }//מעקב הזמנה - רשימה של צמדים המכילה תאריך והסבר מה קרה בתאריך זה

}
