using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MT.OnlineRestaurant.BusinessLayer.interfaces;

namespace MT.OnlineRestaurant.OrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : Controller
    {
        #region private Variables
        private readonly ICartActions _cartActions;
        #endregion

        #region Constructor
        public CartController(ICartActions cartActions)
        {
            _cartActions = cartActions;
        }
        #endregion
        [HttpGet]
        [Route("api/getitemsincart")]
        public IActionResult GetItemsInCart(int customerId)
        {
            var itemsInCart = _cartActions.GetItemsinCart(customerId);
            return Ok(itemsInCart);
        }
    }
}
