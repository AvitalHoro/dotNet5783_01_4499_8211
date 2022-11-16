using DalApi;
using DO;

namespace Dal;

public class DalProduct: IProduct
{
    DataSource ds = DataSource.s_instance;

    public int Add(Product? product)
    {
        ds.ListProduct.Add(product);
        return ds.ListProduct.Count();//צריך להחזיר כאן את התז
    }
    public Product? GetById(int id)
    {
        return (ds.ListProduct.Find(product => product.GetValueOrDefault().ID == id));
    }
    public void Update(Product? product)
    {
        Product? temp = ds.ListProduct.Find(found => found.GetValueOrDefault().ID == product.GetValueOrDefault().ID);
        if (temp==null) 
           return; 
       int i= ds.ListProduct.IndexOf(temp);
        ds.ListProduct[i]=product;
    }
    public void Delete(int id)
    {
        ds.ListProduct.RemoveAll(product => product.GetValueOrDefault().ID == id);
    }

    //IEnumerable<T?> GetAll(Func<T?, bool>? filter = null);
    public IEnumerable<Product?> GetAll()
    {
        List <Product?> temp= ds.ListProduct;
        return temp; //לבדוק שבאמת עושה העתקה רדודה
    }
}

