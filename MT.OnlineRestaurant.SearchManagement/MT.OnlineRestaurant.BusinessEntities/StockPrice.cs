using System;
using System.Collections.Generic;
using System.Text;

namespace MT.OnlineRestaurant.BusinessEntities
{
    public class StockPrice
    {
        public int MenuId { get; set; }
        public decimal ChangedPrice { get; set; }

        public int RestuarantId { get; set; }
    }
}
