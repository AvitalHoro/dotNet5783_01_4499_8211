using BLApi;
using BO;

namespace BlImplementation;

internal class Product:IProduct
{
    private DalApi.IDal Dal = DalApi.DalFactory.GetDal();

    public IEnumerable<BO.ProductForList?> GetProductList()
    {
        IEnumerable<DO.Product?> tmp= Dal.Product.GetAll();
        ProductForList product =
        from product1 in tmp 
        select ;

    }
    public BO.Product? GetProductDetails(int idProduct);//מנהל
    public BO.ProductItem? GetProductDetails(int idProduct, BO.Cart? cart);//לקוח
    public void AddProduct(BO.Product? newProduct);
    public void RemoveProduct(int idProduct);
    public void UpdateProductDetails(BO.Product? product);
}
