using BLApi;
namespace BlImplementation;

sealed public class Bl: IBl
{
    public IOrder Order { set; get; } = new Order();
    public IProduct Product { set; get; } = new Product();
    public ICart cart { set; get; } = new Cart();
}
