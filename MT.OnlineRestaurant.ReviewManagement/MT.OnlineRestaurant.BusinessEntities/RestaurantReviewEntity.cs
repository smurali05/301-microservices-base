using System;
using System.Collections.Generic;
using System.Text;

namespace MT.OnlineRestaurant.BusinessEntities
{
    public class RestaurantReviewEntity
    {
        public int ReviewId { get; set; }
        public int RestaurantId { get; set; }
        public string Comments { get; set; }
        public string Rating { get; set; }
        public int CustomerId { get; set; }
    }
}
