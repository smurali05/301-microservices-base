using Microsoft.Azure.ServiceBus;
using MT.OnlineRestaurant.BusinessEntities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MT.OnlineRestuarant.Messaging
{
    public class SendAsync
    {
        const string ServiceBusConnectionString = "Endpoint=sb://capstone-servicebus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=+j/6wYFsXwlIIbrP2dZAZVlNK6gjQgV8SXzVJC1WCGc=";
        const string TopicName = "itemoutofstock";
        static ITopicClient topicClient;
        public async Task SendMessagesAsync(OrderEntity orderEntity)
        {
            topicClient = new TopicClient(ServiceBusConnectionString, TopicName);
            try
            {
                string messageBody = JsonConvert.SerializeObject(orderEntity);
                Message message = new Message(Encoding.UTF8.GetBytes(messageBody));
                await topicClient.SendAsync(message);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
            }
        }

    }
}
