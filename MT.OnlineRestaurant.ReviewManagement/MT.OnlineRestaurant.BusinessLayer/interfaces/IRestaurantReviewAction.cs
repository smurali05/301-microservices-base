using MT.OnlineRestaurant.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MT.OnlineRestaurant.BusinessLayer.interfaces
{
    public interface IRestaurantReviewAction
    {
        int AddRestuarantReview(RestaurantReviewEntity restaurantReviewEntity);
        void UpdateRestuarantReview(RestaurantReviewEntity restaurantReviewEntity);
        List<RestaurantReviewEntity> GetRestuarantReviewByRestuarantId(int restaurantId);

    }
}
