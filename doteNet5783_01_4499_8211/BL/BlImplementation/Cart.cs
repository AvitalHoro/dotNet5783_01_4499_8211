using BLApi;
using Dal;
using System.Security.Cryptography;

namespace BlImplementation;

internal class Cart : ICart
{
    private readonly DalApi.IDal Dal = DalApi.DalFactory.GetDal() ?? throw new NullReferenceException("Missing Dal");

    #region AddProduct
    /// <exception cref="BO.DoesNotExistException"></exception>
    /// <exception cref="BO.OutOfStockException"></exception>
    public BO.Cart AddProduct(BO.Cart cart, int idProduct)//להוסיף מוצר לעגלה ע"י המזהה שלו
    {
        try
        {
            DO.Product product = Dal.Product.GetById(idProduct);//מוצא את המוצר אותו רצינו להכניס לעגלה. אם המזהה לא קיים הפונקציה תזרוק חריגה

            BO.OrderItem item =//מכין עצם מסוג פריט בהזמנה עם המוצר המבוקש
                cart.OrderItems?.FirstOrDefault(oi => oi?.ProductID == idProduct)
                ?? new()
                {
                    ID = (cart.OrderItems!.Count+1),//מכניס מזהה מוצר זמני שמסמל את המספר של הפריט ברשימת הפריטים בהזמנה
                    ProductID = idProduct,
                    NameProduct = product.Name,
                    Price = product.Price,
                    TotalPrice = 0,
                    Amount = 0,
                    IsDeleted = false,
                    Path= product.Path,
                };
            if (item.Amount >= product.InStock) // not enough in stock
                throw new BO.OutOfStockException(idProduct);

            if (item.Amount == 0) cart.OrderItems?.Add(item); // it is a new item

            item.Amount++;//מעדכן כמות
            item.TotalPrice += item.Price;//מעדכן מחיר בפריט 
            cart.TotalPrice += item.Price;//מעדכן מחיר בעגלה
        }
        catch (DO.DoesNotExistException ex) { throw new BO.DoesNotExistException(ex.ID); }
        catch (BO.OutOfStockException ex) { throw new BO.OutOfStockException(ex.ID); }

        return cart;
    }
    #endregion

    #region UpdateAmountProduct
    /// <exception cref="BO.ProductNotExistInCartException"></exception>
    /// <exception cref="BO.AmountException"></exception>
    /// <exception cref="BO.OutOfStockException"></exception>
    /// <exception cref="BO.InvalidIDException"></exception>
    public BO.Cart UpdateAmountProduct(BO.Cart cart, int idProduct, int amount)//מעדכן את הכמות של המוצר בעגלה (מוסיף, מוריד או מאפס
    {
        try
        {
            DO.Product product = Dal.Product.GetById(idProduct);//מוצא את המוצר המבוקש לפי המזהה שהתקבל
            if (product.InStock < amount)//בודק, אם הכמות של המוצר במלאי קטנה מהכמות המבוקשת, זורק חריגה
                throw new BO.OutOfStockException(idProduct);
            BO.OrderItem item =//מעביר נתונים עכשווים של המוצר המבוקש הקיים בעגלה, ואם המוצר לא קיים בעגלה, זורק חריגה
                cart.OrderItems?.FirstOrDefault(oi => oi?.ProductID == idProduct)
                ?? throw new BO.ProductNotExistInCartException(idProduct);
            if (amount == 0)
            {
                cart.OrderItems.Remove(item);//אם הכמות המבוקשת היא אפס מסיר את המוצר מהרשימה
                cart.TotalPrice -= item.TotalPrice;//ומעדכן את המחיר של העגלה
            }
            else if (amount > 0)
            {
                cart.OrderItems.Remove(item);//מסיר את המוצר מהרשימה
                cart.TotalPrice -= item.TotalPrice;//מוריד את מחירו ממחיר העגלה
                item.Amount = amount;//מעדכן את הכמות של המוצר
                item.TotalPrice = item.Price * amount;//מעדכן את המחיר הסופי של המוצר
                cart.OrderItems.Add(item);//מחזיר את המוצר לעגלה
                cart.TotalPrice += item.TotalPrice;//מעדכן את מחיר העגלה
            }
            else
                throw new BO.AmountException();//אם הכמות שנשלחה לפונקציה שלילית, זורק חריגה

        }
        catch (BO.OutOfStockException ex) { throw new BO.OutOfStockException(ex.ID); }
        catch (BO.InvalidIDException ex) { throw new BO.InvalidIDException(ex.ID); }
        return cart;
    }
    #endregion

    #region ValidationChecks 
    /// <exception cref="BO.AmountException"></exception>
    /// <exception cref="BO.OutOfStockException"></exception>
    public BO.OrderItem ValidationChecks(BO.OrderItem item)//בודק את תקינות הפריטים בעגלה
    {
        DO.Product product = Dal.Product.GetById(item.ProductID);
        if (item.Amount < 1)//תקינות הכמות בעגלה
            throw new BO.AmountException();
        if (product.InStock < item.Amount)//תקינות הכמות בהתאם למלאי
            throw new BO.OutOfStockException(product.ID);
        return item;
    }
    #endregion

    #region EnterOrderItemsToList
    public BO.OrderItem? EnterOrderItemsToList(BO.OrderItem? item, int newOrderId)//לכל פריט בסל - מעדכן את רשימת הפריטים בשכבת הנתונים, ומעדכן את הכמות במוצר
    {
        DO.Product product = Dal.Product.GetById(item!.ProductID);
        DO.OrderItem orderItem = new()
        {
            //ID = item.ID,
            OrderID = newOrderId,
            ProductID = product.ID,
            Amount = item.Amount,
            Price = (item.Price / item.Amount),//מחיר לפריט בודד
            Path = item.Path,   
            IsDeleted = false,
        };
        Dal.OrderItem.Add(orderItem);//מוסיף לשכבת הנתונים את כל פריטי ההזמנה של ההזמנה החדשה
        DO.Product newProduct = new()//כדי לעדכן כמות במוצר שהוזמן, יוצרים אובייקט מוצר חדש עם אותם הערכים, רק בשינוי הכמות.
        {
            ID = product.ID,
            Name = product.Name,
            Price = product.Price,
            Category = product.Category,
            InStock = (product.InStock - item.Amount),
            Path = product.Path,    
            IsDeleted = product.IsDeleted,
        };
        Dal.Product.Update(newProduct);//מעדכנים את הכמות של המוצר ברשימה
        return item;
    }
    #endregion

    #region MakeOrder
    /// <exception cref="BO.EmptyCartException"></exception>
    /// <exception cref="BO.NoCostumerNameException"></exception>
    /// <exception cref="BO.NoCostumerEmailException"></exception>
    /// <exception cref="BO.NoCostumerAdressException"></exception>
    /// <exception cref="DO.DoesNotExistException"></exception>
    /// <exception cref="DO.AlreadyExistsException"></exception>
    /// <exception cref="BO.AmountException"></exception>
    /// <exception cref="BO.OutOfStockException"></exception>
    public int MakeOrder(BO.Cart cart)//ביצוע הזמנה-העברת הסל למצב הזמנה
    {
        try
        {
            //בודק תקינות של הנתונים בסל
            if (cart.OrderItems?.Count == 0)//אם אין מוצרים בסל לא ניתן לאשר את ההזמנה
                throw new BO.EmptyCartException();
            if (cart.CostumerName == null)//אם לא הוכנס שם ללקוח זורק חריגה
                throw new BO.NoCostumerNameException();
            if (cart.CostumerEmail == null || !(cart.CostumerEmail.Contains('@')))//אם לא הוכנס מייל/כתובת לא תקינה
                throw new BO.NoCostumerEmailException();
            if (cart.CostumerAdress == null)//אם לא הוכנסה כתובת
                throw new BO.NoCostumerAdressException();

            cart.OrderItems = (from BO.OrderItem item in cart.OrderItems!//עובר על הפריטים ומחזיר רק את התקינים
                               select ValidationChecks(item))
                               .ToList();

            DO.Order order = new ()
            {
                CostumerAdress = cart.CostumerAdress,
                CostumerEmail = cart.CostumerEmail,
                CostumerName = cart.CostumerName,
                OrderDate = DateTime.Now,
                DeliveryDate = null,
                ShipDate = null,
            };

            int newOrderId = Dal.Order.Add(order);//מוסיף את ההזמנה לרשימת ההזמנות בשכבת הנתונים

            ////תבנה אובייקטים של פריט בהזמנה (ישות נתונים) על פי הנתונים מהסל ומספר ההזמה הנ"ל ותבצע ניסיונות בקשת הוספת פריט הזמנה
            var newList =(from BO.OrderItem? item in cart.OrderItems
                        let i = EnterOrderItemsToList(item, newOrderId)
                       select i).ToList();

            cart.TotalPrice=0;
            cart.OrderItems = new();
            return newOrderId;
        }
        catch (BO.NoCostumerNameException ex) { throw new BO.NoCostumerNameException(ex.Message); }
        catch (BO.NoCostumerEmailException ex) { throw new BO.NoCostumerEmailException(ex.Message); }
        catch (BO.NoCostumerAdressException ex) { throw new BO.NoCostumerAdressException(ex.Message); }
        catch (DO.DoesNotExistException ex) { throw new BO.DoesNotExistException(ex.ID); }
        catch (DO.AlreadyExistsException ex) { throw new BO.AlreadyExistsException(ex.ID); }
        catch (BO.AmountException ex) { throw new BO.AmountException(ex.Message); }
        catch (BO.OutOfStockException ex) { throw new BO.OutOfStockException(ex.ID); }
    }
    #endregion
}
