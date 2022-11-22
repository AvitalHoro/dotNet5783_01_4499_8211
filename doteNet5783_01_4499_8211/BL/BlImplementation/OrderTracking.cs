using BLApi;

namespace BlImplementation;

internal class OrderTracking:IOrderTracking
{
    private DalApi.IDal Dal = DalApi.DalFactory.GetDal();
}
