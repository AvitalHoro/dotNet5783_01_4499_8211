using BO;
using MaterialDesignThemes.Wpf;
using PL.PO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace PO;

public class CartPO : INotifyPropertyChanged
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

    private ObservableCollection<OrderItemPO?>? orderItems;
    public ObservableCollection<OrderItemPO?>? OrderItems
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
