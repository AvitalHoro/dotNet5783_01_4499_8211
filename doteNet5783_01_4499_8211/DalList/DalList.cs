using DalApi;

namespace Dal;

// An "umbrella" class. Its purpose is to centralize the functionality of all data access entities.
// Through it, access to all types of data can be created.

sealed internal class DalList: IDal
{
    public static IDal Instance { get; } = new DalList();

    private DalList() { } 

    public IOrder Order => new DalOrder(); 

    public IProduct Product => new DalProduct();

    public IOrderItem OrderItem => new DalOrderItem();
}