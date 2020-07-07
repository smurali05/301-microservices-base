using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MT.OnlineRestaurant.BusinessLayer.AzureBusServices
{
    public interface IServiceBusTopicReceiver
    {
        void RegisterOnMessageHandlerAndReceiveMessages();

        Task CloseSubscriptionClientAsync();
    }
}
