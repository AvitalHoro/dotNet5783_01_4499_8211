using BLApi;
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
            boOrder = BlImplementation.Cloning.Clone(order, boOrder);
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
    {
        try
        {
            if (IdOrder < 0)
                throw new BO.InvalidIDException(IdOrder);
        }
        catch (BO.InvalidIDException ex){ Console.WriteLine(ex); }
        BO.Order order = new BO.Order();
        order= BlImplementation.Cloning.Clone( Dal.Order.GetById(IdOrder), order);
        order.Items=
       
    }

    public BO.Order? UpdateShipDate(int IdOrder);
    public BO.Order? UpdateDeliveryDate(int IdOrder);
    public BO.OrderTracking? Tracking(int IdOrder);
    public void UpdateOrder(int IdOrder);
}
