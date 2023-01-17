using BO;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace PL.PO;

public class OrderPO : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private int id;
    public int ID
    {
        get
        { return id; }
        set
        {
            id = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("ID"));
            }
        }
    }

    private string? costumerName;
    public string? CostumerName
    {
        get
        { return costumerName; }
        set
        {
            costumerName = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("CostumerName"));
            }
        }
    }


    private string? costumerEmail;
    public string? CostumerEmail
    {
        get
        { return costumerEmail; }
        set
        {
            costumerEmail = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("CostumerEmail"));
            }
        }
    }


    private string? costumerAdress;
    public string? CostumerAdress
    {
        get
        { return costumerAdress; }
        set
        {
            costumerAdress = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("CostumerAdress"));
            }
        }
    }

    private Status state;
    public Status State
    {
        get
        { return state; }
        set
        {
            state = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("State"));
            }
        }
    }

    private DateTime? orderDate;
    public DateTime? OrderDate
    {
        get
        { return orderDate; }
        set
        {
            orderDate = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("OrderDate"));
            }
        }
    }

    private DateTime? shipDate;
    public DateTime? ShipDate
    {
        get
        { return shipDate; }
        set
        {
            shipDate = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("ShipDate"));
            }
        }
    }

    private DateTime? deliveryDate;
    public DateTime? DeliveryDate
    {
        get
        { return deliveryDate; }
        set
        {
            deliveryDate = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("DeliveryDate"));
            }
        }
    }

    private double totalPrice;
    public double TotalPrice
    {
        get
        { return totalPrice; }
        set
        {
            totalPrice = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("TotalPrice"));
            }
        }
    }

    private int itemsAmount;
    public int ItemsAmount
    {
        get
        { return itemsAmount; }
        set
        {
            itemsAmount = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("ItemsAmount"));
            }
        }
    }


    private List<OrderItem?>? items;
    public List<OrderItem?>?  Items
    {
        get
        { return items; }
        set
        {
            items = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Items"));
            }
        }
    }

    private bool isDeleted;
    public bool IsDeleted
    {
        get
        { return isDeleted; }
        set
        {
            isDeleted = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("IsDeleted"));
            }
        }
    }

}
