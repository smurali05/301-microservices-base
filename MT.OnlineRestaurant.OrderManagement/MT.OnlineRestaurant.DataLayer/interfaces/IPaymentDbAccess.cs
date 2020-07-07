using MT.OnlineRestaurant.BusinessEntities;
using MT.OnlineRestaurant.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace MT.OnlineRestaurant.DataLayer.interfaces
{
    public interface IPaymentDbAccess
    {
        int MakePaymentForOrder(TblOrderPayment orderPaymentDetails);
        int UpdatePaymentAndOrderStatus(TblOrderPayment orderPaymentDetails);

        List<TblFoodOrderMapping> CheckIfOrderOutOfStock(int OrderId);
        int UpdatePaymentDone(int OrderId, int PaymentTypeId);
        List<StockInformation> GetOrderDetails(int OrderId);
    }
}
