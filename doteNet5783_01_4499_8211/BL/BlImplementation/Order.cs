using BLApi;
using Dal;
using System.Diagnostics.CodeAnalysis;

namespace BlImplementation;

internal class Order : IOrder
{
    private DalApi.IDal Dal = DalApi.DalFactory.GetDal() ?? throw new NullReferenceException("Missing Dal");

    public IEnumerable<BO.OrderForList?> getOrderList()
    {
        IEnumerable<DO.Order?> tmp = Dal.Order.GetAll();
        List<BO.OrderForList?> newList = new List<BO.OrderForList?> { };
        BO.OrderForList boOrder = new BO.OrderForList();
        double sum = 0;

        foreach (DO.Order? order in tmp)
        {
            BlImplementation.Cloning.CopyPropTo(order, boOrder);
            var OrderItems = Dal.OrderItem.GetAll(order.GetValueOrDefault().ID);
            boOrder.ItemsAmount = OrderItems.Count();
            foreach (var item in OrderItems)
            { sum = sum + (item.GetValueOrDefault().Price * item.GetValueOrDefault().Amount); };
            boOrder.TotalPrice = sum;
            newList.Add(boOrder);
        }
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
        catch (BO.InvalidIDException ex){ Console.WriteLine(ex); }
        DO.Order? orderDo = Dal.Order.GetById(IdOrder); //מבקשים משכבת הנתונים את ההזמנה הרצויה
        //עושים המרה מהזמנה מסוג שכבת הנתונים להזמנה מסוג שכבת הלוגיקה
        BO.Order order = new BO.Order();
        BlImplementation.Cloning.CopyPropTo(orderDo, order); //מעתיקים את כל השדות שיש בשניהם
        var list = Dal.OrderItem.GetAll(IdOrder); //מבקשים משכבת הנתונים רשימה של כל הפריטים בהזמנה 
        List <BO.OrderItem> newList = new List<BO.OrderItem>(); 
        BO.OrderItem orderItemBo = new BO.OrderItem();
        foreach (var item in list) //ממירים כל פריט בהזמנה לפריט מסוג שכבת הלוגיקה 
        {
            BlImplementation.Cloning.CopyPropTo(item, orderItemBo); //מעתיקים את כל השדות שיש בשניהם
            try {orderItemBo.NameProduct = (Dal.Product.GetById(orderItemBo.ProductID)).GetValueOrDefault().Name;} 
            //מעדכנים את שם המוצר בכל פריט
            catch (BO.DontExistException ex) { throw new BO.DontExistException(ex.ID, ex.Message, ex); }
            //אם המוצר שאת השם שלו ביקשנו לא נמצא בחנות
            orderItemBo.TotalPrice = item.GetValueOrDefault().Price * item.GetValueOrDefault().Amount; 
            //מעדכנים את המחיר הכולל בכל פריט עפ"י מספר המוצרים והמחיר שלהם
            newList.Add(orderItemBo); //מוסיפים את ההזמנה החדשה לרשימה של הפריטים
        }
        order.Items = newList; //מעדכנים את הרשימה של הפריטים שיש בהזמנה
        foreach (var item in order.Items) total = total+ item.TotalPrice; //סוכמים את כל המחירים של הפריטים בהזמנה
        order.TotalPrice = total; //המחיר הכללי של ההזמנה שווה לסך מחיר כל הפריטים
        return order;
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
            DO.Order? order = Dal.Order.GetById(IdOrder);
            if(order == null)

        }
    }
    public BO.Order? UpdateDeliveryDate(int IdOrder)
    {

    }
    public BO.OrderTracking? Tracking(int IdOrder)
    {

    }
    public void UpdateOrder(int IdOrder)
        //בונוס, בשביל המנהל
    {

    }
}
