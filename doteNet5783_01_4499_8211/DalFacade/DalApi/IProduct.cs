using DO;

namespace DalApi;

public interface IProduct : ICrud<Product>
{
    void BackInStock(int id);
}