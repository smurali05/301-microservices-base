using MT.OnlineRestaurant.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MT.OnlineRestaurant.BusinessLayer.AzureBusServices
{
    public interface IServiceBusSenderOutOfStock
    {
        Task SendMessage(StockInformation stock);
    }
}
