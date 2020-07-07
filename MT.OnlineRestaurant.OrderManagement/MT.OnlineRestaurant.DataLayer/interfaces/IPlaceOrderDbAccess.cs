using MT.OnlineRestaurant.BusinessEntities;
using MT.OnlineRestaurant.BusinessEntities.ServiceModels;
using MT.OnlineRestaurant.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MT.OnlineRestaurant.DataLayer.interfaces
{
    public interface IPlaceOrderDbAccess
    {
        int PlaceOrder(TblFoodOrder foodOrderDetails);

        int CancelOrder(int orderId);

        /// <summary>
        /// gets the customer placed order details
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        IQueryable<TblFoodOrder> GetReports(int customerId);

        IQueryable<TblFoodOrderMapping> UpdateStockPrice(StockPrice stocks);

        IQueryable<TblFoodOrderMapping> UpdateOutOfStock(StockInformation stock);
    }
}
