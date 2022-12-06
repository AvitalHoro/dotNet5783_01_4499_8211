using BLApi;
using BO;
using Dal;
using DO;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;

namespace BlImplementation;

internal class Order : IOrder
{
    private DalApi.IDal Dal = DalApi.DalFactory.GetDal() ?? throw new NullReferenceException("Missing Dal");
    public delegate BO.OrderItem? func(DO.OrderItem? DoOrder, BO.OrderItem? BoOrder);

    public BO.OrderItem updateItemListForOrder(DO.OrderItem DoOrder, BO.OrderItem BoOrder, ref double total)
    {
        BO.Tools.CopyPropTo(DoOrder, BoOrder);
        try { BoOrder.NameProduct = (Dal.Product.GetById(BoOrder.ProductID)).Name; }
        catch (BO.DoesNotExistException ex) { throw new BO.DoesNotExistException(ex.ID, ex.Message, ex); }
        BoOrder.TotalPrice = DoOrder.Price * DoOrder.Amount;
        total += BoOrder.TotalPrice;
        return BoOrder;
    }

    public void orderToBoOrder(DO.Order? orderDo, BO.Order? orderBo)
    //ממירה הזמנה משכבת הנתונים להזמנה משכבת הלוגיקה
    {
        double total = 0;
        BO.Tools.CopyPropTo(orderDo, orderBo);
        IEnumerable<DO.OrderItem> list = Dal.OrderItem.GetAll(orderDo.GetValueOrDefault().ID); //מבקשים משכבת הנתונים רשימה של כל הפריטים בהזמנה 
        BO.OrderItem orderItemBo = new BO.OrderItem();
        var newList = (from DO.OrderItem item in list select updateItemListForOrder(item, orderItemBo, ref total)).ToList();
        orderBo.Items = newList; //מעדכנים את הרשימה של הפריטים שיש בהזמנה
        orderBo.TotalPrice = total; //המחיר הכללי של ההזמנה שווה לסך מחיר כל הפריטים
    }

    public BO.OrderForList doOrderToOrderForList(DO.Order DoOrder, BO.OrderForList? BoOrder /*, ref double total, ref int amount*/)
    {
        BO.Tools.CopyPropTo(DoOrder, BoOrder);
        var OrderItems = Dal.OrderItem.GetAll(DoOrder.ID);
        BoOrder.ItemsAmount = OrderItems.Sum(item => item.Amount);
        BoOrder.TotalPrice = OrderItems.Sum(item => item.Price * item.Amount);
        if (DoOrder.DeliveryDate != null)
            BoOrder.State = Status.delivered;
        else if (DoOrder.ShipDate != null)
            BoOrder.State = Status.sent;
        else
            BoOrder.State = Status.approved;
        return BoOrder;
    }

    public IEnumerable<BO.OrderForList?> getOrderList()
    {
        IEnumerable<DO.Order> tmp = Dal.Order.GetAll();
        BO.OrderForList boOrder = new BO.OrderForList();
        //double total = 0;
        //int amount = 0;
        return from DO.Order item in tmp select doOrderToOrderForList(item, boOrder /*, ref total, ref amount*/);
    }

    public BO.Order getDetailsOrder(int IdOrder)
    //מקבלת מזהה של הזמנה ומחזירה את ההזמנה שזה המזהה שלה
    {
        double total = 0;
        try //אם הת"ז שלילית, זורקים חריגה
        {
            if (IdOrder < 0)
                throw new BO.InvalidIDException(IdOrder);
        }
        catch (BO.InvalidIDException ex) { new BO.InvalidIDException(ex.ID); }
        DO.Order? orderDo = Dal.Order.GetById(IdOrder); //מבקשים משכבת הנתונים את ההזמנה הרצויה
        //עושים המרה מהזמנה מסוג שכבת הנתונים להזמנה מסוג שכבת הלוגיקה
        BO.Order? orderBo = new BO.Order();
        orderToBoOrder(orderDo, orderBo);
        return orderBo;
    }

    public BO.Order UpdateShipDate(int IdOrder)
    {
        try //אם הת"ז שלילית, זורקים חריגה
        {
            if (IdOrder < 0)
                throw new BO.InvalidIDException(IdOrder);
        }
        catch (BO.InvalidIDException ex) { throw new BO.InvalidIDException(ex.ID); }
        try
        {
            DO.Order orderDo = Dal.Order.GetById(IdOrder);
            if (orderDo.ShipDate == null)
            {
                Dal.Order.Update(new DO.Order
                {
                    ID = IdOrder,
                    CostumerName = orderDo.CostumerName,
                    CostumerEmail = orderDo.CostumerEmail,
                    CostumerAdress = orderDo.CostumerAdress,
                    OrderDate = orderDo.OrderDate,
                    ShipDate = DateTime.Now,
                    DeliveryDate = null,
                    IsDeleted = false
                });
                BO.Order orderBo = new();
                orderToBoOrder(orderDo, orderBo);
                orderBo.ShipDate = DateTime.Now;
                return orderBo;
            }
        }
        catch (BO.DoesNotExistException ex) { throw new BO.DoesNotExistException(ex.ID, ex.Message, ex); }
        return null;
    }
    public BO.Order UpdateDeliveryDate(int IdOrder)
    {
        try //אם הת"ז שלילית, זורקים חריגה
        {
            if (IdOrder < 0)
                throw new BO.InvalidIDException(IdOrder);
        }
        catch (BO.InvalidIDException ex) { throw new BO.InvalidIDException(ex.ID); }
        try
        {
            DO.Order orderDo = Dal.Order.GetById(IdOrder);
            if (orderDo.DeliveryDate == null && orderDo.ShipDate != null)
            {
                Dal.Order.Update(new DO.Order
                {
                    ID = IdOrder,
                    CostumerName = orderDo.CostumerName,
                    CostumerEmail = orderDo.CostumerEmail,
                    CostumerAdress = orderDo.CostumerAdress,
                    OrderDate = orderDo.OrderDate,
                    ShipDate = orderDo.ShipDate,
                    DeliveryDate = DateTime.Now,
                    IsDeleted = false
                });
                BO.Order? orderBo = new BO.Order();
                orderToBoOrder(orderDo, orderBo);
                orderBo.DeliveryDate = DateTime.Now;
                return orderBo;
            }
        }
        catch (BO.DoesNotExistException ex) { throw new BO.DoesNotExistException(ex.ID, ex.Message, ex); }
        return null;
    }
    public BO.OrderTracking Tracking(int IdOrder)
    {
        try //אם הת"ז שלילית, זורקים חריגה
        {
            if (IdOrder < 0)
                throw new BO.InvalidIDException(IdOrder);
        }
        catch (BO.InvalidIDException ex) { new BO.InvalidIDException(ex.ID); }
        try
        {
            DO.Order orderDo = Dal.Order.GetById(IdOrder);
            BO.OrderTracking orderTracking = new BO.OrderTracking();
            List<Tuple<DateTime?, string>> listTuples = new List<Tuple<DateTime?, string>> { };
            orderTracking.ID = orderDo.ID;
            orderTracking.State = BO.Status.approved;
            listTuples.Add(Tuple.Create(orderDo.OrderDate, "The order was approved"));
            if (orderDo.ShipDate != null)
            {
                orderTracking.State = BO.Status.sent;
                listTuples.Add(Tuple.Create(orderDo.ShipDate, "The order was sent"));
            }
            if (orderDo.DeliveryDate != null)
            {
                orderTracking.State = BO.Status.delivered;
                listTuples.Add(Tuple.Create(orderDo.DeliveryDate, "The order was delivered"));
            }
            orderTracking.Tracking = listTuples;
            return orderTracking;
        }
        catch (DO.DoesNotExistException ex) { throw new BO.DoesNotExistException(ex.ID); }
    }
    public DO.OrderItem UpdateOrder(int IdOrder, int IdProduct, int newAmount)
    //בונוס, בשביל המנהל
    {
        if (newAmount < 0)
            throw new BO.AmountException();
        DO.OrderItem item = Dal.OrderItem.getItem(IdOrder, IdProduct);
        Dal.OrderItem.Update(new DO.OrderItem
        {
            ID = item.ID,
            OrderID = IdOrder,
            ProductID = IdProduct,
            Price = item.Price,
            Amount = newAmount,
            IsDeleted = false
        });
        return Dal.OrderItem.getItem(IdOrder, IdProduct);
    }
}
