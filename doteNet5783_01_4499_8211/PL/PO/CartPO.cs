using BO;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace PO;

class CartPO : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

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

    private List<OrderItem?>? orderItems;
    public List<OrderItem?>? OrderItems
    {
        get
        { return orderItems; }
        set
        {
            orderItems = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("OrderItems"));
            }
        }
    }

}
