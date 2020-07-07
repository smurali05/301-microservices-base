using MT.OnlineRestaurant.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MT.OnlineRestaurant.OrderAPI.MessageManagement
{
    public interface IServiceBusTopicSender
    {
        Task SendMessage(List<StockInformation> stock);
    }
}
