using BLApi;
using BO;
using System.Diagnostics.CodeAnalysis;

namespace BlImplementation;

internal class Order: IOrder
{
    private DalApi.IDal Dal = DalApi.DalFactory.GetDal() ?? throw new NullReferenceException("Missing Dal");;

    public IEnumerable<BO.OrderForList?> getOrderList()
    {
        IEnumerable<DO.Order?> tmp = Dal.Order.GetAll();
        List<BO.OrderForList?> newList = new List<BO.OrderForList?> { };
        BO.OrderForList boOrder= new BO.OrderForList();
        double sum = 0;

        foreach (DO.Order? order in tmp)
        {
            boOrder =BlImplementation.Cloning.Clone(order, boOrder);
            var OrderItems = Dal.OrderItem.GetAll(order.GetValueOrDefault().ID);
            boOrder.ItemsAmount = OrderItems.Count();
            foreach (var item in OrderItems)
            { sum = sum + (item.GetValueOrDefault().Price * item.GetValueOrDefault().Amount); };
            boOrder.TotalPrice = sum;
                newList.Add(boOrder);
        }
        return newList; 
    }
    //{
    //    IEnumerable<DO.Order?> tmp = Dal.Order.GetAll();
    //    List<BO.OrderForList?> newList = new List<BO.OrderForList?> { };
    //    double sum = 0;

    //    foreach (DO.Order? order in tmp)
    //    {
    //        var OrderItems = Dal.OrderItem.GetAll(order.GetValueOrDefault().ID);
    //        if (order.GetValueOrDefault().DeliveryDate == null)
    //        {

    //        }

    //        newList.Add(new OrderForList
    //        {
    //            ID = order.GetValueOrDefault().ID,
    //            CostumerName = order.GetValueOrDefault().CostumerName,  
    //            ItemsAmount= OrderItems.Count(),
    //            TotalPrice = foreach(var item in OrderItems)
    //            { sum = sum + (item.GetValueOrDefault().Price * item.GetValueOrDefault().Amount); };
    //            state=
    //        }
    //        );
    // };
    // return newList;
    // }

    public BO.Order? getDetailsOrder(int IdOrder);
    public BO.Order? UpdateShipDate(int IdOrder);
    public BO.Order? UpdateDeliveryDate(int IdOrder);
    public BO.OrderTracking? Tracking(int IdOrder);
    public void UpdateOrder(int IdOrder);
}
