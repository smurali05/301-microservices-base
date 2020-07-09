using MT.OnlineRestaurant.BusinessEntities;
using MT.OnlineRestaurant.DataLayer.Context;
using MT.OnlineRestaurant.DataLayer.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MT.OnlineRestaurant.DataLayer
{
    public class RestaurantReviewDbAccess : IRestaurantReviewDbAccess
    {
        private readonly RestaurantManagementContext _context;

        public RestaurantReviewDbAccess(RestaurantManagementContext context)
        {
            _context = context;
        }

        public int InsertRestuarantReview(TblRestaurantReview restuarantReview)
        {
            _context.TblRestaurantReview.Add(restuarantReview);
            _context.SaveChanges();
            return restuarantReview.TblReviewId;
        }

        public void UpdateRestuarantReview(TblRestaurantReview restuarantReview)
        {
            var tblRestuarantReview = _context.TblRestaurantReview.Where(fo => fo.TblReviewId == restuarantReview.TblReviewId).FirstOrDefault();

            if (tblRestuarantReview != null)
            {
                tblRestuarantReview.TblUserComments = restuarantReview.TblUserComments;
                tblRestuarantReview.TblRating = restuarantReview.TblRating;

                _context.Update(tblRestuarantReview);
                _context.SaveChanges();
            }
        }

        public IQueryable<TblRestaurantReview> GetRestuarantReviewByRestuarantId(int restuarantId)
        {
            return _context.TblRestaurantReview.Where(fo => fo.TblRestaurantId == restuarantId);
        }
    }
}
