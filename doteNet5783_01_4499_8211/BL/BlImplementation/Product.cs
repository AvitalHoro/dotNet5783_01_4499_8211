using BLApi;
using BO;
using System;
using DO;
using System.Security.Cryptography;
using System.Runtime.Intrinsics.Arm;

namespace BlImplementation;

internal class Product : IProduct
{
    private DalApi.IDal Dal = DalApi.DalFactory.GetDal() ?? throw new NullReferenceException("Missing Dal");

    #region GetProductList
    public IEnumerable<BO.ProductForList> GetProductList(BO.Filters enumFilter = BO.Filters.None, 
        Object? filterValue = null, bool isInStock= false)
    {
        if (isInStock)
        {
            IEnumerable<DO.Product?> doProductList =
            enumFilter switch
            {
                BO.Filters.filterByCategory =>
                Dal!.Product.GetAll(dp => (dp?.Category == (filterValue != null ? (DO.Category)filterValue : DO.Category.All) && dp?.IsDeleted == false && dp?.InStock > 0 )),

                BO.Filters.filterByName =>
                Dal!.Product.GetAll(dp => ((dp?.Name.Contains((string?)(filterValue)) == true || 
                (dp?.Name.ToLower()).Contains(((string)(filterValue)).ToLower()) == true) && dp?.IsDeleted == false && dp?.InStock > 0)),

                BO.Filters.None =>
                Dal!.Product.GetAll(dp=> dp?.IsDeleted == false && dp?.InStock > 0),
                _ => Dal!.Product.GetAll(dp => dp?.IsDeleted == false && dp?.InStock > 0),
            };

            return (from DO.Product doProduct in doProductList
                    select BO.Tools.CopyPropTo(doProduct, new BO.ProductForList()))
                   .ToList();
        }

        else
        {
            IEnumerable<DO.Product?> doProductList =
         enumFilter switch
         {
             BO.Filters.filterByCategory =>
             Dal!.Product.GetAll(dp => (dp?.Category == (filterValue != null ? (DO.Category)filterValue : DO.Category.All) && dp?.IsDeleted == false)),

             BO.Filters.filterByName =>
             Dal!.Product.GetAll(dp => (dp?.Name.Contains((string?)(filterValue)) == true || (dp?.Name.ToLower()).Contains(((string)(filterValue)).ToLower()) == true) && dp?.IsDeleted == false),

             BO.Filters.deleted =>
             Dal!.Product.GetAll(dp => dp?.IsDeleted == true),

             BO.Filters.None =>
             Dal!.Product.GetAll(dp=> dp?.IsDeleted == false),
             _ => Dal!.Product.GetAll(dp=> dp?.IsDeleted == false),
         };

            return (from DO.Product doProduct in doProductList
                    select BO.Tools.CopyPropTo(doProduct, new BO.ProductForList()))
                   .ToList();
        }
    }
    #endregion

    #region GetCatalog
    //מחזיר רשימה של כל המוצרים בשביל הלקוח
    public IEnumerable<BO.ProductItem?> GetCatalog(BO.Cart cart, BO.Filters enumFilter = BO.Filters.None,
        Object? filterValue = null, bool isInStock = false)
    {
        IEnumerable<BO.ProductForList> tmp = GetProductList(enumFilter, filterValue, isInStock);
        return tmp.Select(product => GetProductDetails(product.ID, cart));
    }
    #endregion

    #region GetProductDetails
    /// <exception cref="BO.InvalidIDException"></exception>
    /// <exception cref="DO.DoesNotExistException"></exception>
    //מחזיר למנהל פרטים של מוצר ספציפי לפי המזהה שלו
    public BO.Product GetProductDetails(int idProduct)
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
            BO.Tools.CopyPropTo(product, boProduct);
            return boProduct;
        }
        catch (BO.DoesNotExistException ex)
        {
            throw new DO.DoesNotExistException(ex.ID, ex.Message, ex);
        }
    }
    #endregion

    #region GetProductDetails
    /// <exception cref="DO.DoesNotExistException"></exception>
    //מחזיר ללקוח פרטים של מוצר ספציפי לפי המזהה שלו
    public BO.ProductItem GetProductDetails(int idProduct, BO.Cart cart)
    {
        try
        {
            DO.Product product = Dal.Product.GetById(idProduct);
            BO.ProductItem productItem = new BO.ProductItem();
            BO.Tools.CopyPropTo(product, productItem);
            BO.OrderItem? item = cart.OrderItems.FirstOrDefault(x => x.ProductID == productItem.ID)??throw new BO.DoesNotExistException(productItem.ID);
            if (item != null)
                productItem.AmountInCart = item.Amount;
             else
                productItem.AmountInCart = 0;
            return productItem;
        }
        catch (BO.DoesNotExistException ex)
        {
            throw new DO.DoesNotExistException(ex.ID, ex.Message, ex);
        }
    }
    #endregion 

    #region AddProduct
    /// <exception cref="BO.InvalidIDException"></exception>
    /// <exception cref="BO.NoNameException"></exception>
    /// <exception cref="BO.InvalidPriceException"></exception>
    /// <exception cref="BO.OutOfStockException"></exception>
    /// <exception cref="BO.AlreadyExistsException"></exception>
    //מוסיף מוצר חדש לחנות
    public void AddProduct(BO.Product? newProduct)
    {
        try
        {
            if (newProduct?.ID < 0)
                throw new BO.InvalidIDException(newProduct.ID);
            if (newProduct?.Name == null)
                throw new BO.NoNameException(newProduct!.ID);
            if (newProduct.Price < 0)
                throw new BO.InvalidPriceException(newProduct.ID);
            if (newProduct.InStock <= 0)
                throw new BO.OutOfStockException(newProduct.ID);
        }
        catch (BO.InvalidIDException ex) { throw new BO.InvalidIDException(ex.ID); }
        catch (BO.NoNameException ex) { throw new BO.NoNameException(ex.ID); }
        catch (BO.InvalidPriceException ex) { throw new BO.InvalidPriceException(ex.ID); }
        catch (BO.OutOfStockException ex) { throw new BO.OutOfStockException(ex.ID); }

        try
        {
            Dal.Product.Add((DO.Product)BO.Tools.CopyPropToStruct(newProduct, typeof(DO.Product)));
        }
        catch (DO.AlreadyExistsException ex) { throw new BO.AlreadyExistsException(ex.ID, ex.Message, ex); }
    }
    #endregion

    #region RemoveProduct
    /// <exception cref="BO.ProductExistInOrderException"></exception>
    /// <exception cref="BO.DoesNotExistException"></exception>
    //מוחק מוצר מהחנות
    public void RemoveProduct(int idProduct)
    {
        try
        {
            var items = Dal.OrderItem.GetAll(item => item?.ProductID == idProduct);
            var list = from DO.OrderItem item in items
                       where Dal.Order.GetById(item.OrderID).ShipDate == null
                       select item;
            if(list.Count() == 0)
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
    #endregion

    #region UpdateProductDetails
    /// <exception cref="BO.InvalidIDException"></exception>
    /// <exception cref="BO.NoNameException"></exception>
    /// <exception cref="BO.InvalidPriceException"></exception>
    /// <exception cref="BO.OutOfStockException"></exception>
    /// <exception cref="BO.DoesNotExistException"></exception>
    //מקבל מוצר ולפי המזהה שלו מעדכן את המוצר בחנות
    public void UpdateProductDetails(BO.Product? product)
    {
        try
        {
            //בדיקות תקינות לערכי מוצר
            if (product?.ID < 0)
                throw new BO.InvalidIDException(product.ID);
            if (product?.Name == null)
                throw new BO.NoNameException(product!.ID);
            if (product.Price < 0)
                throw new BO.InvalidPriceException(product.ID);
            if (product.InStock <= 0)
                throw new BO.OutOfStockException(product.ID);
        }
        catch (BO.InvalidIDException ex) { throw new BO.InvalidIDException(ex.ID); }
        catch (BO.NoNameException ex) { throw new BO.NoNameException(ex.ID); }
        catch (BO.InvalidPriceException ex) { throw new BO.InvalidPriceException(ex.ID); }
        catch (BO.OutOfStockException ex) { throw new BO.OutOfStockException(ex.ID); }
        try
        {
            Dal.Product.Update((DO.Product)BO.Tools.CopyPropToStruct(product, typeof(DO.Product)));
        }
        catch (DO.DoesNotExistException ex) { throw new BO.DoesNotExistException(ex.ID, ex.Message, ex); }
    }
    #endregion
}

