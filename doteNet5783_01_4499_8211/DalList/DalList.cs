using DalApi;

namespace Dal;

sealed internal class DalList:IDal
{
    public IOrdered Order => new DalOrder();

    public IProduct Product => new DalProduct();

    public IOrderedItem OrderItem => new DalOrderItem();
}
