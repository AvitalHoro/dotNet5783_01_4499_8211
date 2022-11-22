using BLApi;

namespace BlImplementation;

internal class ProductForList: IProductForList
{
    private DalApi.IDal Dal = DalApi.DalFactory.GetDal();
}
