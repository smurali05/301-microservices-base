using AutoMapper;
using Microsoft.Extensions.Options;
using MT.OnlineRestaurant.BusinessEntities;
using MT.OnlineRestaurant.BusinessEntities.ServiceModels;
using MT.OnlineRestaurant.BusinessLayer.interfaces;
using MT.OnlineRestaurant.DataLayer;
using MT.OnlineRestaurant.DataLayer.interfaces;
using MT.OnlineRestaurant.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MT.OnlineRestaurant.BusinessLayer
{
    public class PlaceOrderActions : IPlaceOrderActions
    {
        // Create a field to store the mapper object
        private readonly IMapper _mapper;
        private readonly IPlaceOrderDbAccess _placeOrderDbAccess;
        private readonly IOptions<ConnectionStrings> _connectionStrings;

        public PlaceOrderActions()
        {

        }

        public PlaceOrderActions(IPlaceOrderDbAccess placeOrderDbAccess)
        {
            _placeOrderDbAccess = placeOrderDbAccess;
        }

        public PlaceOrderActions(IPlaceOrderDbAccess placeOrderDbAccess, IMapper mapper, IOptions<ConnectionStrings> connectionStrings)
        {
            _placeOrderDbAccess = placeOrderDbAccess;
            _mapper = mapper;
            _connectionStrings = connectionStrings;
        }

        /// <summary>
        /// Place order
        /// </summary>
        /// <param name="orderEntity">Order details</param>
        /// <returns>order id</returns>
        public int PlaceOrder(OrderEntity orderEntity)
        {
            DataLayer.Context.TblFoodOrder tblFoodOrder = _mapper.Map<DataLayer.Context.TblFoodOrder>(orderEntity);

            IList<DataLayer.Context.TblFoodOrderMapping> tblFoodOrderMappings = new List<DataLayer.Context.TblFoodOrderMapping>();

            foreach (OrderMenus orderMenu in orderEntity.OrderMenuDetails)
            {
                tblFoodOrderMappings.Add(new DataLayer.Context.TblFoodOrderMapping()
                {
                    TblFoodOrderId = 0,
                    TblMenuId = orderMenu.MenuId,
                    Price = orderMenu.Price,
                    UserCreated = 0,
                    RecordTimeStampCreated = DateTime.Now
                });
            }

            tblFoodOrder.TblFoodOrderMapping = tblFoodOrderMappings;

            return _placeOrderDbAccess.PlaceOrder(tblFoodOrder);
        }

        public List<OrderMenus> IsOrderPriceChanged(OrderEntity orderEntity, int orderId)
        {
            List<OrderMenus> orderMenus = new List<OrderMenus>();
            var orderFoods = _placeOrderDbAccess.IsOrderPriceChanged(orderEntity, orderId);
            if (orderFoods.Count > 0)
            {
                foreach (var item in orderFoods)
                {
                    orderMenus.Add(new OrderMenus()
                    {
                        MenuId = item.TblMenuId,
                        Price = item.Price
                    });
                }
            }

            return orderMenus;
        }

        /// <summary>
        /// Cancel Order
        /// </summary>
        /// <param name="orderId">order id</param>
        /// <returns></returns>
        public int CancelOrder(int orderId)
        {
            return (orderId > 0 ? _placeOrderDbAccess.CancelOrder(orderId) : 0);
        }

        /// <summary>
        /// gets the customer placed order details
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public IQueryable<CustomerOrderReport> GetReports(int customerId)
        {
            var foodOrders = _placeOrderDbAccess.GetReports(customerId);
            if (foodOrders.Any())
            {
                return foodOrders.Select(x => new CustomerOrderReport
                {
                    OrderedDate = x.RecordTimeStampCreated,
                    OrderStatus = x.TblOrderStatus.Status,
                    OrderId = x.Id,
                    PaymentStatus = x.TblOrderPayment.Any() ? x.TblOrderPayment.FirstOrDefault().TblPaymentStatus.Status : string.Empty,
                    price = x.TotalPrice
                }).AsQueryable();
            }

            return null;
        }

        public async Task<bool> IsValidRestaurantAsync(OrderEntity orderEntity, int UserId, string UserToken)
        {
            using (HttpClient httpClient = WebAPIClient.GetClient(UserToken, UserId, _connectionStrings.Value.RestaurantApiUrl))
            {
                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("api/ResturantDetail?RestaurantID=" + orderEntity.RestaurantId);
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string json = await httpResponseMessage.Content.ReadAsStringAsync();
                    RestaurantInformation restaurantInformation = JsonConvert.DeserializeObject<RestaurantInformation>(json);
                    if (restaurantInformation != null)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public async Task<bool> IsOrderItemInStock(OrderEntity orderEntity, int UserId, string UserToken)
        {
            //using (HttpClient httpClient = WebAPIClient.GetClient(UserToken, UserId, _connectionStrings.Value.RestaurantApiUrl))
            using (HttpClient httpClient = new HttpClient())
            {
                //HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("http://localhost:10601/api/OrderDetail?RestaurantID=" + orderEntity.RestaurantId + "&menuID=" + orderEntity.OrderMenuDetails.First().MenuId);
                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("https://searchapi123.azurewebsites.net/api/OrderDetail?RestaurantID=" + orderEntity.RestaurantId + "&menuID=" + orderEntity.OrderMenuDetails.First().MenuId);
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string json = await httpResponseMessage.Content.ReadAsStringAsync();
                    int restaurantInformationId = JsonConvert.DeserializeObject<int>(json);
                    if (restaurantInformationId == orderEntity.RestaurantId)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //public async Task<bool> IsValidRestaurant(OrderEntity orderEntity)
        //{
        //    using (HttpClient httpClient = WebAPIClient.GetClient(orderEntity.UserToken, orderEntity.UserId, "http://localhost:10601/"))
        //    {
        //        HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("api/ResturantMenuDetail?RestaurantID=" + orderEntity.RestaurantId);
        //        if (httpResponseMessage.IsSuccessStatusCode)
        //        {
        //            string json = await httpResponseMessage.Content.ReadAsStringAsync();
        //            IQueryable<RestaurantMenu> restaurantInformation = JsonConvert.DeserializeObject<IQueryable<RestaurantMenu>>(json);
        //            if (restaurantInformation != null)
        //            {
        //                return true;
        //            }
        //        }
        //    }
        //    return false;
        //}

        public List<FoodOrderMapping> UpdateStockPrice(StockPrice stocks)
        {
            List<FoodOrderMapping> Food = new List<FoodOrderMapping>();
            var foodOrder = _placeOrderDbAccess.UpdateStockPrice(stocks);
            if (foodOrder != null)
            {
                foreach (var order in foodOrder)
                {
                    Food.Add(new FoodOrderMapping()
                    {
                        IsItemOutOfStock = order.IsItemOutOfStock,
                        TblFoodOrderId = order.TblFoodOrderId,
                        TblMenuId = order.TblMenuId,
                        Price = order.Price,
                        Active = order.Active,
                        Quantity = order.Quantity
                    });
                }
   
            }
            
            return Food;
        }

        public List<FoodOrderMapping> UpdateOutOfStock(StockInformation stock)
        {
            List<FoodOrderMapping> foodOrderMappings = new List<FoodOrderMapping>();
            var foodOrder = _placeOrderDbAccess.UpdateOutOfStock(stock);
            foreach (var item in foodOrder)
            {
                var Order = _mapper.Map<FoodOrderMapping>(foodOrder);
                foodOrderMappings.Add(Order);
            }
            return foodOrderMappings;
        }
    }
}
