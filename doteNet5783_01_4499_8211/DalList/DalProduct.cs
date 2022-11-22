using DalApi;
using DO;

namespace Dal;

public class DalProduct: IProduct
//realizes all the methods of the products
{
    DataSource ds = DataSource.s_instance; //כדי שלא ניצור כל פעם עצם חדש של נתונים

    public int Add(Product? product)
     //פונקציה להוספת מוצר חדש לרשימת המוצרים
    {
        //if(ds.ListProduct.Find(p => product.GetValueOrDefault().ID == p.GetValueOrDefault().ID)!=null) //checks if the product is already in the store
        //    throw new Exception "The product ia alredey in the store";
        ds.ListProduct.Add(product);
        return product.GetValueOrDefault().ID;
    }
    public Product? GetById(int id)
     //מקבל ת"ז ומחזיר את המוצר שזה הת"ז שלו
    {
        if (ds.ListProduct.Find(product => product.GetValueOrDefault().ID == id)==null) //checks if the product is already in the store
            throw new DontExistException(id);
        return (ds.ListProduct.Find(product => product.GetValueOrDefault().ID == id));
    }
    public void Update(Product? product)
        //הפונקציה מעדכנת מוצר מסוים ברשימת הפרודוקטים, ומוצאת את הקודם ע"י הת"ז שנשארת אותו הדבר
    {
        Product? temp = ds.ListProduct.Find(found => found.GetValueOrDefault().ID == product.GetValueOrDefault().ID);
        if (temp==null)
            throw new DontExistException(product.GetValueOrDefault().ID);
        int i= ds.ListProduct.IndexOf(temp);
        ds.ListProduct[i]=product;
    }
    public void Delete(int id)
    //מוחק מוצר מהרשימה
   //הפונקציה לא באמת מוחקת את ההזמנה אלא רק מכדכנת בפרטים שלה שהיא מחוקה
    {
        Product? found = ds.ListProduct.Find(item => item.GetValueOrDefault().ID == id);
        if (found == null)
           //בודק אם ההזמנה לא נמצאת ברשימה, ואם לא נמצאת זורק חריגה
         throw new DontExistException(id);
   
        Product product = new Product //בונה מוצר חדש עם אותם ערכים בדיוק, משנה רק את הערך של המחיקה
        {
            ID = id,
            Name = found.GetValueOrDefault().Name,
            Category = found.GetValueOrDefault().Category,
            InStock = found.GetValueOrDefault().InStock,
            Price = found.GetValueOrDefault().Price,
            isDeleted = true
        };
        Update(product); //מעדכן ברשימה את ההזמנה המחוקה
    }

        //IEnumerable<T?> GetAll(Func<T?, bool>? filter = null);
     public IEnumerable<Product?> GetAll()
        //מחזירה את כל הרשימה של המוצרים בהעתקה עמוקה, אי אפשר לשנות דרכה את הרשימה
     {
        List<Product?> newListProduct = new List<Product?> { };
        foreach (Product product in ds.ListProduct) { newListProduct.Add(product); };
        return newListProduct;
     }
}

