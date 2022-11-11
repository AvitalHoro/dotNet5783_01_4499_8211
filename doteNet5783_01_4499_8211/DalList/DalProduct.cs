using DalApi;
using DO;

namespace Dal;

public class DalProduct: IProduct
{
    public int Add(Product product)
    {
        DataSource.ListProduct.Add(product);
        return 0;
    }
    public Product GetById(int id)
    {
        return (DataSource.ListProduct.Find(product => product.ID == id));
    }
    public void Update(Product product)
    {
        Product temp = DataSource.ListProduct.Find(found => found.ID == product.ID);
        //if (Product==null) 
        //    return; //לשאול את נורית
        DataSource.ListProduct.Remove(temp);
        DataSource.ListProduct.Add(product);
    }
    public void Delete(int id)
    {
        DataSource.ListProduct.RemoveAll(product => product.ID == id);
    }

    //IEnumerable<T?> GetAll(Func<T?, bool>? filter = null);
    public IEnumerable<Product> GetAll()
    {
        return DataSource.ListProduct;
    }
}

