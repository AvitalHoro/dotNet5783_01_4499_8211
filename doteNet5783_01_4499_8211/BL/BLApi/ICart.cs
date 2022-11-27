using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLApi;

 public interface ICart
{
    public BO.Cart? AddProduct(BO.Cart? cart, int idProduct);
    public BO.Cart? UpdateAmountProduct(BO.Cart cart, int idProduct, int amount);
    public int MakeOrder(BO.Cart cart, string costumerName, string costumerEmail, string costumerAdress);
}
