using BLApi;
using BO;

namespace BlImplementation;

internal class Cart : ICart
{
    private DalApi.IDal Dal = DalApi.DalFactory.GetDal() ?? throw new NullReferenceException("Missing Dal");;

    public BO.Cart? AddProduct(BO.Cart? cart, int idProduct)
    {
        DO.Product? product = Dal.Product.GetById(idProduct);
        try
        {
            if (product == null)
                throw new BO.InvalidIDException(idProduct);
            BO.OrderItem? item = cart.orderItems.Find(oi => oi.ProductID == idProduct);//למה לא עובד פה גט ווליו אור דיפולט?

            if (item == null)
            {
                if (product.GetValueOrDefault().InStock < 1)
                    throw new BO.OutOfStockException(idProduct);
                cart.orderItems.Add(new BO.OrderItem
                {
                    ID = 2345,//מה לשים פה????
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


    public int MakeOrder(BO.Cart cart, string costumerName, string costumerEmail, string costumerAdress)
    {
        try
        {
            if (cart.CostumerName == null)
                throw;
            if (cart.CostumerEmail == null || cart.CostumerEmail.Contains('@'))
                throw;
            if (cart.CostumerAdress == null)
                throw;
            foreach(OrderItem item in cart.orderItems )
            if(cart.)

        }
        catch () { }
    }

}
