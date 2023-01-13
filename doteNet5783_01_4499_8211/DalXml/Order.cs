namespace Dal;
using DalApi;
using DO;
using System;
using System.Security.Principal;

internal class Order : IOrder
{
    const string s_Orders = "Orders"; //XML Serializer
    public const string s_Config = "Config";

    public IEnumerable<DO.Order?> GetAll(Func<DO.Order?, bool>? filter = null)
    {
        var listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_Orders)!;
        return filter == null ? listOrders.OrderBy(lec => ((DO.Order)lec!).ID)
                              : listOrders.Where(filter).OrderBy(lec => ((DO.Order)lec!).ID);
    }

    public DO.Order GetById(int id) =>
        XMLTools.LoadListFromXMLSerializer<DO.Order>(s_Orders).FirstOrDefault(p => p?.ID == id)
        ?? throw new DoesNotExistException(id);

    public int Add(DO.Order order)
    {
        var listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_Orders);

        if (listOrders.Exists(lec => lec?.ID == order.ID && lec?.IsDeleted == false))
            throw new AlreadyExistsException(order.ID);

        var runningList = XMLTools.LoadListFromXMLSerializer<RuningNumber>(s_Config);


        RuningNumber runningNum = runningList.FirstOrDefault(num => num?.typeOfnumber == "Order Running Number")
            ??throw new RuningNumberDoesNotExistException("Order Running Number");

        runningList.Remove(runningNum);

        runningNum.numberSaved++;

        order.ID = (int)runningNum.numberSaved;

        listOrders.Add(order);
        runningList.Add(runningNum);    

        XMLTools.SaveListToXMLSerializer(listOrders, s_Orders);
        XMLTools.SaveListToXMLSerializer(runningList, s_Config);

        return order.ID;
    }

    public void Delete(int id)
    {
        var listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_Orders);

        var o = listOrders.FirstOrDefault(p => p?.ID == id) ?? throw new DoesNotExistException(id);
         
        if (o.IsDeleted)
            throw new DoesNotExistException(id);

        listOrders.Remove(o);

        DO.Order order = new()
        {
            ID = id,
            CostumerName = o.CostumerName,
            CostumerEmail = o.CostumerEmail,
            CostumerAdress = o.CostumerAdress,
            OrderDate = o.OrderDate,
            DeliveryDate = o.DeliveryDate,
            ShipDate = o.ShipDate,
            IsDeleted = true
        };

        listOrders.Add(order);
        XMLTools.SaveListToXMLSerializer(listOrders, s_Orders);
    }

    public void Update(DO.Order order)
    {
        var listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_Orders);
        var o = listOrders.FirstOrDefault(p => p?.ID == order.ID) ?? throw new DoesNotExistException(order.ID);
        listOrders.Remove(o);
        listOrders.Add(order);
        XMLTools.SaveListToXMLSerializer(listOrders, s_Orders);
    }
}