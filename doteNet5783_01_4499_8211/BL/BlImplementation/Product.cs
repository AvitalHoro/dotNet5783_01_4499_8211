using BLApi;

namespace BlImplementation;

internal class Product:IProduct
{
    private DalApi.IDal Dal = DalApi.DalFactory.GetDal();
}
