using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MT.OnlineRestaurant.OrderAPI.MessageManagement
{
    public interface IServiceBusTopicReceiver
    {
        void RegisterOnMessageHandlerAndReceiveMessages();

        Task CloseSubscriptionClientAsync();
    }
}
