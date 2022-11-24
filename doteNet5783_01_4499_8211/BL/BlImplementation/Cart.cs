using BLApi;
namespace BlImplementation;

internal class Cart : ICart
{
    private DalApi.IDal Dal = DalApi.DalFactory.GetDal();

    public BO.Cart? AddProduct(BO.Cart cart, int idProduct)
    public BO.Cart? UpdateAmountProduct(BO.Cart cart, int idProduct, int amount);
    public int MakeOrder(BO.Cart cart, string costumerName, string costumerEmail, string costumerAdress);

}
