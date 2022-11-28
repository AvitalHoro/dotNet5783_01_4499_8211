using BLApi;

namespace BlImplementation;

sealed internal class Bl: IBl
{
    public static IBl instance { get; } = new Bl();
    private Bl() { }

    public IOrder Order { set; get; } = new Order();
    public IProduct Product { set; get; } = new Product();
    public ICart Cart { set; get; } = new Cart();
}