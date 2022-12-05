using BLApi;
using BO;
using Dal;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;

namespace BlImplementation;

internal class Order : IOrder
{
    private DalApi.IDal Dal = DalApi.DalFactory.GetDal() ?? throw new NullReferenceException("Missing Dal");
    public delegate BO.OrderItem? func(DO.OrderItem? DoOrder, BO.OrderItem? BoOrder);

    public BO.OrderItem? updateItemListForOrder(DO.OrderItem? DoOrder, BO.OrderItem? BoOrder, ref double total)
    {
        BO.Tools.CopyPropTo(DoOrder, ref BoOrder);
        try { BoOrder.NameProduct = (Dal.Product.GetById(BoOrder.ProductID)).GetValueOrDefault().Name; }
        catch (BO.DontExistException ex) { throw new BO.DontExistException(ex.ID, ex.Message, ex); }
        BoOrder.TotalPrice = DoOrder.GetValueOrDefault().Price * DoOrder.GetValueOrDefault().Amount;
        total += BoOrder.TotalPrice;
        return BoOrder;
    }

    public void orderToBoOrder(DO.Order? orderDo, BO.Order? orderBo)
    //ממירה הזמנה משכבת הנתונים להזמנה משכבת הלוגיקה
    {
        double total = 0;
        BO.Tools.CopyPropTo(orderDo, ref orderBo);
        IEnumerable<DO.OrderItem?> list = Dal.OrderItem.GetAll(orderDo.GetValueOrDefault().ID); //מבקשים משכבת הנתונים רשימה של כל הפריטים בהזמנה 
        BO.OrderItem orderItemBo = new BO.OrderItem();
        var newList = (from DO.OrderItem item in list select updateItemListForOrder(item, orderItemBo, ref total)).ToList();
        orderBo.Items = newList; //מעדכנים את הרשימה של הפריטים שיש בהזמנה
        orderBo.TotalPrice = total; //המחיר הכללי של ההזמנה שווה לסך מחיר כל הפריטים
    }

    public BO.OrderForList? doOrderToOrderForList(DO.Order? DoOrder, BO.OrderForList? BoOrder, ref double total, ref int amount)
    {
        BO.Tools.CopyPropTo(DoOrder, ref BoOrder);
        var OrderItems = Dal.OrderItem.GetAll(DoOrder.GetValueOrDefault().ID);
        BoOrder.ItemsAmount = OrderItems.Sum(item => item.GetValueOrDefault().Amount);
        BoOrder.TotalPrice = OrderItems.Sum(item => item.GetValueOrDefault().Price * item.GetValueOrDefault().Amount);
        return BoOrder;
    }

    public IEnumerable<BO.OrderForList?> getOrderList()
    {
        IEnumerable<DO.Order?> tmp = Dal.Order.GetAll();
        BO.OrderForList boOrder = new BO.OrderForList();
        double total = 0;
        int amount = 0;
        var newList = (from DO.Order item in tmp select doOrderToOrderForList(item, boOrder, ref total, ref amount)).ToList();
        return newList;
    }

    public BO.Order? getDetailsOrder(int IdOrder)
    //מקבלת מזהה של הזמנה ומחזירה את ההזמנה שזה המזהה שלה
    {
        double total = 0;
        try //אם הת"ז שלילית, זורקים חריגה
        {
            if (IdOrder < 0)
                throw new BO.InvalidIDException(IdOrder);
        }
        catch (BO.InvalidIDException ex) { Console.WriteLine(ex); }
        DO.Order? orderDo = Dal.Order.GetById(IdOrder); //מבקשים משכבת הנתונים את ההזמנה הרצויה
        //עושים המרה מהזמנה מסוג שכבת הנתונים להזמנה מסוג שכבת הלוגיקה
        BO.Order? orderBo = new BO.Order();
        orderToBoOrder(orderDo, orderBo);
        return orderBo;
    }

    public BO.Order? UpdateShipDate(int IdOrder)
    {
        try //אם הת"ז שלילית, זורקים חריגה
        {
            if (IdOrder < 0)
                throw new BO.InvalidIDException(IdOrder);
        }
        catch (BO.InvalidIDException ex) { Console.WriteLine(ex); }
        try
        {
            DO.Order? orderDo = Dal.Order.GetById(IdOrder);
            if (orderDo.GetValueOrDefault().ShipDate == null)
            {
                Dal.Order.Update(new DO.Order
                {
                    ID = IdOrder,
                    CostumerName = orderDo.GetValueOrDefault().CostumerName,
                    CostumerEmail = orderDo.GetValueOrDefault().CostumerEmail,
                    CostumerAdress = orderDo.GetValueOrDefault().CostumerAdress,
                    OrderDate = orderDo.GetValueOrDefault().OrderDate,
                    ShipDate = DateTime.Now,
                    DeliveryDate = null,
                    isDeleted = false
                });
                BO.Order? orderBo = new BO.Order();
                orderToBoOrder(orderDo, orderBo);
                orderBo.ShipDate = DateTime.Now;
                return orderBo;
            }
        }
        catch (BO.DontExistException ex) { throw new BO.DontExistException(ex.ID, ex.Message, ex); }
        return null;
    }
    public BO.Order? UpdateDeliveryDate(int IdOrder)
    {
        try //אם הת"ז שלילית, זורקים חריגה
        {
            if (IdOrder < 0)
                throw new BO.InvalidIDException(IdOrder);
        }
        catch (BO.InvalidIDException ex) { Console.WriteLine(ex); }
        try
        {
            DO.Order? orderDo = Dal.Order.GetById(IdOrder);
            if (orderDo.GetValueOrDefault().DeliveryDate == null && orderDo.GetValueOrDefault().ShipDate != null)
            {
                Dal.Order.Update(new DO.Order
                {
                    ID = IdOrder,
                    CostumerName = orderDo.GetValueOrDefault().CostumerName,
                    CostumerEmail = orderDo.GetValueOrDefault().CostumerEmail,
                    CostumerAdress = orderDo.GetValueOrDefault().CostumerAdress,
                    OrderDate = orderDo.GetValueOrDefault().OrderDate,
                    ShipDate = orderDo.GetValueOrDefault().ShipDate,
                    DeliveryDate = DateTime.Now,
                    isDeleted = false
                });
                BO.Order? orderBo = new BO.Order();
                orderToBoOrder(orderDo, orderBo);
                orderBo.DeliveryDate = DateTime.Now;
                return orderBo;
            }
        }
        catch (BO.DontExistException ex) { throw new BO.DontExistException(ex.ID, ex.Message, ex); }
        return null;
    }
    public BO.OrderTracking? Tracking(int IdOrder)
    {
        try //אם הת"ז שלילית, זורקים חריגה
        {
            if (IdOrder < 0)
                throw new BO.InvalidIDException(IdOrder);
        }
        catch (BO.InvalidIDException ex) { Console.WriteLine(ex); }
        DO.Order? orderDo = Dal.Order.GetById(IdOrder);
        BO.OrderTracking orderTracking = new BO.OrderTracking();
        orderTracking.Tracking = new List<Tuple<DateTime?, string>> { };
        orderTracking.ID = orderDo.GetValueOrDefault().ID;
        if (orderDo.GetValueOrDefault().DeliveryDate == null)
        {
            if (orderDo.GetValueOrDefault().ShipDate == null)
            {
                orderTracking.State = BO.Status.approved;
                Tuple<DateTime?, string> t = new(orderDo.GetValueOrDefault().OrderDate, "The order was approved");
                orderTracking.Tracking.Add(t);
                //Console.WriteLine(t.Item1);
                //Console.WriteLine(t.Item2);
            }
            else
            {
                orderTracking.State = BO.Status.sent;
                Tuple<DateTime?, string> t = new(orderDo.GetValueOrDefault().ShipDate, "The order was sent");
                orderTracking.Tracking.Add(t);
                //Console.WriteLine(t.Item1);
                //Console.WriteLine(t.Item2);
            }
        }
        else
        {
            orderTracking.State = BO.Status.delivered;
            Tuple<DateTime?, string> t = new(orderDo.GetValueOrDefault().DeliveryDate, "The order was delivered");
            orderTracking.Tracking.Add(t);
            //Console.WriteLine(t.Item1);
            //Console.WriteLine(t.Item2);
        }
        return orderTracking;
    }
    public DO.OrderItem? UpdateOrder(int IdOrder, int IdProduct, int newAmount)
    //בונוס, בשביל המנהל
    {
        if (newAmount < 0)
            throw new BO.AmountException();
        DO.OrderItem? item = Dal.OrderItem.getItem(IdOrder, IdProduct);
        Dal.OrderItem.Update(new DO.OrderItem
        {
            ID = item.GetValueOrDefault().ID,
            OrderID = IdOrder,
            ProductID = IdProduct,
            Price = item.GetValueOrDefault().Price,
            Amount = newAmount,
            isDeleted = false
        });
        return Dal.OrderItem.getItem(IdOrder, IdProduct);
    }
}
