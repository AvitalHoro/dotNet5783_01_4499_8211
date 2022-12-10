using BLApi;
using Dal;
using System.Security.Cryptography;

namespace BlImplementation;

internal class Cart : ICart
{
    private DalApi.IDal Dal = DalApi.DalFactory.GetDal() ?? throw new NullReferenceException("Missing Dal");

    public BO.Cart AddProduct(BO.Cart cart, int idProduct)
    {
        try
        {
            DO.Product product = Dal.Product.GetById(idProduct);//מוצא את המוצר אותו רצינו להכניס לעגלה. אם המזהה לא קיים הפונקציה תזרוק חריגה

            BO.OrderItem item =
                cart.orderItems?.FirstOrDefault(oi => oi?.ProductID == idProduct)
                ?? new()
                {
                    ID = 0,
                    ProductID = idProduct,
                    NameProduct = product.Name,
                    Price = product.Price,
                    TotalPrice = 0,
                    Amount = 0,
                    IsDeleted = false,
                };
            if (item.Amount >= product.InStock) // not enough in stock
                throw new BO.OutOfStockException(idProduct);

            if (item.Amount == 0) cart.orderItems?.Add(item); // it is a new item

            item.Amount++;
            item.TotalPrice += item.Price;
            cart.TotalPrice += item.Price;
        }
        catch (DO.DoesNotExistException ex) { throw new BO.DoesNotExistException(ex.ID); }
        catch (BO.OutOfStockException ex) { throw new BO.OutOfStockException(ex.ID); }

        return cart;
    }


    public BO.Cart UpdateAmountProduct(BO.Cart cart, int idProduct, int amount)
    {
        try
        {
            DO.Product product = Dal.Product.GetById(idProduct);
            if (product.InStock < amount)
                throw new BO.OutOfStockException(idProduct);
            BO.OrderItem item =
                cart.orderItems?.FirstOrDefault(oi => oi?.ProductID == idProduct)
                ?? throw new BO.ProductNotExistInCartException(idProduct);
            if (amount == 0)
            {
                cart.orderItems.Remove(item);//אם הכמות היא אפס מסיר את המוצר מהרשימה
                cart.TotalPrice -= item.TotalPrice;
            }
            else
            {
                cart.orderItems.Remove(item);
                cart.TotalPrice -= item.TotalPrice;
                item.Amount = amount;
                item.TotalPrice += item.Price * amount;
                cart.orderItems.Add(item);
                cart.TotalPrice += item.TotalPrice;
            }

        }
        catch (BO.OutOfStockException ex) { throw new BO.OutOfStockException(ex.ID); }
        catch (BO.InvalidIDException ex) { throw new BO.InvalidIDException(ex.ID); }
        return cart;
    }

    public BO.OrderItem ValidationChecks(BO.OrderItem item)
    {
        DO.Product product = Dal.Product.GetById(item.ProductID);
        if (item.Amount < 1)
            throw new BO.AmountException();
        if (product.InStock < item.Amount)
            throw new BO.OutOfStockException(product.ID);
        return item;
    }

    //public BO.OrderItem? fu(BO.OrderItem? item, int newOrderId)
    //{
    //    DO.Product product = Dal.Product.GetById(item!.ProductID);
    //    DO.OrderItem orderItem = new()
    //    {
    //        //ID = item.ID,
    //        OrderID = newOrderId,
    //        ProductID = product.ID,
    //        Amount = item.Amount,
    //        Price = (item.Price / item.Amount),//מחיר לפריט בודד?
    //        IsDeleted = false,
    //    };
    //    Dal.OrderItem.Add(orderItem);
    //    DO.Product newProduct = new()//כדי לעדכן כמות במוצר שהוזמן, יוצרים אובייקט מוצר חדש עם אותם הערכים, רק בשינוי הכמות.
    //    {
    //        ID = product.ID,
    //        Name = product.Name,
    //        Price = product.Price,
    //        Category = product.Category,
    //        InStock = (product.InStock - item.Amount),
    //        IsDeleted = product.IsDeleted,
    //    };
    //    Dal.Product.Update(newProduct);//מעדכנים את הכמות של המוצר ברשימה
    //    return item;
    //}

    public int MakeOrder(BO.Cart cart)
    {
        try
        {
            if (cart.CostumerName == null)
                throw new BO.NoCostumerNameException();
            if (cart.CostumerEmail == null || !(cart.CostumerEmail.Contains('@')))
                throw new BO.NoCostumerEmailException();
            if (cart.CostumerAdress == null)
                throw new BO.NoCostumerAdressException();
            cart.orderItems = (from BO.OrderItem item in cart.orderItems
                               select ValidationChecks(item))
                               .ToList();
            DO.Order order = new DO.Order
            {
                CostumerAdress = cart.CostumerAdress,
                CostumerEmail = cart.CostumerEmail,
                CostumerName = cart.CostumerName,
                OrderDate = DateTime.Now,
                //DeliveryDate = null,
                //ShipDate = null,
                //isDeleted = false
            };


            //var newList= from BO.OrderItem? item in cart.orderItems
            //select fu(item, newOrderId);

            int newOrderId = Dal.Order.Add(order);
            foreach (BO.OrderItem? item in cart.orderItems)
            //תבנה אובייקטים של פריט בהזמנה (ישות נתונים) על פי הנתונים מהסל ומספר ההזמה הנ"ל ותבצע ניסיונות בקשת הוספת פריט הזמנה
            {
                DO.Product product = Dal.Product.GetById(item.ProductID);
                DO.OrderItem orderItem = new()
                {
                    //ID = item.ID,
                    OrderID = newOrderId,
                    ProductID = product.ID,
                    Amount = item.Amount,
                    Price = (item.Price / item.Amount),//מחיר לפריט בודד?
                    IsDeleted = false,
                };
                Dal.OrderItem.Add(orderItem);
                DO.Product newProduct = new()//כדי לעדכן כמות במוצר שהוזמן, יוצרים אובייקט מוצר חדש עם אותם הערכים, רק בשינוי הכמות.
                {
                    ID = product.ID,
                    Name = product.Name,
                    Price = product.Price,
                    Category = product.Category,
                    InStock = (product.InStock - item.Amount),
                    IsDeleted = product.IsDeleted,
                };
                Dal.Product.Update(newProduct);//מעדכנים את הכמות של המוצר ברשימה
            }
            return newOrderId;
        }
        catch (BO.NoCostumerNameException ex) { throw new BO.NoCostumerNameException(); }
        catch (BO.NoCostumerEmailException ex) { throw new BO.NoCostumerEmailException(); }
        catch (BO.NoCostumerAdressException ex) { throw new BO.NoCostumerAdressException(); }
        catch (BO.DoesNotExistException ex) { throw new DO.DoesNotExistException(ex.ID); }
        catch (BO.AlreadyExistsException ex) { throw new DO.AlreadyExistsException(ex.ID); }
        catch (BO.AmountException ex) { throw new BO.AmountException(); }
        catch (BO.OutOfStockException ex) { throw new BO.OutOfStockException(ex.ID); }
    }
}
