using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MT.OnlineRestaurant.BusinessEntities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.OnlineRestaurant.BusinessLayer.AzureBusServices
{
    public class ServiceBusSenderOutOfStock : IServiceBusSenderOutOfStock
    {
        private readonly TopicClient _topicClient;
        private readonly IConfiguration _configuration;
        private const string ITEM_OUT_OF_STOCKPRICE_TOPIC_PATH = "itemoutofstock";
        private readonly ILogger _logger;

        public ServiceBusSenderOutOfStock(IConfiguration configuration,
            ILogger<ServiceBusTopicSender> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _topicClient = new TopicClient(
                _configuration.GetConnectionString("ServiceBusConnectionString"),
                ITEM_OUT_OF_STOCKPRICE_TOPIC_PATH
            );
        }

        public async Task SendMessage(StockInformation stocks)
        {
            string data = JsonConvert.SerializeObject(stocks);
            Message message = new Message(Encoding.UTF8.GetBytes(data));

            try
            {
                await _topicClient.SendAsync(message);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }
    }
}
