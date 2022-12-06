using BLApi;
using BO;
using Dal;

namespace BlImplementation;

internal class Cart : ICart
{
    private DalApi.IDal Dal = DalApi.DalFactory.GetDal() ?? throw new NullReferenceException("Missing Dal");

    public BO.Cart AddProduct(BO.Cart cart, int idProduct)
    {
        try
        {
            DO.Product product = Dal.Product.GetById(idProduct);
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
                    isDeleted = false,
                };
            if (item.Amount >= product.InStock) // not enough in stock
                throw new BO.OutOfStockException(idProduct);

            if (item.Amount == 0) cart.orderItems?.Add(item); // it is a new item

            item.Amount++;
            item.TotalPrice += item.Price;
            cart.TotalPrice += item.TotalPrice;
        }
        catch (DO.DoesNotExistException ex) { throw new BO.DoesNotExistException(ex.ID); }

        return cart;
    }


    public BO.Cart? UpdateAmountProduct(BO.Cart cart, int idProduct, int amount)
    {
        DO.Product? product = Dal.Product.GetById(idProduct);
        try
        {
            if (product == null)
                throw new BO.InvalidIDException(idProduct);
            else if (product.GetValueOrDefault().InStock < 1)
                throw new BO.OutOfStockException(idProduct);
            BO.OrderItem? item = cart.orderItems.Find(oi => oi.ProductID == idProduct);//למה לא עובד פה גט ווליו אור דיפולט?
            if (item == null)
                throw new BO.ProductNotExistInCartException(idProduct);
            else if (amount == 0)
            {
                cart.orderItems.Remove(item);
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
        catch (BO.OutOfStockException ex) { Console.WriteLine(ex); }
        catch (BO.InvalidIDException ex) { Console.WriteLine(ex); }
        return cart;
    }

    public OrderItem fu(OrderItem item)
    {
        DO.Product? product = Dal.Product.GetById(item.ProductID);
        if (item.Amount < 1)
            throw new BO.AmountException();
        if (product.GetValueOrDefault().InStock < item.Amount)
            throw new BO.OutOfStockException(product.GetValueOrDefault().ID);
        return item;
    }

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
            cart.orderItems = (from OrderItem item in cart.orderItems select fu(item)).ToList();
            DO.Order order = new DO.Order
            {
                //ID = 1234,//אוף, שוב!
                CostumerAdress = cart.CostumerAdress,
                CostumerEmail = cart.CostumerEmail,
                CostumerName = cart.CostumerName,
                OrderDate = DateTime.Now,
                DeliveryDate = null,
                ShipDate = null,
                isDeleted = false
            };
            Dal.Order.Add(order);
            foreach (OrderItem item in cart.orderItems)
            //תבנה אובייקטים של פריט בהזמנה (ישות נתונים) על פי הנתונים מהסל ומספר ההזמה הנ"ל ותבצע ניסיונות בקשת הוספת פריט הזמנה
            {
                DO.Product? product = Dal.Product.GetById(item.ProductID);
                DO.OrderItem orderItem = new DO.OrderItem
                {
                    ID = item.ID,
                    OrderID = order.ID,
                    ProductID = product.GetValueOrDefault().ID,
                    Amount = item.Amount,
                    Price = item.Price / item.Amount,//מחיר לפריט בודד?
                    isDeleted = false,
                };
                Dal.OrderItem.Add(orderItem);
                DO.Product newProduct = new DO.Product
                {
                    ID = product.GetValueOrDefault().ID,
                    Name = product.GetValueOrDefault().Name,
                    Price = product.GetValueOrDefault().Price,
                    Category = product.GetValueOrDefault().Category,
                    InStock = product.GetValueOrDefault().InStock - item.Amount,
                    isDeleted = product.GetValueOrDefault().isDeleted,
                };
                Dal.Product.Update(newProduct);
            }
            return order.ID;
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
