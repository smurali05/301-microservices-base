using System;
using System.Collections.Generic;
using System.Text;

namespace MT.OnlineRestaurant.DataLayer.Context
{
    public class TblRestaurantReview
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int TblReviewId { get; set; }
        public int TblRestaurantId { get; set; }
        public string TblUserComments { get; set; }
        public string TblRating { get; set; }
        public int TblCustomerId { get; set; }
    }
}
