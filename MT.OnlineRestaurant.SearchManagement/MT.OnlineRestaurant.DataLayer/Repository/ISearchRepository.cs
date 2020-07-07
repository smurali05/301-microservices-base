using MT.OnlineRestaurant.DataLayer.DataEntity;
using MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel;
using System.Collections.Generic;
using System.Linq;
using MT.OnlineRestaurant.BusinessEntities;

namespace MT.OnlineRestaurant.DataLayer.Repository
{
    public interface ISearchRepository
    {
        TblRestaurant GetResturantDetails(int restaurantID);
        IQueryable<TblRating> GetRestaurantRating(int restaurantID);

        IQueryable<MenuDetails> GetRestaurantMenu(int restaurantID);

        IQueryable<TblRestaurantDetails> GetTableDetails(int restaurantID);
        IQueryable<RestaurantSearchDetails> GetRestaurantsBasedOnLocation(MT.OnlineRestaurant.DataLayer.DataEntity.LocationDetails location_Details);
        IQueryable<RestaurantSearchDetails> GetRestaurantsBasedOnMenu(AddtitionalFeatureForSearch searchDetails);
        IQueryable<RestaurantSearchDetails> SearchForRestaurant(SearchForRestautrant searchDetails);
        IQueryable<RestaurantSearchDetails> MultiParameterSearchForRestaurant(SearchForRestautrant searchDetails);

        /// <summary>
        /// Recording the customer rating the restaurants
        /// </summary>
        /// <param name="tblRating"></param>
        void RestaurantRating(TblRating tblRating);
        TblMenu ItemInStock(int restaurantID,int MenuID);

        int UpdateStockPrice(StockPrice stock);

        /// <summary>
        /// Recording the customer rating the restaurants
        /// </summary>
        /// <param name="tblRating"></param>
        List<TblMenu> UpdatestockCount(List<StockInformation> stocks);

    }
}
