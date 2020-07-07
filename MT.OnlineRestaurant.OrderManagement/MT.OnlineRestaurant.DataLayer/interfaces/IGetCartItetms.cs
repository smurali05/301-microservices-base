using MT.OnlineRestaurant.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace MT.OnlineRestaurant.DataLayer.interfaces
{
    public interface IGetCartItems
    {
        List<TblFoodOrder> GetItemsInCart(int CustomerId);
    }
}
