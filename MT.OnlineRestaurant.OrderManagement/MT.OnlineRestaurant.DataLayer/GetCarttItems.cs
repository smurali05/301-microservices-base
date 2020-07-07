using MT.OnlineRestaurant.DataLayer.Context;
using MT.OnlineRestaurant.DataLayer.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MT.OnlineRestaurant.DataLayer
{
    public class GetCartItems : IGetCartItems
    {
        private readonly OrderManagementContext _context;
        public GetCartItems(OrderManagementContext context)
        {
            _context = context;
        }
        public List<TblFoodOrder> GetItemsInCart(int CustomerId)
        {
            var foodOrderInCart = _context.TblFoodOrder.Where(p => p.TblPaymentTypeId == 4 & p.TblCustomerId == CustomerId).ToList();
            return foodOrderInCart;
        }
    }
}
