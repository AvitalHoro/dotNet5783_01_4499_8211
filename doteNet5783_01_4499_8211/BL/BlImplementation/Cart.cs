using BLApi;
using System.Security.Cryptography;

namespace BlImplementation;

internal class Cart : ICart
{
    private readonly DalApi.IDal Dal = DalApi.DalFactory.GetDal() ?? throw new NullReferenceException("Missing Dal");

    #region AddProduct
    /// <exception cref="BO.DoesNotExistException"></exception>
    /// <exception cref="BO.OutOfStockException"></exception>
    public BO.Cart AddProduct(BO.Cart cart, int idProduct) // Add a product to the cart by its ID
    {
        try
        {
            DO.Product product = Dal.Product.GetById(idProduct); // Find the product we want to add to the cart. If the ID doesn't exist, the function will throw an exception

            BO.OrderItem item = // Prepare an OrderItem object with the requested product
                cart.OrderItems?.FirstOrDefault(oi => oi?.ProductID == idProduct)
                ?? new()
                {
                    ID = (cart.OrderItems!.Count+1), // Assign a temporary product ID representing the item's number in the list of order items
                    ProductID = idProduct,
                    NameProduct = product.Name,
                    Price = product.Price,
                    TotalPrice = 0,
                    Amount = 0,
                    IsDeleted = false,
                    Path= product.Path,
                };
            if (item.Amount >= product.InStock) // Not enough in stock
                throw new BO.OutOfStockException(idProduct);

            if (item.Amount == 0) cart.OrderItems?.Add(item); // It is a new item

            item.Amount++; // Update quantity
            item.TotalPrice += item.Price; // Update item price
            cart.TotalPrice += item.Price; // Update cart price
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
    public BO.Cart UpdateAmountProduct(BO.Cart cart, int idProduct, int amount) // Update the quantity of a product in the cart (add, remove, or reset)
    {
        try
        {
            DO.Product product = Dal.Product.GetById(idProduct); // Find the requested product by the received ID
            if (product.InStock < amount) // Check if the product's stock is less than the requested amount, and if so, throw an exception
                throw new BO.OutOfStockException(idProduct);
            BO.OrderItem item = // Get the current data of the requested product in the cart, and if the product is not in the cart, throw an exception
                cart.OrderItems?.FirstOrDefault(oi => oi?.ProductID == idProduct)
                ?? throw new BO.ProductNotExistInCartException(idProduct);
            if (amount == 0)
            {
                cart.OrderItems.Remove(item); // If the requested amount is zero, remove the product from the list
                cart.TotalPrice -= item.TotalPrice; // And update the cart price
            }
            else if (amount > 0)
            {
                cart.OrderItems.Remove(item); // Remove the product from the list
                cart.TotalPrice -= item.TotalPrice; // Subtract its price from the cart price
                item.Amount = amount; // Update the product quantity
                item.TotalPrice = item.Price * amount; // Update the product's total price
                cart.OrderItems.Add(item); // Add the product back to the cart
                cart.TotalPrice += item.TotalPrice; // Update the cart price
            }
            else
                throw new BO.AmountException(); // If the amount sent to the function is negative, throw an exception

        }
        catch (BO.OutOfStockException ex) { throw new BO.OutOfStockException(ex.ID); }
        catch (BO.InvalidIDException ex) { throw new BO.InvalidIDException(ex.ID); }
        return cart;
    }
    #endregion

    #region ValidationChecks 
    /// <exception cref="BO.AmountException"></exception>
    /// <exception cref="BO.OutOfStockException"></exception>
    public BO.OrderItem ValidationChecks(BO.OrderItem item) // Check the validity of the items in the cart
    {
        DO.Product product = Dal.Product.GetById(item.ProductID);
        if (item.Amount < 1) // Check the validity of the quantity in the cart
            throw new BO.AmountException();
        if (product.InStock < item.Amount) // Check the validity of the quantity based on the stock
            throw new BO.OutOfStockException(product.ID);
        return item;
    }
    #endregion

    #region EnterOrderItemsToList
    public BO.OrderItem? EnterOrderItemsToList(BO.OrderItem? item, int newOrderId) // For each item in the cart, update the list of items in the data layer and update the quantity of the product
    {
        DO.Product product = Dal.Product.GetById(item!.ProductID);
        DO.OrderItem orderItem = new()
        {
            //ID = item.ID,
            OrderID = newOrderId,
            ProductID = product.ID,
            Amount = item.Amount,
            Price = item.Price, // Price for a single item
            Path = item.Path,   
        };
        Dal.OrderItem.Add(orderItem); // Add all order items of the new order to the data layer

        DO.Product newProduct = new() // To update the quantity of the ordered product, create a new Product object with the same values, but with the updated quantity.
        {
            ID = product.ID,
            Name = product.Name,
            Price = product.Price,
            Category = product.Category,
            InStock = (product.InStock - item.Amount),
            Path = product.Path,    
            IsDeleted = product.IsDeleted,
        };
        Dal.Product.Update(newProduct); // Update the product quantity in the list
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
    public int MakeOrder(BO.Cart cart) // Place an order - transfer the cart to order status
    {
        try
        {
            // Check the validity of the data in the cart
            if (cart.OrderItems?.Count == 0) // If there are no products in the cart, it is not possible to confirm the order
                throw new BO.EmptyCartException();
            if (cart.CostumerName == null) // If no customer name is provided, throw an exception
                throw new BO.NoCostumerNameException();
            if (cart.CostumerEmail == null || !(cart.CostumerEmail.Contains('@'))) // If no email/invalid address is provided
                throw new BO.NoCostumerEmailException();
            if (cart.CostumerAdress == null) // If no address is provided
                throw new BO.NoCostumerAdressException();

            cart.OrderItems = (from BO.OrderItem item in cart.OrderItems! // Iterate over the items and return only the valid ones
                               select ValidationChecks(item))
                               .ToList();

            DO.Order order = new ()
            {
                TotalPrice = cart.TotalPrice, 
                CostumerAdress = cart.CostumerAdress,
                CostumerEmail = cart.CostumerEmail,
                CostumerName = cart.CostumerName,
                OrderDate = DateTime.Now,
                DeliveryDate = null,
                ShipDate = null,
            };

            int newOrderId = Dal.Order.Add(order); // Add the order to the list of orders in the data layer

            // Create OrderItem objects (data entity) based on the data from the cart and the above order number, and attempt to add order items
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
