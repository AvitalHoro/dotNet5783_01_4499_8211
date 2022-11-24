using BLApi;
using BO;
using DO;

namespace BlImplementation;

internal class Product : IProduct
{
    private DalApi.IDal Dal = DalApi.DalFactory.GetDal() ?? throw new NullReferenceException("Missing Dal");

    public IEnumerable<BO.ProductForList?> GetProductList()
    {
        IEnumerable<DO.Product?> tmp = Dal.Product.GetAll();
        List<BO.ProductForList?> newList = new List<BO.ProductForList?> { };
        foreach (DO.Product? product in tmp)
        {
            newList.Add(new ProductForList
            {
                ID = product.GetValueOrDefault().ID,
                Name = product.GetValueOrDefault().Name,
                Category = product.GetValueOrDefault().Category,
                Price = product.GetValueOrDefault().Price,
            }
            );
        };
        return newList;
    }

    public BO.Product? GetProductDetails(int idProduct)//מנהל
    {
        //if(idProduct <= 0)
        //    throw 
        try
        {
            DO.Product? product = Dal.Product.GetById(idProduct);
            BO.Product boProduct = new BO.Product
            {
                ID = product.GetValueOrDefault().ID,
                Name = product.GetValueOrDefault().Name,
                Category = product.GetValueOrDefault().Category,
                Price = product.GetValueOrDefault().Price,
                InStock = product.GetValueOrDefault().InStock,
                isDeleted = product.GetValueOrDefault().isDeleted,
            };
            return boProduct;
        }
        catch (DontExistException ex)
        {
            Console.WriteLine(ex);
            return null;
        }
    }

    public BO.ProductItem? GetProductDetails(int idProduct, BO.Cart? cart)//לקוח
    {
        try
        {
            DO.Product? product = Dal.Product.GetById(idProduct);
            BO.ProductItem productItem = new BO.ProductItem
            {
                ID = product.GetValueOrDefault().ID,
                Name = product.GetValueOrDefault().Name,
                Category = product.GetValueOrDefault().Category,
                Price = product.GetValueOrDefault().Price,
                isInStock = (product.GetValueOrDefault().InStock > 0), //אולי אפשר לעשות פה דלגט??
            };
            return productItem;
        }
        catch (DontExistException ex)
        {
            Console.WriteLine(ex);
            return null;
        }
    }
    public void AddProduct(BO.Product? newProduct)
    {
        try
        {
            //if(newProduct.ID<0 || newProduct.Name== null||newProduct.Price<0|| newProduct.InStock<=0 )
            //  throw
            DO.Product product = new DO.Product
            {
                ID = newProduct.ID,
                Name = newProduct.Name,
                Category = newProduct.Category,
                Price = newProduct.Price,
                InStock = newProduct.InStock,
                isDeleted = newProduct.isDeleted,
            };
            Dal.Product.Add(product);
        }
        catch()
        {

        }
    }

    public void RemoveProduct(int idProduct)
    { 
        try
        {
            if (Dal.OrderItem.GetAllProduct(idProduct) == null) 
                //אם ההזמנה כבר נשלחה , האם עדיין זה בעייתי למחוק את המוצר?
                //האם עבור רשימה ריקה, הוא יחזיר באמת נל
                throw new ProductExistInOrderException(idProduct);
            Dal.Product.Delete(idProduct);
        }
        catch(DontExistException ex)
        {
            Console.WriteLine(ex);
        }
    }

    public void UpdateProductDetails(BO.Product? product)
    {
        try
        {
            //if(newProduct.ID<0 || newProduct.Name== null||newProduct.Price<0|| newProduct.InStock<=0 )
            //  throw
            DO.Product productDo = new DO.Product
            {
                ID = product.ID,
                Name = product.Name,
                Category = product.Category,
                Price = product.Price,
                InStock = product.InStock,
                isDeleted = product.isDeleted,
            };
            Dal.Product.Update(productDo);
        } 
        catch()
        {

        }
    }
}
