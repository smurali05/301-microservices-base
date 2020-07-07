using System;
using System.Collections.Generic;
using System.Text;

namespace MT.OnlineRestaurant.BusinessEntities
{
    public class StockInformation
    {
        public int OrderId { get; set; }

        public int? MenuId { get; set; }

        public int Quantity { get; set; }
    }
}
