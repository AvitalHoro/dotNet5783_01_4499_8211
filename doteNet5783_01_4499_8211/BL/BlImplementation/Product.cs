using BLApi;
using System.Security.Cryptography;

namespace BlImplementation;

internal class Product : IProduct
{
    private DalApi.IDal Dal = DalApi.DalFactory.GetDal() ?? throw new NullReferenceException("Missing Dal");

    public IEnumerable<BO.ProductForList?> GetProductList()
    {
        IEnumerable<DO.Product> tmp = Dal.Product.GetAll();
        BO.ProductForList? productBo = new BO.ProductForList();
        return (from DO.Product? product in tmp 
                select BO.Tools.CopyPropTo(product, productBo)); 
    }

    public IEnumerable<BO.Product?> GetCatalog()
    {
        IEnumerable<DO.Product> tmp = Dal.Product.GetAll();
        BO.Product productBo = new BO.Product();
        var newList = from DO.Product? product in tmp 
                      select BO.Tools.CopyPropTo(product,  productBo);
        return newList;
    }

    public BO.Product? GetProductDetails(int idProduct)//מנהל
    {
        try
        {
            if (idProduct <= 0)
                throw new BO.InvalidIDException(idProduct);
        }
        catch (BO.InvalidIDException ex) { throw new BO.InvalidIDException(ex.ID); }
        try
        {
            DO.Product? product = Dal.Product.GetById(idProduct);
            BO.Product boProduct = new BO.Product();
            BO.Tools.CopyPropTo(product,  boProduct);
            return boProduct;
        }
        catch (BO.DoesNotExistException ex)
        {
            throw new DO.DoesNotExistException(ex.ID, ex.Message, ex);
        }
    }

    public BO.ProductItem? GetProductDetails(int idProduct, BO.Cart? cart)//לקוח
    {
        try
        {
            DO.Product? product = Dal.Product.GetById(idProduct);
            BO.ProductItem productItem = new BO.ProductItem();
            BO.Tools.CopyPropTo(product,  productItem);
            productItem.isInStock = (product.GetValueOrDefault().InStock > 0);
            return productItem;
        }
        catch (BO.DoesNotExistException ex)
        {
            throw new DO.DoesNotExistException(ex.ID, ex.Message, ex);
        }
    }
    public void AddProduct(BO.Product? newProduct)
    {
        try
        {
            if (newProduct.ID < 0)
                throw new BO.InvalidIDException(newProduct.ID);
            if (newProduct.Name == null)
                throw new BO.NoNameException(newProduct.ID);
            if (newProduct.Price < 0)
                throw new BO.InvalidPriceException(newProduct.ID);
            if (newProduct.InStock <= 0)
                throw new BO.OutOfStockException(newProduct.ID);
        }
        catch (BO.InvalidIDException ex) { throw new BO.InvalidIDException(ex.ID); }
        catch (BO.NoNameException ex) { throw new BO.NoNameException(ex.ID); }
        catch (BO.InvalidPriceException ex) { throw new BO.InvalidPriceException(ex.ID); }
        catch (BO.OutOfStockException ex) { throw new BO.OutOfStockException(ex.ID); }

        DO.Product product = new DO.Product();
        try
        {
            Dal.Product.Add((DO.Product)BO.Tools.CopyPropToStruct(newProduct, typeof(DO.Product)));
        }
        catch (DO.AlreadyExistsException ex) { throw new BO.AlreadyExistsException(ex.ID, ex.Message, ex); }
    }


    public void RemoveProduct(int idProduct)
    {
        try
        {
            if (Dal.OrderItem.GetAllProduct(idProduct) == null)
                //אם ההזמנה כבר נשלחה , האם עדיין זה בעייתי למחוק את המוצר?
                //האם עבור רשימה ריקה, הוא יחזיר באמת נל
                throw new BO.ProductExistInOrderException(idProduct);
        }
        catch (BO.DoesNotExistException ex)
        {
            throw new BO.ProductExistInOrderException(ex.ID);
        }
        try
        {
            Dal.Product.Delete(idProduct);
        }
        catch (DO.DoesNotExistException ex)
        { throw new BO.DoesNotExistException(ex.ID, ex.Message, ex); }
    }

    public void UpdateProductDetails(BO.Product? product)
    {
        try
        {
            if (product.ID < 0)
                throw new BO.InvalidIDException(product.ID);
            if (product.Name == null)
                throw new BO.NoNameException(product.ID);
            if (product.Price < 0)
                throw new BO.InvalidPriceException(product.ID);
            if (product.InStock <= 0)
                throw new BO.OutOfStockException(product.ID);
        }
        catch (BO.InvalidIDException ex) { throw new BO.InvalidIDException(ex.ID); }
        catch (BO.NoNameException ex) { throw new BO.NoNameException(ex.ID); }
        catch (BO.InvalidPriceException ex) { throw new BO.InvalidPriceException(ex.ID); }
        catch (BO.OutOfStockException ex) { throw new BO.OutOfStockException(ex.ID); }
        DO.Product productDo = new DO.Product();
        BO.Tools.CopyPropTo(product,  productDo);
        try
        {
            Dal.Product.Update(productDo);
        }
        catch (DO.DoesNotExistException ex) { throw new BO.DoesNotExistException(ex.ID, ex.Message, ex); }
    }
}
