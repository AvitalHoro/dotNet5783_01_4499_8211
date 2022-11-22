using BLApi;
namespace BlImplementation;

internal class Cart :ICart
{
    private DalApi.IDal Dal = DalApi.DalFactory.GetDal();


}
