using System;
using System.Collections.Generic;
using System.Linq;

namespace BLApi;

 public interface ICart
{
    public BO.Cart? AddProduct(BO.Cart? cart, int idProduct);
    public BO.Cart? UpdateAmountProduct(BO.Cart cart, int idProduct, int amount);
    public int MakeOrder(BO.Cart cart);
}
