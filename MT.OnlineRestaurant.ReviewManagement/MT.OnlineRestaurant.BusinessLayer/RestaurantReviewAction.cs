using AutoMapper;
using MT.OnlineRestaurant.BusinessEntities;
using MT.OnlineRestaurant.BusinessLayer.interfaces;
using MT.OnlineRestaurant.DataLayer.Context;
using MT.OnlineRestaurant.DataLayer.interfaces;
using System;
using System.Collections.Generic;

namespace MT.OnlineRestaurant.BusinessLayer
{
    public class RestaurantReviewAction : IRestaurantReviewAction
    {
        private readonly IRestaurantReviewDbAccess _restaurantReviewDbAccess;
        private readonly IMapper _mapper;

        public RestaurantReviewAction(IRestaurantReviewDbAccess restaurantReviewDbAccess, IMapper mapper)
        {
            _restaurantReviewDbAccess = restaurantReviewDbAccess;
            _mapper = mapper;
        }

        public int AddRestuarantReview(RestaurantReviewEntity restaurantReviewEntity)
        {
            var tblRestaurantReview = _mapper.Map<TblRestaurantReview>(restaurantReviewEntity);

            return _restaurantReviewDbAccess.InsertRestuarantReview(tblRestaurantReview);
        }

        public void UpdateRestuarantReview(RestaurantReviewEntity restaurantReviewEntity)
        {
            var tblRestaurantReview = _mapper.Map<TblRestaurantReview>(restaurantReviewEntity);

            _restaurantReviewDbAccess.UpdateRestuarantReview(tblRestaurantReview);
        }

        public List<RestaurantReviewEntity> GetRestuarantReviewByRestuarantId(int restaurantId)
        {
            var items = _restaurantReviewDbAccess.GetRestuarantReviewByRestuarantId(restaurantId);
            List<RestaurantReviewEntity> restaurantReviews = new List<RestaurantReviewEntity>();
            if (items != null)
            {
                foreach (var item in items)
                {
                    var review = _mapper.Map<RestaurantReviewEntity>(item);
                    restaurantReviews.Add(review);
                }

                return restaurantReviews;
            }
            return null;
        }
    }
}
