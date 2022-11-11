﻿using DalApi;
using DO;


namespace Dal;

public class DalOrder :IOrder
{
   public int Add(Order item)
    {
        DataSource.ListOrder.Add(item);
        return 0;   
    }
    public Order GetById(int id)
    {
        return (DataSource.ListOrder.Find(item => item.ID == id));
    }
    public void Update(Order item)
    {
        Order order= DataSource.ListOrder.Find(found => found.ID == item.ID);
        //if (order==null) 
        //    return; //לשאול את נורית
        DataSource.ListOrder.Remove(order);
        DataSource.ListOrder.Add(item);    
    }
    public  void Delete(int id)
    {
        DataSource.ListOrder.RemoveAll(item => item.ID == id);
    }

    //IEnumerable<T?> GetAll(Func<T?, bool>? filter = null);
    public IEnumerable<Order> GetAll()
    {
        return DataSource.ListOrder;
    }

}