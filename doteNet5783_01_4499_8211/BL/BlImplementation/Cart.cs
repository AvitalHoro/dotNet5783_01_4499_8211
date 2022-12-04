﻿using BLApi;
using BO;
using Dal;

namespace BlImplementation;

internal class Cart : ICart
{
    private DalApi.IDal Dal = DalApi.DalFactory.GetDal() ?? throw new NullReferenceException("Missing Dal");

    public BO.Cart? AddProduct(BO.Cart? cart, int idProduct)
    {
        try
        {
            cart.orderItems = new List<BO.OrderItem?> { };
            DO.Product? product = Dal.Product.GetById(idProduct);
            BO.OrderItem? item = null;
            if (cart.orderItems!=null)
                item = cart.orderItems.Find(oi => oi.ProductID == idProduct);//למה לא עובד פה גט ווליו אור דיפולט?
            if (item == null)
            {
                if (product.GetValueOrDefault().InStock < 1)
                    throw new BO.OutOfStockException(idProduct);
                cart.orderItems.Add(new BO.OrderItem
                {
                    ID = 1234,//מה לשים פה????
                    ProductID = idProduct,
                    NameProduct = product.GetValueOrDefault().Name,
                    Price = product.GetValueOrDefault().Price,
                    TotalPrice = product.GetValueOrDefault().Price,
                    Amount = 1,
                    isDeleted = false,
                });
                cart.TotalPrice += product.GetValueOrDefault().Price;
            }
            else
            {
                if (product.GetValueOrDefault().InStock < item.Amount + 1)
                    throw new BO.OutOfStockException(idProduct);
                cart.orderItems.Remove(item);
                cart.TotalPrice -= item.TotalPrice;
                item.Amount++;
                item.TotalPrice += item.Price;
                cart.orderItems.Add(item);
                cart.TotalPrice += item.TotalPrice;
            }

        }
        catch (DO.DontExistException ex) { throw new BO.DontExistException(ex.ID); }
        catch (BO.OutOfStockException ex) { Console.WriteLine(ex); }
        catch (BO.InvalidIDException ex) { Console.WriteLine(ex); }
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


    public int MakeOrder(BO.Cart cart)
    {
        try
        {
            if (cart.CostumerName == null)
                throw new BO.NoCostumerNameException();
            if (cart.CostumerEmail == null || cart.CostumerEmail.Contains('@'))
                throw new BO.NoCostumerEmailException();
            if (cart.CostumerAdress == null)
                throw new BO.NoCostumerAdressException();
            foreach (OrderItem item in cart.orderItems)
            {
                DO.Product? product = Dal.Product.GetById(item.ProductID);
                if (item.Amount < 1)
                    throw new BO.AmountException();
                if (product.GetValueOrDefault().InStock < item.Amount)
                    throw new BO.OutOfStockException(product.GetValueOrDefault().ID);
            }
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
                DO.OrderItem? orderItem = new DO.OrderItem
                {
                    ID = item.ID,
                    OrderID = order.ID,
                    ProductID = product.GetValueOrDefault().ID,
                    Amount = item.Amount,
                    Price = item.Price / item.Amount,//מחיר לפריט בודד?
                    isDeleted = false,
                };
                Dal.OrderItem.Add(orderItem);
                DO.Product? newProduct = new DO.Product
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
        catch (BO.DontExistException ex) { throw new DO.DontExistException(ex.ID); }
        catch (BO.AlreadyExistsException ex) { throw new DO.AlreadyExistsException(ex.ID); }
        catch (BO.AmountException ex) { throw new BO.AmountException(); }
        catch (BO.OutOfStockException ex) { throw new BO.OutOfStockException(ex.ID); }
    }
}
