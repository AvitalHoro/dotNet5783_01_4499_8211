using BLApi;
using BO;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;

namespace BlImplementation;

internal class Order : BLApi.IOrder
    {
        private DalApi.IDal Dal = DalApi.DalFactory.GetDal() ?? throw new NullReferenceException("Missing Dal");

        #region UpdateItemListForOrder
        /// <summary>
        /// Function that receives an order item from the data layer and converts it to a business logic layer order item.
        /// </summary>
        /// <param name="doOrder">The order item from the data layer.</param>
        /// <returns>The converted order item in the business logic layer.</returns>
        /// <exception cref="BO.DoesNotExistException"></exception>
        public BO.OrderItem UpdateItemListForOrder(DO.OrderItem doOrder)
        {
            BO.OrderItem boOrder = new BO.OrderItem();
            BO.Tools.CopyPropTo(doOrder, boOrder);
            try 
            { 
                DO.Product product = Dal.Product.GetById(boOrder.ProductID);
                boOrder.NameProduct = product.Name;
                boOrder.Path = product.Path; 
            }
            catch (BO.DoesNotExistException ex) { throw new BO.DoesNotExistException(ex.ID, ex.Message, ex); }
            boOrder.TotalPrice = doOrder.Price * doOrder.Amount;
            return boOrder;
        }
        #endregion

        #region OrderToBoOrder
        /// <summary>
        /// Converts an order from the data layer to an order in the business logic layer.
        /// </summary>
        /// <param name="orderDo">The order from the data layer.</param>
        /// <param name="orderBo">The order in the business logic layer to populate.</param>
        public void OrderToBoOrder(DO.Order? orderDo, BO.Order? orderBo)
        {
            double total = 0;
            BO.Tools.CopyPropTo(orderDo, orderBo);
            IEnumerable<DO.OrderItem?> list = Dal.OrderItem.GetAll(item => orderDo?.ID == item?.OrderID);
            var newList = (from DO.OrderItem item in list
                           let orderItem = UpdateItemListForOrder(item)
                           select orderItem)
                           .ToList();
            orderBo.Items = newList;
            if (orderDo?.TotalPrice == 0)
                orderBo.TotalPrice = newList.Sum(item => item.TotalPrice);
            if (orderDo?.DeliveryDate != null)
                orderBo.State = BO.Status.Delivered;
            else if (orderDo?.ShipDate != null)
                orderBo.State = BO.Status.Sent;
            else
                orderBo.State = BO.Status.Approved;
        }
        #endregion

        #region DoOrderToOrderForList
        /// <summary>
        /// Converts an order from the data layer to an order for a list in the business logic layer.
        /// </summary>
        /// <param name="doOrder">The order from the data layer.</param>
        /// <returns>The converted order for a list in the business logic layer.</returns>
        public BO.OrderForList DoOrderToOrderForList(DO.Order doOrder)
        {
            BO.OrderForList boOrder = new BO.OrderForList();
            BO.Tools.CopyPropTo(doOrder, boOrder);
            var OrderItems = Dal.OrderItem.GetAll(item => doOrder.ID == item?.OrderID);
            boOrder.ItemsAmount = OrderItems.Sum(item => item?.Amount) ?? 0;
            if (doOrder.TotalPrice == 0)
                boOrder.TotalPrice = OrderItems.Sum(item => item?.Price * item?.Amount) ?? 0;
            else
                boOrder.TotalPrice = doOrder.TotalPrice;
            if (doOrder.DeliveryDate != null)
                boOrder.State = BO.Status.Delivered;
            else if (doOrder.ShipDate != null)
                boOrder.State = BO.Status.Sent;
            else
                boOrder.State = BO.Status.Approved;
            return boOrder;
        }
        #endregion

        #region GetOrderList
        /// <summary>
        /// Retrieves a list of all orders.
        /// </summary>
        /// <param name="state">Optional filter by order state.</param>
        /// <returns>List of orders.</returns>
        public IEnumerable<BO.OrderForList?> GetOrderList(Status? state = null)
        {
            if (state != null)
            {
                IEnumerable<DO.Order?> tmp =
                state switch
                {
                    Status.Approved =>
                        Dal.Order.GetAll(order => order?.ShipDate == null && order?.DeliveryDate == null && order?.IsDeleted == false).OrderBy(order => order?.ID),

                    Status.Sent =>
                        Dal.Order.GetAll(order => order?.ShipDate != null && order?.DeliveryDate == null && order?.IsDeleted == false).OrderBy(order => order?.ID),

                    Status.Delivered =>
                        Dal.Order.GetAll(order => order?.ShipDate != null && order?.DeliveryDate != null && order?.IsDeleted == false).OrderBy(order => order?.ID),
                };
                return (from DO.Order item in tmp
                        let orderForList = DoOrderToOrderForList(item)
                        select orderForList)
                        .OrderBy(order => order?.ID)
                        .ToList();
            }
            else
            {
                IEnumerable<DO.Order?> tmp = Dal.Order.GetAll(o => o?.IsDeleted == false);
                return (from DO.Order item in tmp
                        let orderForList = DoOrderToOrderForList(item)
                        select orderForList)
                        .OrderBy(order => order?.ID)
                        .ToList();
            }
        }
        #endregion

        #region GetDetailsOrder
        /// <summary>
        /// Retrieves an order details based on the given order ID.
        /// </summary>
        /// <param name="idOrder">The ID of the order to retrieve.</param>
        /// <returns>The order details.</returns>
        /// <exception cref="BO.InvalidIDException"></exception>
        public BO.Order GetDetailsOrder(int idOrder)
        {
            try 
            {
                if (idOrder < 0)
                    throw new BO.InvalidIDException(idOrder);
                DO.Order? orderDo = Dal.Order.GetById(idOrder);
                BO.Order? orderBo = new BO.Order();
                OrderToBoOrder(orderDo, orderBo);
                return orderBo;
            }
            catch (BO.InvalidIDException ex) { throw new BO.InvalidIDException(ex.ID); }
        }
        #endregion

        #region CancelOrder
        /// <summary>
        /// Cancels an order based on the given order ID.
        /// </summary>
        /// <param name="idOrder">The ID of the order to cancel.</param>
        public void CancelOrder(int idOrder)
        {
            try
            {
                var items = Dal.OrderItem.GetAll(item => item?.OrderID == idOrder);
                foreach (DO.OrderItem item in items) Dal.OrderItem.Delete(item.ID);
                Dal.Order.Delete(idOrder); 
            }
            catch (BO.InvalidIDException ex) { new BO.InvalidIDException(ex.ID); }
        }
        #endregion

        #region UpdateShipDate
        /// <summary>
        /// Updates the shipping date of an order and returns the updated order.
        /// </summary>
        /// <param name="idOrder">The ID of the order to update.</param>
        /// <param name="date">The new shipping date (default is current date).</param>
        /// <returns>The updated order.</returns>
        /// <exception cref="BO.InvalidIDException"></exception>
        /// <exception cref="BO.OrderAlreadyShippedExecption"></exception>
        /// <exception cref="BO.DoesNotExistException"></exception>
        public BO.Order UpdateShipDate(int idOrder, DateTime? date = null)
        {
            try
            {
                if (idOrder < 0)
                    throw new BO.InvalidIDException(idOrder);
            }
            catch (BO.InvalidIDException ex) { throw new BO.InvalidIDException(ex.ID); }
            if (date == null)
                date = DateTime.Now;
            try
            {
                DO.Order orderDo = Dal.Order.GetById(idOrder);
                if (orderDo.ShipDate == null)
                {
                    Dal.Order.Update(new DO.Order
                    {
                        ID = idOrder,
                        CostumerName = orderDo.CostumerName,
                        CostumerEmail = orderDo.CostumerEmail,
                        CostumerAdress = orderDo.CostumerAdress,
                        OrderDate = orderDo.OrderDate,
                        ShipDate = date,
                        DeliveryDate = null,
                        IsDeleted = false
                    });
                    orderDo = Dal.Order.GetById(idOrder);
                    BO.Order orderBo = new();
                    OrderToBoOrder(orderDo, orderBo);
                    orderBo.State = Status.Sent;
                    return orderBo;
                }
                else
                {
                    throw new BO.OrderAlreadyShippedExecption(idOrder);
                }
            }
            catch (BO.DoesNotExistException ex) { throw new BO.DoesNotExistException(ex.ID, ex.Message, ex); }
        }
        #endregion

    #region UpdateDeliveryDate
    /// <exception cref="BO.InvalidIDException"></exception>
    /// <exception cref="BO.OrderAlreadyDeliveredExecption"></exception>
    /// <exception cref="BO.DoesNotExistException"></exception>
    /// <summary>
    /// Updates the delivery date of an order and returns the updated order.
    /// </summary>
    /// <param name="idOrder">The ID of the order to update.</param>
    /// <param name="date">The new delivery date (default is current date if not specified).</param>
    /// <returns>The updated order.</returns>
    public BO.Order UpdateDeliveryDate(int idOrder, DateTime? date = null)
    {
        try // Throws exception if the ID is negative
        {
            if (idOrder < 0)
                throw new BO.InvalidIDException(idOrder);
        }
        catch (BO.InvalidIDException ex) { throw new BO.InvalidIDException(ex.ID); }

        // If date is not provided, set it to current date
        if (date == null)
            date = DateTime.Now;

        try
        {
            DO.Order orderDo = Dal.Order.GetById(idOrder);

            // Check if the order has been shipped but not delivered yet
            if (orderDo.DeliveryDate == null && orderDo.ShipDate != null)
            {
                // Update in data layer that the order is now delivered
                Dal.Order.Update(new DO.Order
                {
                    ID = idOrder,
                    CostumerName = orderDo.CostumerName,
                    CostumerEmail = orderDo.CostumerEmail,
                    CostumerAdress = orderDo.CostumerAdress,
                    OrderDate = orderDo.OrderDate,
                    ShipDate = orderDo.ShipDate,
                    DeliveryDate = date,
                    IsDeleted = false
                });

                // Create business object for the updated order
                BO.Order orderBo = new BO.Order();
                OrderToboOrder(orderDo, orderBo);
                orderBo.State = Status.delivered;
                return orderBo;
            }
            else
            {
                throw new BO.OrderAlreadyDeliveredExecption(idOrder);
            }
        }
        catch (BO.DoesNotExistException ex) { throw new BO.DoesNotExistException(ex.ID, ex.Message, ex); }
    }
    #endregion

    #region Tracking
    /// <exception cref="BO.DoesNotExistException"></exception>
    /// <summary>
    /// Retrieves the tracking details of an order, including events and their corresponding dates.
    /// </summary>
    /// <param name="idOrder">The ID of the order to track.</param>
    /// <returns>The order tracking details.</returns>
    public BO.OrderTracking Tracking(int idOrder)
    {
        try // Throws exception if the ID is negative
        {
            if (idOrder < 0)
                throw new BO.InvalidIDException(idOrder);
        }
        catch (BO.InvalidIDException ex) { new BO.InvalidIDException(ex.ID); }

        try
        {
            DO.Order orderDo = Dal.Order.GetById(idOrder);
            BO.OrderTracking orderTracking = new BO.OrderTracking();

            // Create an empty list of tuples to store events and their dates
            List<Tuple<DateTime?, string>> listTuples = new List<Tuple<DateTime?, string>>();

            orderTracking.ID = orderDo.ID;
            orderTracking.State = BO.Status.approved;
            listTuples.Add(Tuple.Create(orderDo.OrderDate, ":Approved"));

            // If there is a shipping date, add it to the list
            if (orderDo.ShipDate != null)
            {
                orderTracking.State = BO.Status.sent;
                listTuples.Add(Tuple.Create(orderDo.ShipDate, ":Shipped"));
            }

            // If there is a delivery date, add it to the list
            if (orderDo.DeliveryDate != null)
            {
                orderTracking.State = BO.Status.delivered;
                listTuples.Add(Tuple.Create(orderDo.DeliveryDate, ":Delivered"));
            }

            orderTracking.Tracking = listTuples;
            return orderTracking;
        }
        catch (DO.DoesNotExistException ex) { throw new BO.DoesNotExistException(ex.ID); }
    }
    #endregion

    #region UpdateOrder
    /// <exception cref="BO.AmountException"></exception>
    /// <summary>
    /// Updates the quantity of a product in an existing order. This function is accessible only to administrators.
    /// </summary>
    /// <param name="idOrder">The ID of the order to update.</param>
    /// <param name="IdProduct">The ID of the product to update.</param>
    /// <param name="newAmount">The new quantity of the product.</param>
    /// <returns>The updated order item.</returns>
    public DO.OrderItem UpdateOrder(int idOrder, int IdProduct, int newAmount)
    {
        // Throws exception if the new amount is negative
        if (newAmount < 0)
            throw new BO.AmountException();

        // Retrieve the order item from the data layer
        DO.OrderItem item = Dal.OrderItem.GetItem(idOrder, IdProduct);

        // Update the product quantity in the data layer
        Dal.OrderItem.Update(new DO.OrderItem
        {
            ID = item.ID,
            OrderID = idOrder,
            ProductID = IdProduct,
            Price = item.Price,
            Amount = newAmount,
        });

        // Return the updated order item
        return Dal.OrderItem.GetItem(idOrder, IdProduct);
    }
    #endregion

    }