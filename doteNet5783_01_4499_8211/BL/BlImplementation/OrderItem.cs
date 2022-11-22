using BLApi;

namespace BlImplementation;

internal class OrderItem: IOrderItem
{
    private DalApi.IDal Dal = DalApi.DalFactory.GetDal();
}
