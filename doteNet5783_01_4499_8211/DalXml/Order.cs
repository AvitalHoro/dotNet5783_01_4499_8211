namespace Dal;
using DalApi;
using DO;
using System;
using System.Security.Principal;

internal class Order : IOrder
{
    const string s_Orders = "Orders"; //XML Serializer

    public IEnumerable<DO.Order?> GetAll(Func<DO.Order?, bool>? filter = null)
    {
        var listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_Orders)!;
        return filter == null ? listOrders.OrderBy(lec => ((DO.Order)lec!).ID)
                              : listOrders.Where(filter).OrderBy(lec => ((DO.Order)lec!).ID);
    }

    public DO.Order GetById(int id) =>
        XMLTools.LoadListFromXMLSerializer<DO.Order>(s_Orders).FirstOrDefault(p => p?.ID == id)
        //DalMissingIdException(id, "Order");
        ?? throw new Exception("missing id");

    public int Add(DO.Order Order)
    {
        var listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_Orders);

        if (listOrders.Exists(lec => lec?.ID == Order.ID))
            throw new Exception("id already exist");//DalAlreadyExistIdException(Order.ID, "Order");

        listOrders.Add(Order);

        XMLTools.SaveListToXMLSerializer(listOrders, s_Orders);

        return Order.ID;
    }

    public void Delete(int id)
    {
        ////var listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_Orders);

        ////if (listOrders.RemoveAll(p => p?.ID == id) == 0)
        ////    throw new Exception("missing id"); //new DalMissingIdException(id, "Order");

        ////XMLTools.SaveListToXMLSerializer(listOrders, s_Orders);

        var listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_Orders);

        var o = listOrders.FirstOrDefault(p => p?.ID == id) ?? throw new Exception("missing id");
         
        if (o.IsDeleted)
            throw new Exception("missing id"); //new DalMissingIdException(id, "Order");

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

        Update(order);
    }

    public void Update(DO.Order Order)
    {
        Delete(Order.ID);
        Add(Order);
    }
}