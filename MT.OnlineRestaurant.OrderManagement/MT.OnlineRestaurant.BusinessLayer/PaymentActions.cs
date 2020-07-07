using MT.OnlineRestaurant.BusinessEntities;
using MT.OnlineRestaurant.BusinessEntities.Enums;
using MT.OnlineRestaurant.BusinessLayer.interfaces;
using MT.OnlineRestaurant.DataLayer.interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace MT.OnlineRestaurant.BusinessLayer
{
    public class PaymentActions : IPaymentActions
    {
        private readonly IPaymentDbAccess _paymentDbAccess;
        private readonly IMapper _mapper;

        public PaymentActions(IPaymentDbAccess paymentDbAccess, IMapper mapper)
        {
            _paymentDbAccess = paymentDbAccess;
            _mapper = mapper;
        }

        public int MakePaymentForOrder(PaymentEntity orderPaymentDetails)
        {
            return _paymentDbAccess.MakePaymentForOrder(new DataLayer.Context.TblOrderPayment()
            {
                TblFoodOrderId = orderPaymentDetails.OrderId,
                TblPaymentTypeId = orderPaymentDetails.PaymentTypeId,
                Remarks = orderPaymentDetails.Remarks,
                TblCustomerId = orderPaymentDetails.CustomerId,
                TblPaymentStatusId = (int)Status.Initiated,
                RecordTimeStampCreated = DateTime.Now
            });
        }

        public int UpdatePaymentAndOrderStatus(UpdatePaymentEntity orderPaymentDetails)
        {
            return _paymentDbAccess.UpdatePaymentAndOrderStatus(new DataLayer.Context.TblOrderPayment()
            {
                Id = orderPaymentDetails.PaymentId,
                TransactionId = orderPaymentDetails.TransactionReferenceNo,
                TblPaymentStatusId = orderPaymentDetails.PaymentStatusId
            });
        }

        public List<FoodOrderMapping> CheckIfOrderOutOfStock(int OrderId)
        {

            var items = _paymentDbAccess.CheckIfOrderOutOfStock(OrderId);
            List<FoodOrderMapping> foodOrders = new List<FoodOrderMapping>();
            if (items != null)
            {
                foreach (var item in items)
                {
                    var Orders = _mapper.Map<FoodOrderMapping>(item);
                    foodOrders.Add(Orders);
                }

                return foodOrders;
            }
            return null;
        }

        public List<StockInformation> GetOrderDetails(int OrderId)
        {
            return _paymentDbAccess.GetOrderDetails(OrderId);
        }
    }
}
