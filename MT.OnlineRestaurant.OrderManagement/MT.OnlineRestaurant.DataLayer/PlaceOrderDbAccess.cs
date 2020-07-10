using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MT.OnlineRestaurant.BusinessEntities;
using MT.OnlineRestaurant.BusinessEntities.ServiceModels;
using MT.OnlineRestaurant.DataLayer.Context;
using MT.OnlineRestaurant.DataLayer.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MT.OnlineRestaurant.DataLayer
{
    public class PlaceOrderDbAccess : IPlaceOrderDbAccess
    {
        private readonly OrderManagementContext _context;

        public PlaceOrderDbAccess(OrderManagementContext context)
        {
            _context = context;
        }

        public int PlaceOrder(TblFoodOrder OrderedFoodDetails)
        {
            _context.TblFoodOrder.Add(OrderedFoodDetails);
            _context.SaveChanges();
            return OrderedFoodDetails.Id;
        }

        public int CancelOrder(int orderId)
        {
            var orderedFood = _context.TblFoodOrder.Include(p => p.TblFoodOrderMapping)
                .SingleOrDefault(p => p.Id == orderId);

            orderedFood.TblFoodOrderMapping.ToList().ForEach(p => _context.TblFoodOrderMapping.Remove(p));
            _context.TblFoodOrder.Remove(orderedFood);
            _context.SaveChanges();
            
            return (orderedFood != null ? orderedFood.Id : 0);
        }

        /// <summary>
        /// gets the customer placed order details
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public IQueryable<TblFoodOrder> GetReports(int customerId)
        {
            return _context.TblFoodOrder.Where(fo => fo.TblCustomerId == customerId);
        }

        /// <summary>
        /// Update Stock price
        /// </summary>
        /// <param name="stocks"></param>
        /// <returns></returns>
        public IQueryable<TblFoodOrderMapping> UpdateStockPrice(StockPrice stocks)
        {
            try
            {
                List<TblFoodOrderMapping> tblFoodOrders = new List<TblFoodOrderMapping>();
                var foodOrderMapping = _context.TblFoodOrderMapping.Where(p => p.TblMenuId == stocks.MenuId);
                foreach (var item in foodOrderMapping)
                {
                    item.Price = stocks.ChangedPrice;
                    _context.TblFoodOrderMapping.Update(item);

                    tblFoodOrders.Add(item);
                }
                _context.SaveChanges();
                return tblFoodOrders.AsQueryable();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Item out of Stock
        /// </summary>
        /// <param name="stock"></param>
        /// <returns></returns>
        public IQueryable<TblFoodOrderMapping> UpdateOutOfStock(StockInformation stock)
        {
            List<TblFoodOrderMapping> tblFoodOrders = new List<TblFoodOrderMapping>();
            var foodOrderMapping = _context.TblFoodOrderMapping.Where(p => p.TblMenuId == stock.MenuId);
            foreach (var item in foodOrderMapping)
            {
                item.IsItemOutOfStock = true;
                _context.Update(item);

                tblFoodOrders.Add(item);
            }
            _context.SaveChanges();
            return tblFoodOrders.AsQueryable();
        }

        public List<TblFoodOrderMapping> IsOrderPriceChanged(OrderEntity orderEntity, int orderId)
        {
            var orderFoods = _context.TblFoodOrderMapping
                .Where(foodOrder =>
                    orderEntity.OrderMenuDetails.Any(o => o.MenuId == foodOrder.TblMenuId && foodOrder.TblFoodOrderId == orderId && o.Price != foodOrder.Price)).ToList();

            return orderFoods;
        }
    }
}
