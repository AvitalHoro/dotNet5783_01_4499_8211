using BO;

namespace BLApi;

public interface IBl
{
    public ICart Cart { get; }
    public IOrder Order { get; }
    public IProduct Product { get; }
}
