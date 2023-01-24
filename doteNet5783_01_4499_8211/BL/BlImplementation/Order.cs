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
    /// <exception cref="BO.DoesNotExistException"></exception>
    //פונקציה שמקבלת פריט הזמנה מסוג שכבת הנתונים וממירה אותו לפריט הזמנה מסוג שכבת הלוגיקה
    public BO.OrderItem UpdateItemListForOrder(DO.OrderItem doOrder)
    {
        BO.OrderItem boOrder = new BO.OrderItem();
        BO.Tools.CopyPropTo(doOrder, boOrder);
        try 
        { 
            DO.Product product =(Dal.Product.GetById(boOrder.ProductID));
            boOrder.NameProduct = product.Name;
            boOrder.Path= product.Path; 
        }
        catch (BO.DoesNotExistException ex) { throw new BO.DoesNotExistException(ex.ID, ex.Message, ex); }
        boOrder.TotalPrice = doOrder.Price * doOrder.Amount;
        return boOrder;
    }
    #endregion

    #region OrderToboOrder
    //ממירה הזמנה משכבת הנתונים להזמנה משכבת הלוגיקה
    public void OrderToboOrder(DO.Order? orderDo, BO.Order? orderBo)
    {
        double total = 0;
        BO.Tools.CopyPropTo(orderDo, orderBo);
        IEnumerable<DO.OrderItem?> list = Dal.OrderItem.GetAll(item=> orderDo?.ID== item?.OrderID); //מבקשים משכבת הנתונים רשימה של כל הפריטים בהזמנה 
        var newList = (from DO.OrderItem item in list
                       let orderItem = UpdateItemListForOrder(item)
                       select orderItem)
                       .ToList();
        orderBo.Items = newList; //מעדכנים את הרשימה של הפריטים שיש בהזמנה
        if(orderDo?.TotalPrice == 0)
             orderBo.TotalPrice = newList.Sum(item => item.TotalPrice); //המחיר הכללי של ההזמנה שווה לסך מחיר כל הפריטים
        if (orderDo?.DeliveryDate != null)
            orderBo.State = BO.Status.delivered;
        else if (orderDo?.ShipDate != null)
            orderBo.State = BO.Status.sent;
        else
            orderBo.State = BO.Status.approved;
    }
    #endregion

    #region DoOrderToOrderForList
    //ממירה הזמנה מסוג שכבת הנתונים להזמנה לרשימה מסוג שכבת הלוגיקה
    public BO.OrderForList DoOrderToOrderForList(DO.Order doOrder)
    {
        BO.OrderForList boOrder = new BO.OrderForList();
        BO.Tools.CopyPropTo(doOrder, boOrder);
        var OrderItems = Dal.OrderItem.GetAll(item=> doOrder.ID == item?.OrderID ); //מביא משכבת הנתונים את כל הפריטים של ההזמנה
        boOrder.ItemsAmount = OrderItems.Sum(item => item?.Amount)??0; //מעדכן את כמות המוצרים בהזמנה
        if (doOrder.TotalPrice == 0)
            boOrder.TotalPrice = OrderItems.Sum(item => item?.Price * item?.Amount) ?? 0; //מעדכן את המחיר הכולל של הזמנה
        else boOrder.TotalPrice = doOrder.TotalPrice;
        //מעדכן את סטטוס ההזמנה
        if (doOrder.DeliveryDate != null)
            boOrder.State = BO.Status.delivered;
        else if (doOrder.ShipDate != null)
            boOrder.State = BO.Status.sent;
        else
            boOrder.State = BO.Status.approved;
        return boOrder;
    }
    #endregion

    #region GetOrderList
    //מחזירה רשימה של כל ההזמנות
    public IEnumerable<BO.OrderForList?> GetOrderList(Status? state = null)
    {
        if(state != null)
        {
            IEnumerable<DO.Order?> tmp =
           state switch
           {
               Status.approved =>
                 Dal.Order.GetAll(order => order?.ShipDate == null && order?.DeliveryDate == null && order?.IsDeleted == false).OrderBy(order=>order?.ID),

               Status.sent =>
                   Dal.Order.GetAll(order => order?.ShipDate != null && order?.DeliveryDate == null && order?.IsDeleted == false).OrderBy(order => order?.ID),

               Status.delivered =>
                  Dal.Order.GetAll(order => order?.ShipDate != null && order?.DeliveryDate != null && order?.IsDeleted == false).OrderBy(order => order?.ID),
           };
            return (from DO.Order item in tmp
                    let orderForList = DoOrderToOrderForList(item) //ממיר הזמנה מסוג שכבת הנתונים להזמנה לרשימה מסוג שכבת הלוגיקה
                    select orderForList)
                    .OrderBy(order => order?.ID)
              .ToList();
        }
        else
        {
            IEnumerable<DO.Order?> tmp = Dal.Order.GetAll(o => o?.IsDeleted == false); 
            return (from DO.Order item in tmp
                    let orderForList = DoOrderToOrderForList(item) //ממיר הזמנה מסוג שכבת הנתונים להזמנה לרשימה מסוג שכבת הלוגיקה
                    select orderForList)
                    .OrderBy(order => order?.ID)
             .ToList();
        }
    }
    #endregion

    #region GetDetailsOrder
    //מקבלת מזהה של הזמנה ומחזירה את ההזמנה שזה המזהה שלה
    public BO.Order GetDetailsOrder(int idOrder)
    {
        try //אם הת"ז שלילית, זורקים חריגה
        {
            if (idOrder < 0)
                throw new BO.InvalidIDException(idOrder);
            DO.Order? orderDo = Dal.Order.GetById(idOrder); //מבקשים משכבת הנתונים את ההזמנה הרצויה
            BO.Order? orderBo = new BO.Order();
            OrderToboOrder(orderDo, orderBo);
            return orderBo;
        }
        catch (BO.InvalidIDException ex) { throw new BO.InvalidIDException(ex.ID); }
    }
    #endregion

    #region CancelOrder
    public void CancelOrder(int idOrder)
    {
        try
        {
            var items = Dal.OrderItem.GetAll(item => item?.OrderID == idOrder);
            foreach(DO.OrderItem item in items) Dal.OrderItem.Delete(item.ID);
            Dal.Order.Delete(idOrder); 
        }
        catch (BO.InvalidIDException ex) { new BO.InvalidIDException(ex.ID); }
    }
    #endregion

    #region UpdateShipDate
    /// <exception cref="BO.InvalidIDException"></exception>
    /// <exception cref="BO.OrderAlreadyShippedExecption"></exception>
    /// <exception cref="BO.DoesNotExistException"></exception>
    //מעדכנת את התאריך שליחה של הזמנה ומחזיר את ההזמנה המעוכנת
    public BO.Order UpdateShipDate(int idOrder, DateTime? date=null)
    {
        try //אם הת"ז שלילית, זורקים חריגה
        {
            if (idOrder < 0)
                throw new BO.InvalidIDException(idOrder);
        }
        catch (BO.InvalidIDException ex) { throw new BO.InvalidIDException(ex.ID); }
        if(date==null)
            date= DateTime.Now;
        try
        {
            DO.Order orderDo = Dal.Order.GetById(idOrder);
            if (orderDo.ShipDate == null) //אם ההזמנה לא נשלחה עדיין
            {
               Dal.Order.Update(new DO.Order //מעדכן בשכבת הנתונים שההזמנה נשלחה כרגע
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
                OrderToboOrder(orderDo, orderBo);
                orderBo.State = Status.sent;
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
    //מעדכנת את התאריך מסירה של הזמנה ומחזירה את ההזמנה המעוכנת
    public BO.Order UpdateDeliveryDate(int idOrder, DateTime? date=null)
    {
        try //אם הת"ז שלילית, זורקים חריגה
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
            if (orderDo.DeliveryDate == null && orderDo.ShipDate != null) //בודק שההזמנה אכן כבר נשלחה ועדיין לא נמסרה
            {
                Dal.Order.Update(new DO.Order //מעדכן בשכבת הנתונים שההזמנה נמסרה כרגע
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
                BO.Order? orderBo = new BO.Order();
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
    //הפונקציה מחזירה מעקב הזמנה, שבה יש רשימה של צמדים- תאריכים ומה קרה באותו תאריך להזמנה
    public BO.OrderTracking Tracking(int idOrder)
    {
        try //אם הת"ז שלילית, זורקים חריגה
        {
            if (idOrder < 0)
                throw new BO.InvalidIDException(idOrder);
        }
        catch (BO.InvalidIDException ex) { new BO.InvalidIDException(ex.ID); }
        try
        {
            DO.Order orderDo = Dal.Order.GetById(idOrder);
            BO.OrderTracking orderTracking = new BO.OrderTracking();
            //יוצר רשימה ריקה של צמדים
            List<Tuple<DateTime?, string>> listTuples = new List<Tuple<DateTime?, string>> { }; 
            orderTracking.ID = orderDo.ID;
            orderTracking.State = BO.Status.approved;
            listTuples.Add(Tuple.Create(orderDo.OrderDate, ":אושרה"));
            if (orderDo.ShipDate != null) //אם כבר יש תאריך שליחה, מוסיף לרשימת הצמדים
            {
                orderTracking.State = BO.Status.sent;
                listTuples.Add(Tuple.Create(orderDo.ShipDate, ":נשלחה"));
            }
            if (orderDo.DeliveryDate != null) //אם כבר יש תאריך מסירה, מוסיף לרשימת הצמדים
            {
                orderTracking.State = BO.Status.delivered;
                listTuples.Add(Tuple.Create(orderDo.DeliveryDate, ":התקבלה"));
            }
            orderTracking.Tracking = listTuples;
            return orderTracking;
        }
        catch (DO.DoesNotExistException ex) { throw new BO.DoesNotExistException(ex.ID); }
    }
    #endregion

    #region UpdateOrder
    /// <exception cref="BO.AmountException"></exception>
    //מעדכן כמות בהזמנה קיימת, פונקציה שזמינה רק למנהל
    public DO.OrderItem UpdateOrder(int idOrder, int IdProduct, int newAmount)
    {
        if (newAmount < 0)
            throw new BO.AmountException();
        DO.OrderItem item = Dal.OrderItem.GetItem(idOrder, IdProduct);
        Dal.OrderItem.Update(new DO.OrderItem  //מעדכן את המוצר בשכבת הנתונים
        {
            ID = item.ID,
            OrderID = idOrder,
            ProductID = IdProduct,
            Price = item.Price,
            Amount = newAmount,
        });
        return Dal.OrderItem.GetItem(idOrder, IdProduct); 
    }
    #endregion
}
