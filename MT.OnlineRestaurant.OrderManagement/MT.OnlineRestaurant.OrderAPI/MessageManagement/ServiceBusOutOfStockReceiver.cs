﻿using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MT.OnlineRestaurant.BusinessEntities;
using MT.OnlineRestaurant.BusinessLayer.interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MT.OnlineRestaurant.OrderAPI.MessageManagement
{
    public class ServiceBusOutOfStockReceiver:IServiceBusOutOfStockReceiver
    {
        private readonly IPlaceOrderActions _processData;
        private readonly IConfiguration _configuration;
        private readonly SubscriptionClient _subscriptionClient;
        private const string TOPIC_PATH = "itemoutofstock";
        private const string SUBSCRIPTION_NAME = "ItemOutOfStock";
        private readonly ILogger _logger;

        public ServiceBusOutOfStockReceiver(IPlaceOrderActions processData,
            IConfiguration configuration,
            ILogger<ServiceBusTopicReceiver> logger)
        {
            _processData = processData;
            _configuration = configuration;
            _logger = logger;

            _subscriptionClient = new SubscriptionClient(
                _configuration.GetConnectionString("ServiceBusConnectionString"),
                TOPIC_PATH,
                SUBSCRIPTION_NAME);
        }

        public void RegisterOnMessageHandlerAndReceiveMessages()
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };
            _subscriptionClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);

        }

        private async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {

            var stocks = JsonConvert.DeserializeObject<StockInformation>(Encoding.UTF8.GetString(message.Body));
            _processData.UpdateOutOfStock(stocks);
            await _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            _logger.LogError(exceptionReceivedEventArgs.Exception, "Message handler encountered an exception");
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;

            _logger.LogDebug($"- Endpoint: {context.Endpoint}");
            _logger.LogDebug($"- Entity Path: {context.EntityPath}");
            _logger.LogDebug($"- Executing Action: {context.Action}");

            return Task.CompletedTask;
        }

        public async Task CloseSubscriptionClientAsync()
        {
            await _subscriptionClient.CloseAsync();
        }
    }
}
