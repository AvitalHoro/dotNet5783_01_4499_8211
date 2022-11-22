using BLApi;

namespace BlImplementation;

internal class Order: IOrder
{
    private DalApi.IDal Dal = DalApi.DalFactory.GetDal();

    public IEnumerable<BO.OrderForList?> getOrderList();
    public BO.Order? getDetailsOrder(int IdOrder);
    public BO.Order? UpdateShipDate(int IdOrder);
    public BO.Order? UpdateDeliveryDate(int IdOrder);
    public BO.OrderTracking? Tracking(int IdOrder);
    public void UpdateOrder(int IdOrder);
}
