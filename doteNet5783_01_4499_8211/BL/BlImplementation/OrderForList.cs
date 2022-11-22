using BLApi;

namespace BlImplementation;

internal class OrderForList: IOrderForList
{
    private DalApi.IDal Dal = DalApi.DalFactory.GetDal();
}
