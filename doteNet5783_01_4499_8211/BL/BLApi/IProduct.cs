
namespace BLApi;

public interface IProduct
{
    public IEnumerable<BO.ProductForList> GetProductList(BO.Filters enumFilter = BO.Filters.None,
        Object? filterValue = null, bool isInStock= false );
    public IEnumerable<BO.ProductItem?> GetCatalog(BO.Cart cart, BO.Filters enumFilter = BO.Filters.None,
        Object? filterValue = null, bool isInStock = false); //לקוח
    public BO.Product GetProductDetails(int idProduct);//מנהל
    public BO.ProductItem GetProductDetails(int idProduct, BO.Cart cart);//לקוח
    public void AddProduct(BO.Product newProduct);
    public void RemoveProduct(int idProduct);
    public void UpdateProductDetails(BO.Product product);
    public void RestoreProduct(int idProduct);
}

