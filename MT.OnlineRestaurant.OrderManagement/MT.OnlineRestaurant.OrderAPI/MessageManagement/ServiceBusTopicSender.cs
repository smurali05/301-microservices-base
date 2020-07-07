using Microsoft.Azure.ServiceBus;
using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;
using MT.OnlineRestaurant.BusinessEntities;

namespace MT.OnlineRestaurant.OrderAPI.MessageManagement
{
    public class ServiceBusTopicSender:IServiceBusTopicSender
    {
        private readonly TopicClient _topicClient;
        private readonly IConfiguration _configuration;
        private const string UPDATESTOCK_TOPIC_PATH = "updatestockcount";
        private readonly ILogger _logger;

        public ServiceBusTopicSender(IConfiguration configuration,
            ILogger<ServiceBusTopicSender> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _topicClient = new TopicClient(
                _configuration.GetConnectionString("ServiceBusConnectionString"),
                UPDATESTOCK_TOPIC_PATH
            );
        }

        public async Task SendMessage(List<StockInformation> stocks)
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
