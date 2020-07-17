#region References
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using MT.OnlineRestaurant.BusinessEntities;
using MT.OnlineRestaurant.BusinessLayer.interfaces;
using MT.OnlineRestaurant.OrderAPI.MessageManagement;
using MT.OnlineRestaurant.OrderAPI.ModelValidators;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
#endregion

namespace MT.OnlineRestaurant.OrderAPI.Controllers
{
    /// <summary>
    /// Payment controller
    /// </summary>
    [Produces("application/json")]
    public class PaymentController : Controller
    {
        private readonly IPaymentActions _paymentActions;
        private readonly IServiceBusTopicSender _serviceBusTopicSender;
        private readonly IServiceBusOutOfStockReceiver _serviceBusOutofStockReviever;
        /// <summary>
        /// Inject buisiness layer dependency
        /// </summary>
        /// <param name="paymentActions"></param>
        public PaymentController(IPaymentActions paymentActions, IServiceBusTopicSender serviceBusTopicSender, IServiceBusOutOfStockReceiver serviceBusOutOfStockReceiver)
        {
            _paymentActions = paymentActions;
            _serviceBusTopicSender = serviceBusTopicSender;
            _serviceBusOutofStockReviever = serviceBusOutOfStockReceiver;
        }

        /// <summary>
        /// Make payments for orders
        /// </summary>
        /// <param name="paymentEntity">Payment details</param>
        /// <returns>Payment status</returns>
        [HttpPost]
        [Route("api/MakePayment")]
        public async Task<IActionResult> MakePayment(PaymentEntity paymentEntity)
        {
            _serviceBusOutofStockReviever.RegisterOnMessageHandlerAndReceiveMessages();

            Thread.Sleep(5000);
         
            var items = _paymentActions.CheckIfOrderOutOfStock(paymentEntity.OrderId);
            if (items == null || items.Count == 0)
            {
                PaymentEntityValidator paymentEntityValidator = new PaymentEntityValidator();
                ValidationResult validationResult = paymentEntityValidator.Validate(paymentEntity);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.ToString("; "));
                }
                else
                {
                    var result = _paymentActions.MakePaymentForOrder(paymentEntity);
                    if (result == 0)
                    {
                        return BadRequest("Payment failed, Please try again later");
                    }
                    List<StockInformation> stock = new List<StockInformation>();
                    stock = _paymentActions.GetOrderDetails(paymentEntity.OrderId);
                    await _serviceBusTopicSender.SendMessage(stock);
                }
                return Ok("Payment is successful");
            }
            else
            {
                return BadRequest("Few Items are Out Of Stock");
            }
        }

        /// <summary>
        /// Update payment status after retrieving status from payment gateway plugin
        /// </summary>
        /// <param name="paymentEntity">Payment details</param>
        /// <returns>Updated payment status and order status</returns>
        [HttpPut]
        [Route("api/UpdatePaymentAndOrderStatus")]
        public IActionResult UpdatePaymentAndOrderStatus(UpdatePaymentEntity paymentEntity)
        {
            UpdatePaymentEntityValidator paymentEntityValidator = new UpdatePaymentEntityValidator();
            ValidationResult validationResult = paymentEntityValidator.Validate(paymentEntity);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ToString("; "));
            }
            else
            {
                var result = _paymentActions.UpdatePaymentAndOrderStatus(paymentEntity);
                if (result == 0)
                {
                    return BadRequest("Failed to update payment status, Please try again later");
                }
            }
            return Ok("Payment status updated successfully");
        }
    }
}