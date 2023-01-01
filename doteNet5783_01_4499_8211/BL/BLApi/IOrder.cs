
using BO;

namespace BLApi;

public interface IOrder
{
    public IEnumerable<BO.OrderForList?> getOrderList(Status? state = null);
    public BO.Order  getDetailsOrder (int IdOrder);
    public BO.Order UpdateShipDate (int IdOrder);
    public BO.Order UpdateDeliveryDate(int IdOrder);
    public BO.OrderTracking Tracking (int IdOrder);
    public DO.OrderItem UpdateOrder(int IdOrder, int IdProduct, int newAmount);
}
