using DalApi;
using DO;
using System.Security.Cryptography;

namespace Dal;

public class DalProduct: IProduct
//realizes all the methods of the products
{
    DataSource ds = DataSource.s_instance; //כדי שלא ניצור כל פעם עצם חדש של נתונים

    public int Add(Product? product)
     //פונקציה להוספת מוצר חדש לרשימת המוצרים
    {
        Product? pro = ds.ListProduct.Find(p => product.GetValueOrDefault().ID == p.GetValueOrDefault().ID);
        if(pro != null && !pro.GetValueOrDefault().isDeleted) //checks if the product is already in the store
            throw new AlreadyExistsException(product.GetValueOrDefault().ID);
        ds.ListProduct.Add(product);
        return product.GetValueOrDefault().ID;
    }
    public Product? GetById(int id)
     //מקבל ת"ז ומחזיר את המוצר שזה הת"ז שלו
    {
        Product? product = ds.ListProduct.Find(pro => pro.GetValueOrDefault().ID == id);
        if (product==null ||product.GetValueOrDefault().isDeleted) //checks if the product is already in the store
            throw new DontExistException(id);
        return product;
    }
    public void Update(Product? product)
        //הפונקציה מעדכנת מוצר מסוים ברשימת הפרודוקטים, ומוצאת את הקודם ע"י הת"ז שנשארת אותו הדבר
    {
        Product? temp = ds.ListProduct.Find(found => found.GetValueOrDefault().ID == product.GetValueOrDefault().ID);
        if (temp==null|| temp.GetValueOrDefault().isDeleted)
            throw new DontExistException(product.GetValueOrDefault().ID);
       ds.ListProduct.Remove(temp);
        ds.ListProduct.Add(product);
    }
    public void Delete(int id)
    //מוחק מוצר מהרשימה
   //הפונקציה לא באמת מוחקת את ההזמנה אלא רק מכדכנת בפרטים שלה שהיא מחוקה
    {
        Product? found = ds.ListProduct.Find(item => item.GetValueOrDefault().ID == id);
        if (found == null|| found.GetValueOrDefault().isDeleted)
           //בודק אם ההזמנה לא נמצאת ברשימה, ואם לא נמצאת זורק חריגה
         throw new DontExistException(id);
   
        Product product = new Product //בונה מוצר חדש עם אותם ערכים בדיוק, משנה רק את הערך של המחיקה
        {
            ID = id,
            Name = found.GetValueOrDefault().Name,
            Category = found.GetValueOrDefault().Category,
            InStock = found.GetValueOrDefault().InStock,
            Price = found.GetValueOrDefault().Price,
            isDeleted = true,
            Path = found.GetValueOrDefault().Path,
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

