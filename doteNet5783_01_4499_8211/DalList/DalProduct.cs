using DalApi;
using DO;

namespace Dal;

public class DalProduct: IProduct
{
    public int Add(Product? product)
    {
        DataSource.ListProduct.Add(product);
        return DataSource.ListProduct.Count();//צריך להחזיר כאן את התז
    }
    public Product? GetById(int id)
    {
        return (DataSource.ListProduct.Find(product => product.GetValueOrDefault().ID == id));
    }
    public void Update(Product? product)
    {
        Product? temp = DataSource.ListProduct.Find(found => found.GetValueOrDefault().ID == product.GetValueOrDefault().ID);
        if (temp==null) 
           return; 
       int i= DataSource.ListProduct.IndexOf(temp);
        DataSource.ListProduct[i]=product;
    }
    public void Delete(int id)
    {
        DataSource.ListProduct.RemoveAll(product => product.GetValueOrDefault().ID == id);
    }

    //IEnumerable<T?> GetAll(Func<T?, bool>? filter = null);
    public IEnumerable<Product?> GetAll()
    {
        List <Product?> temp= DataSource.ListProduct;
        return temp; //לבדוק שבאמת עושה העתקה רדודה
    }
}

