using MT.OnlineRestaurant.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MT.OnlineRestaurant.BusinessLayer.interfaces
{
    public interface ICartActions
    {
        List<OrdersInCart> GetItemsinCart(int customerId);
    }
}
