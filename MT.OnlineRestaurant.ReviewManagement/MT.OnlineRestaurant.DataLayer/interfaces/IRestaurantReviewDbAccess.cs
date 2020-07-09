using MT.OnlineRestaurant.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MT.OnlineRestaurant.DataLayer.interfaces
{
    public interface IRestaurantReviewDbAccess
    {
        int InsertRestuarantReview(TblRestaurantReview restuarantReview);

        void UpdateRestuarantReview(TblRestaurantReview restuarantReview);

        IQueryable<TblRestaurantReview> GetRestuarantReviewByRestuarantId(int restuarantId);

    }
}
