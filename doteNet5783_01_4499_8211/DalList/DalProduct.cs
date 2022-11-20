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
        if(ds.ListProduct.Find(p => product.GetValueOrDefault().ID == p.GetValueOrDefault().ID)!=null) 
            //checks if the product is already in the store
           throw new Exception ("The product ia alredey in the store");
        ds.ListProduct.Add(product); //מוסיף לרשימה
        return product.GetValueOrDefault().ID; //מחזיר את הת"ז של המוצר החדש
    }
    public Product? GetById(int id)
     //מקבל ת"ז ומחזיר את המוצר שזה הת"ז שלו
    {
        return (ds.ListProduct.Find(product => product.GetValueOrDefault().ID == id));
    }
    public void Update(Product? product)
        //הפונקציה מעדכנת מוצר מסוים ברשימת הפרודוקטים, ומוצאת את הקודם ע"י הת"ז שנשארת אותו הדבר
    {
        Product? temp = ds.ListProduct.Find(found => found.GetValueOrDefault().ID == product.GetValueOrDefault().ID);
        //מכניס למשתנה הזמני את המוצר הישן שאנחנו רוצים לעדכן
        if (temp==null) //אם המוצר לא היה ברשימה
           return; 
       int i= ds.ListProduct.IndexOf(temp);  //מוצא את האינדקס ברשימה של האיבר הישן
        ds.ListProduct[i]=product; //הפונקציה מכניסה את המוצר המעודכן לאותו מקום בדיוק של הישן
    }
    public void Delete(int id)
        //מוחק מוצר מהרשימה
    {
        //צריך למחוק בכאילו, לא באמת, רק לשנות את האיז דיליטד
        ds.ListProduct.RemoveAll(product => product.GetValueOrDefault().ID == id);
        //foreach (Product? product in ds.ListProduct) { if(product.GetValueOrDefault().ID == id) product.isDeleted(true);}
    }

    //IEnumerable<T?> GetAll(Func<T?, bool>? filter = null);
    public IEnumerable<Product?> GetAll()
        //מחזירה את כל הרשימה של המוצרים בהעתקה רדודה, אי אפשר לשנות דרכה את הרשימה
    {
        List <Product?> temp= ds.ListProduct;
        return temp; 
    }
}

