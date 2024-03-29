﻿using DalApi;
using DO;


namespace Dal;

public class DalOrder : IOrder
//מממשת את כל ההמתודות של ההזמנות
{
    readonly DataSource ds = DataSource.s_instance;

    #region Add
    /// <exception cref="AlreadyExistsException"></exception>
    public int Add(Order item)
    //מוסיפה הזמנה חדשה לרשימת ההזמנות
    {
        Order? order = ds.ListOrder.FirstOrDefault(o => item.ID == o?.ID);
        if (order != null)
            if ((bool)order?.IsDeleted!)
                ds.ListOrder.RemoveAll(o => item.ID == o?.ID);
            else
                throw new AlreadyExistsException(item.ID);

        item.ID = DataSource.Config.NextOrderNumber;
        ds.ListOrder.Add(item); //מוסיף את ההזמנה
        return item.ID;//צריך להחזיר פה את התז של המוצר
    }
    #endregion

    #region GetById
    /// <exception cref="DoesNotExistException"></exception>
    public Order GetById(int id)
    {
        Order order = ds.ListOrder.FirstOrDefault(item => item?.ID == id)
            ?? throw new DoesNotExistException(id);
        if (order.IsDeleted) throw new DoesNotExistException(id);
        return order;//מחזיר את ההזמנה
    }
    #endregion

    #region Update
    public void Update(Order item)
    //מעדכן הזמנה קיימת, מזהה את ההזמנה עפ"י הת"ז
    {
        Order order = ds.ListOrder.FirstOrDefault(found => found?.ID == item.ID)
            ?? throw new DoesNotExistException(item.ID);
        if (order.IsDeleted)
            throw new DoesNotExistException(item.ID);
        ds.ListOrder.Remove(order);
        ds.ListOrder.Add(item);
    }
    #endregion

    #region Delete
    /// <exception cref="DoesNotExistException"></exception>
    public void Delete(int id)
    //מוחקת את ההזמנה שהת"ז שלה היא זאת שקיבלנו
    {
        Order found = ds.ListOrder.FirstOrDefault(item => item?.ID == id)
             ?? throw new DoesNotExistException(id); ;
        if (found.IsDeleted)
            //בודק אם ההזמנה לא נמצאת ברשימה, ואם לא נמצאת זורק חריגה
            throw new DoesNotExistException(id);

        Order order = new()
        {
            ID = id,
            CostumerName = found.CostumerName,
            CostumerEmail = found.CostumerEmail,
            CostumerAdress = found.CostumerAdress,
            OrderDate = found.OrderDate,
            DeliveryDate = found.DeliveryDate,
            ShipDate = found.ShipDate,
            IsDeleted = true
        };
        Update(order);
    }
    #endregion

    #region GetAll
    public IEnumerable<Order?> GetAll(Func<Order?, bool>? filter = null)
    {
        if (filter == null)
            return ds.ListOrder;
        return (from Order? order in ds.ListOrder 
                where filter(order)
                select order)
                .ToList();
    }
    #endregion
}
