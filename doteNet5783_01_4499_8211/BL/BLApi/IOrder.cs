
using BO;

namespace BLApi;

public interface IOrder
{
    public IEnumerable<BO.OrderForList?> GetOrderList(Status? state = null);
    public BO.Order  GetDetailsOrder (int idOrder);
    public BO.Order UpdateShipDate (int idOrder, DateTime? date=null);
    public BO.Order UpdateDeliveryDate(int idOrder, DateTime? date=null);
    public BO.OrderTracking Tracking (int idOrder);
    public DO.OrderItem UpdateOrder(int idOrder, int IdProduct, int newAmount);
    public void CancelOrder (int idOrder);  
}
