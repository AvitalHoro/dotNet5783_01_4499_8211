using DalApi;
using DO;

namespace Dal;

public class DalProduct: IProduct
//realizes all the methods of the products
{
    DataSource ds = DataSource.s_instance; 

    public int Add(Product? product)
     //adds a new product to the store and returns the id of the new product
    {
        //if(ds.ListProduct.Find(p => product.GetValueOrDefault().ID == p.GetValueOrDefault().ID)!=null) //checks if the product is already in the store
        //    throw new Exception "The product ia alredey in the store";
        ds.ListProduct.Add(product);
        return product.GetValueOrDefault().ID;
    }
    public Product? GetById(int id)
     //מקבל ת"ז ומחזיר את המוצר שזה הת"ז שלו
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

