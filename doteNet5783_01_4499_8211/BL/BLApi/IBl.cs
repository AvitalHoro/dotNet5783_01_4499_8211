using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLApi;

public interface IBl
{
    public ICart Cart { get; }
    public IOrder Order { get; }
    public IOrderItem OrderItem { get; }
    public IProductForList ProductForList { get; }  
    public IOrderTracking orderTracking { get; }
    public IOrderForList orderForList { get; }
    public IProduct Product { get; }
    public IProductForList productForList { get; }  
    public IProductItem productItem { get; }    
}
