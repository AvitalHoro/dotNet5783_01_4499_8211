using BLApi;
namespace BlImplementation;

sealed public class Bl: IBl
{
    public IOrder Order => new Order();
    public IProduct Product => new Product();
    public IOrderTracking OrderTracking => new OrderTracking(); 
    public ICart cart => new Cart();
    public IOrderForList orderForList => new OrderForList();    
    public IOrderItem orderItem => new OrderItem(); 
    public IProductForList productForList => new ProductForList();  
    public IProductItem productItem => new ProductItem();  
}
