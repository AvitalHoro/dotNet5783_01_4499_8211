using BLApi;
using System.Security.Cryptography;

namespace BlImplementation;

internal class Product : IProduct
{
    private DalApi.IDal Dal = DalApi.DalFactory.GetDal() ?? throw new NullReferenceException("Missing Dal");

    public IEnumerable<BO.ProductForList?> GetProductList()
    {
        IEnumerable<DO.Product?> tmp = Dal.Product.GetAll();
        BO.ProductForList? productBo = new BO.ProductForList();
        var newList = from DO.Product? product in tmp select BO.Tools.CopyPropTo(product, ref productBo);
        return newList;
    }

    public IEnumerable<BO.Product?> GetCatalog()
    {
        IEnumerable<DO.Product?> tmp = Dal.Product.GetAll();
        BO.Product productBo = new BO.Product();
        var newList = from DO.Product? product in tmp select BO.Tools.CopyPropTo(product, ref productBo);
        return newList;
    }

    public BO.Product? GetProductDetails(int idProduct)//מנהל
    {
        try
        {
            if (idProduct <= 0)
                throw new BO.InvalidIDException(idProduct);
        }
        catch (BO.InvalidIDException ex) { Console.WriteLine(ex); }
        try
        {
            DO.Product? product = Dal.Product.GetById(idProduct);
            BO.Product boProduct = new BO.Product();
            BO.Tools.CopyPropTo(product, ref boProduct);
            return boProduct;
        }
        catch (BO.DontExistException ex)
        {
            throw new DO.DontExistException(ex.ID, ex.Message, ex);
        }
    }

    public BO.ProductItem? GetProductDetails(int idProduct, BO.Cart? cart)//לקוח
    {
        try
        {
            DO.Product? product = Dal.Product.GetById(idProduct);
            BO.ProductItem productItem = new BO.ProductItem();
            BO.Tools.CopyPropTo(product, ref productItem);
            productItem.isInStock = (product.GetValueOrDefault().InStock > 0);
            return productItem;
        }
        catch (BO.DontExistException ex)
        {
            throw new DO.DontExistException(ex.ID, ex.Message, ex);
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
        catch (BO.InvalidIDException ex) { Console.WriteLine(ex); }
        catch (BO.NoNameException ex) { Console.WriteLine(ex); }
        catch (BO.InvalidPriceException ex) { Console.WriteLine(ex); }
        catch (BO.OutOfStockException ex) { Console.WriteLine(ex); }

        DO.Product product = new DO.Product();
        BO.Tools.CopyPropTo(newProduct, ref product);
        try
        {
            Dal.Product.Add(product);
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
        catch (BO.DontExistException ex)
        {
            Console.WriteLine(ex);
        }
        try
        {
            Dal.Product.Delete(idProduct);
        }
        catch (DO.DontExistException ex)
        { throw new BO.DontExistException(ex.ID, ex.Message, ex); }
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
        catch (BO.InvalidIDException ex) { Console.WriteLine(ex); }
        catch (BO.NoNameException ex) { Console.WriteLine(ex); }
        catch (BO.InvalidPriceException ex) { Console.WriteLine(ex); }
        catch (BO.OutOfStockException ex) { Console.WriteLine(ex); }
        DO.Product productDo = new DO.Product();
        BO.Tools.CopyPropTo(product, ref productDo);
        try
        {
            Dal.Product.Update(productDo);
        }
        catch (DO.DontExistException ex) { throw new BO.DontExistException(ex.ID, ex.Message, ex); }
    }
}
