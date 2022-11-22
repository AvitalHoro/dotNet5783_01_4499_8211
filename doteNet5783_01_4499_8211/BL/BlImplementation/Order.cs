using BLApi;

namespace BlImplementation;

internal class Order: IOrder
{
    private DalApi.IDal Dal = DalApi.DalFactory.GetDal();

}
