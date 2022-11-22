using System;
using System.Collections.Generic;
using System.Linq;
using BLApi;

namespace BlImplementation;

internal class ProductItem: IProductItem
{
    private DalApi.IDal Dal = DalApi.DalFactory.GetDal();
}
