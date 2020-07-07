using AutoMapper;
using MT.OnlineRestaurant.BusinessEntities;
using MT.OnlineRestaurant.BusinessLayer.interfaces;
using MT.OnlineRestaurant.DataLayer.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MT.OnlineRestaurant.BusinessLayer
{
    public class CartActions : ICartActions
    {
        private readonly IGetCartItems _getCartItems;
        private readonly IMapper _mapper;
        public CartActions(IGetCartItems getCartItems, IMapper mapper)
        {
            _getCartItems = getCartItems;
            _mapper = mapper;
        }

        public List<OrdersInCart> GetItemsinCart(int customerId)
        {
            var itemsInCart = _getCartItems.GetItemsInCart(customerId);
            List<OrdersInCart> CartItems = new List<OrdersInCart>();
            foreach (var item in itemsInCart)
            {

                var cart = _mapper.Map<OrdersInCart>(item);
                CartItems.Add(cart);
            }
            return CartItems;
        }

    }
}
