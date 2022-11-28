
using BlImplementation;

namespace BLApi;

public interface IProduct
{
    public IEnumerable<BO.ProductForList?> GetProductList();
    public BO.Product? GetProductDetails(int idProduct);//מנהל
    public BO.ProductItem? GetProductDetails(int idProduct, BO.Cart? cart);//לקוח
    public void AddProduct(BO.Product? newProduct);
    public void RemoveProduct(int idProduct);
    public void UpdateProductDetails(BO.Product? product);
}

