using System;
using System.Collections.Generic;
using System.Text;

namespace MT.OnlineRestaurant.BusinessEntities
{
    public class CartItemsEntity
    {
        // Number of items in the cart
        public int CartItems { get; set; }
        // Item availability
        public bool IsItemAvailable { get; set; }
    }
}
