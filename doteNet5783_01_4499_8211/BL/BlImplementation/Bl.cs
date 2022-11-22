using BLApi;
namespace BlImplementation;

sealed public class Bl: IBl
{
    public IOrder Order => new Order();
    public IProduct Product => new Product();
    public ICart cart => new Cart();
   
}
