using Microsoft.EntityFrameworkCore;
using MT.OnlineRestaurant.DataLayer.Context;
using MT.OnlineRestaurant.DataLayer.interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using MT.OnlineRestaurant.BusinessEntities;

namespace MT.OnlineRestaurant.DataLayer
{
    public class PaymentDbAccess : IPaymentDbAccess
    {
        private readonly OrderManagementContext _context;

        public PaymentDbAccess(OrderManagementContext context)
        {
            _context = context;
        }

        public int MakePaymentForOrder(TblOrderPayment orderPaymentDetails)
        {
            _context.TblOrderPayment.Add(orderPaymentDetails);
            _context.SaveChanges();
            return orderPaymentDetails.Id;
        }

        public int UpdatePaymentAndOrderStatus(TblOrderPayment orderPaymentDetails)
        {
            var ID = new SqlParameter
            {
                ParameterName = "@ID",
                DbType = System.Data.DbType.Int32,
                Value = orderPaymentDetails.Id,
                Direction = System.Data.ParameterDirection.Input
            };
            var TransactionID = new SqlParameter
            {
                ParameterName = "@TransactionID",
                DbType = System.Data.DbType.String,
                Size = 20,
                Value = orderPaymentDetails.TransactionId,
                Direction = System.Data.ParameterDirection.Input
            };
            var tblPaymentStatusID = new SqlParameter
            {
                ParameterName = "@tblPaymentStatusID",
                DbType = System.Data.DbType.Int32,
                Value = orderPaymentDetails.TblPaymentStatusId,
                Direction = System.Data.ParameterDirection.Input
            };
            var ReturnValue = new SqlParameter
            {
                ParameterName = "@ReturnValue",
                DbType = System.Data.DbType.Int32,
                Direction = System.Data.ParameterDirection.Output
            };

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("exec UpdatePaymentStatus @ID, @TransactionID, ");
            stringBuilder.Append("@tblPaymentStatusID, @ReturnValue OUT");

            _context.Database.ExecuteSqlCommand(stringBuilder.ToString(), 
                ID,
                TransactionID,
                tblPaymentStatusID,
                ReturnValue);

            _context.SaveChanges();
            return (int)ReturnValue.Value;
        }

        public List<TblFoodOrderMapping> CheckIfOrderOutOfStock(int OrderId)
        {
            List<TblFoodOrderMapping> itemsOutOfStock = new List<TblFoodOrderMapping>();

            try
            {
                var FoodOrder = _context.TblFoodOrderMapping.Where(p => p.TblFoodOrderId == OrderId);

                if (FoodOrder.Any())
                {
                    foreach (var item in FoodOrder)
                    {
                        if (item.IsItemOutOfStock == true)
                        {
                            itemsOutOfStock.Add(item);
                        }
                    }
                    return itemsOutOfStock;
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<StockInformation> GetOrderDetails(int OrderId)
        {
            List<StockInformation> stockItems = new List<StockInformation>();
            while (_context != null)
            {
                var OrderMapping = _context.TblFoodOrderMapping.Where(p => p.TblFoodOrderId == OrderId);
                foreach (var order in OrderMapping)
                {
                    StockInformation stock = new StockInformation()
                    {
                        OrderId = OrderId,
                        MenuId = order.TblMenuId,
                        Quantity = order.Quantity,
                    };
                    stockItems.Add(stock);
                }
                return stockItems;
            }

            return null;
        }

        public int UpdatePaymentDone(int OrderId, int PaymentTypeId)
        {
            var FoodOrder = _context.TblFoodOrder.Where(p => p.Id == OrderId).FirstOrDefault();
            FoodOrder.TblPaymentTypeId = PaymentTypeId;
            FoodOrder.TblOrderStatusId = 2;
            _context.TblFoodOrder.Update(FoodOrder);
            return _context.SaveChanges();
        }
    }
}
