using DalApi;
using DO;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Dal;

public class DalProduct: IProduct
//realizes all the methods of the products
{
    readonly DataSource ds = DataSource.s_instance; //כדי שלא ניצור כל פעם עצם חדש של נתונים

    #region Add
    /// <exception cref="AlreadyExistsException"></exception>
    public int Add(Product product)
     //פונקציה להוספת מוצר חדש לרשימת המוצרים
    {
        Product? newProduct = ds.ListProduct.FirstOrDefault(p => product.ID == p?.ID);
        if (newProduct != null)
            if ((bool)newProduct?.IsDeleted!)
                ds.ListOrder.RemoveAll(p => product.ID == p?.ID);
            else
                throw new AlreadyExistsException(product.ID);
        ds.ListProduct.Add(product);
        return product.ID;
    }
    #endregion

    #region GetById
    /// <exception cref="DoesNotExistException"></exception>
    public Product GetById(int id)
     //מקבל ת"ז ומחזיר את המוצר שזה הת"ז שלו
    {
        Product product = ds.ListProduct.FirstOrDefault(p => p?.ID == id)
            ?? throw new DoesNotExistException(id);
        if (product.IsDeleted) throw new DoesNotExistException(id);//checks if the product is already in the store
        return product;
    }
    #endregion

    #region Update
    /// <exception cref="DoesNotExistException"></exception>
    public void Update(Product product)
        //הפונקציה מעדכנת מוצר מסוים ברשימת הפרודוקטים, ומוצאת את הקודם ע"י הת"ז שנשארת אותו הדבר
    {
        Product newProduct = ds.ListProduct.FirstOrDefault(found => found?.ID == product.ID)
            ?? throw new DoesNotExistException(product.ID);
        if (newProduct.IsDeleted)
            throw new DoesNotExistException(product.ID);
        ds.ListProduct.Remove(newProduct);
        ds.ListProduct.Add(product);
    }
    #endregion

    #region Delete
    /// <exception cref="DoesNotExistException"></exception>
    public void Delete(int id)
    //מוחק מוצר מהרשימה
   //הפונקציה לא באמת מוחקת את ההזמנה אלא רק מעדכנת בפרטים שלה שהיא מחוקה
    {
        Product found = ds.ListProduct.FirstOrDefault(item => item?.ID == id) 
            ?? throw new DoesNotExistException(id);

        if (found.IsDeleted)
                    //בודק אם ההזמנה מחוקה כבר ברשימה, ואם כן זורק חריגה
            throw new DoesNotExistException(id);
   
        Product product = new() //בונה מוצר חדש עם אותם ערכים בדיוק, משנה רק את הערך של המחיקה
        {
            ID = id,
            Name = found.Name,
            Category = found.Category,
            InStock = found.InStock,
            Price = found.Price,
            IsDeleted = true,
            Path = found.Path,
        };
        Update(product); //מעדכן ברשימה את ההזמנה המחוקה
    }
    #endregion

    #region GetAll
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? filter = null)
    {
        if (filter == null)
            return ds.ListProduct;
        return (from Product? product in ds.ListProduct 
                where filter!(product)
                select product)
                .ToList();
    }
    #endregion

    public void BackInStock(int id)
    {
        Product found = ds.ListProduct.FirstOrDefault(item => item?.ID == id)
           ?? throw new DoesNotExistException(id);

        Product product = new() //בונה מוצר חדש עם אותם ערכים בדיוק, משנה רק את הערך של המחיקה
        {
            ID = id,
            Name = found.Name,
            Category = found.Category,
            InStock = found.InStock,
            Price = found.Price,
            IsDeleted = false,
            Path = found.Path,
        };

        ds.ListProduct.Remove(found);
        ds.ListProduct.Add(product);
    }
}

