using DalFacade.DO;

namespace DalFacade.DalApi;

public interface IOrder :ICrud<Order>
{
    int Add(Order item)
    {

    }
    Order GetById(int id)
    {

    }
    void Update(Order item)
    {

    }
    void Delete(int id)
    {

    }

    //IEnumerable<T?> GetAll(Func<T?, bool>? filter = null);
    IEnumerable<Order> GetAll()
    {

    }
}
