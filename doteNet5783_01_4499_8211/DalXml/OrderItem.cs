namespace Dal;
using DalApi;
using DO;
using System.Security.Principal;

internal class OrderItem : IOrderItem
{
    const string s_OrderItems = "OrderItems"; //XML Serializer
    public const string s_Config = "Config";

    public IEnumerable<DO.OrderItem?> GetAll(Func<DO.OrderItem?, bool>? filter = null)
    {
        var listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_OrderItems)!;
        return filter == null ? listOrderItems.OrderBy(lec => ((DO.OrderItem)lec!).ID)
                              : listOrderItems.Where(filter).OrderBy(lec => ((DO.OrderItem)lec!).ID);
    }

    public DO.OrderItem GetById(int id) =>
        XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_OrderItems).FirstOrDefault(p => p?.ID == id)
        ?? throw new DoesNotExistException(id);

    public int Add(DO.OrderItem orderItem)
    {
        var listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_OrderItems);

        var runningList = XMLTools.LoadListFromXMLSerializer<RuningNumber>(s_Config);

        RuningNumber runningNum = runningList.FirstOrDefault(num => num?.typeOfnumber == "OrderItem Running Number")
            ?? throw new RuningNumberDoesNotExistException("OrderItem Running Number");

        runningList.Remove(runningNum);

        runningNum.numberSaved++;

        orderItem.ID = (int)runningNum.numberSaved;

        listOrderItems.Add(orderItem);
        runningList.Add(runningNum);

        XMLTools.SaveListToXMLSerializer(listOrderItems, s_OrderItems);
        XMLTools.SaveListToXMLSerializer(runningList, s_Config);

        return orderItem.ID;
    }

    public void Delete(int id)
    {
        var listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_OrderItems);

        if (listOrderItems.RemoveAll(p => p?.ID == id) == 0)
            throw new DoesNotExistException(id); 

        XMLTools.SaveListToXMLSerializer(listOrderItems, s_OrderItems);
    }

    public void Update(DO.OrderItem OrderItem)
    {
        Delete(OrderItem.ID);
        Add(OrderItem);
    }

    public DO.OrderItem GetItem(int IdOrder, int IdProduct)
    {
        var listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_OrderItems);

        return listOrderItems.FirstOrDefault(item => item?.ProductID == IdProduct && item?.OrderID == IdOrder)
            ?? throw new DoesNotExistException(IdOrder); ;
    }
}
