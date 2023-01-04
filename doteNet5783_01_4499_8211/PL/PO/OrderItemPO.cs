using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.PO;
public class OrderItemPO : INotifyPropertyChanged
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

    private int productId;
    public int ProductID
    {
        get
        { return productId; }
        set
        {
            productId = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("ProductID"));
            }
        }
    }

    private string? nameProduct;
    public string? NameProduct
    {
        get
        { return nameProduct; }
        set
        {
            nameProduct = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("NameProduct"));
            }
        }
    }

    private double price;
    public double Price
    {
        get
        { return price; }
        set
        {
            price = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Price"));
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

    private int amount;
    public int Amount
    {
        get
        { return amount; }
        set
        {
            amount = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Amount"));
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

    private string? path;
    public string? Path
    {
        get
        { return path; }
        set
        {
            path = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Path"));
            }
        }
    }
}
