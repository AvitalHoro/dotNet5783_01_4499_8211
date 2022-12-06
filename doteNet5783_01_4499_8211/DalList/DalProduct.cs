using DalApi;
using DO;
using System.Security.Cryptography;

namespace Dal;

public class DalProduct: IProduct
//realizes all the methods of the products
{
    readonly DataSource ds = DataSource.s_instance; //כדי שלא ניצור כל פעם עצם חדש של נתונים

    public int Add(Product product)
     //פונקציה להוספת מוצר חדש לרשימת המוצרים
    {
        Product? newProduct = ds.ListProduct.FirstOrDefault(p => product.ID == p?.ID);
        if (newProduct != null)
            if ((bool)newProduct?.isDeleted!)
                ds.ListOrder.RemoveAll(p => product.ID == p?.ID);
            else
                throw new AlreadyExistsException(product.ID);
        ds.ListProduct.Add(product);
        return product.ID;
    }


    public Product GetById(int id)
     //מקבל ת"ז ומחזיר את המוצר שזה הת"ז שלו
    {
        Product product = ds.ListProduct.FirstOrDefault(p => p?.ID == id)
            ?? throw new DoesNotExistException(id);
        if (product.isDeleted) throw new DoesNotExistException(id);//checks if the product is already in the store
        return product;
    }


    public void Update(Product product)
        //הפונקציה מעדכנת מוצר מסוים ברשימת הפרודוקטים, ומוצאת את הקודם ע"י הת"ז שנשארת אותו הדבר
    {
        Product newProduct = ds.ListProduct.FirstOrDefault(found => found?.ID == product.ID)
            ?? throw new DoesNotExistException(product.ID);
        if (newProduct.isDeleted)
            throw new DoesNotExistException(product.ID);
        ds.ListProduct.Remove(newProduct);
        ds.ListProduct.Add(product);
    }


    public void Delete(int id)
    //מוחק מוצר מהרשימה
   //הפונקציה לא באמת מוחקת את ההזמנה אלא רק מעדכנת בפרטים שלה שהיא מחוקה
    {
        Product found = ds.ListProduct.FirstOrDefault(item => item?.ID == id) 
            ?? throw new DoesNotExistException(id);

        if (found.isDeleted)
                    //בודק אם ההזמנה מחוקה כבר ברשימה, ואם כן זורק חריגה
            throw new DoesNotExistException(id);
   
        Product product = new() //בונה מוצר חדש עם אותם ערכים בדיוק, משנה רק את הערך של המחיקה
        {
            ID = id,
            Name = found.Name,
            Category = found.Category,
            InStock = found.InStock,
            Price = found.Price,
            isDeleted = true,
            Path = found.Path,
        };
        Update(product); //מעדכן ברשימה את ההזמנה המחוקה
    }

     public IEnumerable<Product?> GetAll()
        //מחזירה את כל הרשימה של המוצרים בהעתקה עמוקה, אי אפשר לשנות דרכה את הרשימה
     {
        return ds.ListProduct;
     }

    public IEnumerable<Product?> GetAll(Func<Product?, bool>? filter = null)
    {
        return (from Product? product in ds.ListProduct where filter(product) select product).ToList();
    }
}

