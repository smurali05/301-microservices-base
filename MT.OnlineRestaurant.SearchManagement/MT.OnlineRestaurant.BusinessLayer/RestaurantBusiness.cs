using MT.OnlineRestaurant.BusinessEntities;
using System;
using System.Collections.Generic;
using MT.OnlineRestaurant.DataLayer.Repository;
using MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel;
using MT.OnlineRestaurant.DataLayer.DataEntity;
using System.Text;
using System.Linq;
using AutoMapper;
using MT.OnlineRestaurant.BusinessLayer.AzureBusServices;

namespace MT.OnlineRestaurant.BusinessLayer
{
    public class RestaurantBusiness : IRestaurantBusiness
    {
        ISearchRepository search_Repository;
        private readonly string connectionstring;
        private readonly IMapper _mapper;
        private readonly IServiceBusSenderOutOfStock _serviceBusSenderOutOfStock;
        public RestaurantBusiness(ISearchRepository _searchRepository, IMapper mapper, IServiceBusSenderOutOfStock serviceBusSenderOutOfStock)
        {
            search_Repository = _searchRepository;
            _mapper = mapper;
            _serviceBusSenderOutOfStock = serviceBusSenderOutOfStock;
        }

        public IQueryable<RestaurantMenu> GetRestaurantMenus(int restaurantID)
        {
            IQueryable<MenuDetails> menuDetails;
            List<RestaurantMenu> restaurant_Menu = new List<RestaurantMenu>();
            try
            {
                menuDetails = search_Repository.GetRestaurantMenu(restaurantID);
                foreach (var menu in menuDetails)
                {
                    RestaurantMenu menuInfo = new RestaurantMenu
                    {
                        menu_ID = menu.tbl_Offer.Id,
                        dish_Name = menu.tbl_Menu.Item,
                        price = menu.tbl_Offer.Price,
                        running_Offer = menu.tbl_Offer.Discount,
                        cuisine = menu.tbl_Cuisine.Cuisine
                    };
                    restaurant_Menu.Add(menuInfo);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return restaurant_Menu.AsQueryable();
        }

        public IQueryable<RestaurantRating> GetRestaurantRating(int restaurantID)
        {
            try
            {
                List<RestaurantRating> restaurantRatings = new List<RestaurantRating>();
                IQueryable<TblRating> rating;
                rating = search_Repository.GetRestaurantRating(restaurantID);
                foreach (var item in rating)
                {
                    RestaurantRating ratings = new RestaurantRating
                    {
                        rating = item.Rating,
                        RestaurantId = item.TblRestaurantId,
                        user_Comments = item.Comments,
                        customerId = item.TblCustomerId,
                    };
                    restaurantRatings.Add(ratings);
                }
                return restaurantRatings.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RestaurantInformation GetResturantDetails(int restaurantID)
        {
            try
            {
                TblRestaurant restaurant = new TblRestaurant();
                restaurant = search_Repository.GetResturantDetails(restaurantID);
                RestaurantInformation resturant_Information = new RestaurantInformation
                {
                    restaurant_ID = restaurant.Id,
                    restaurant_Name = restaurant.Name,
                    restaurant_Address = restaurant.Address,
                    restaurant_ContactNo = restaurant.ContactNo,
                    closing_Time = restaurant.CloseTime,
                    opening_Time = restaurant.OpeningTime,
                    website = restaurant.Website,
                    xaxis = (double)restaurant.TblLocation.X,
                    yaxis = (double)restaurant.TblLocation.Y
                };
                return resturant_Information;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<RestaurantTables> GetTableDetails(int restaurantID)
        {
            try
            {
                List<RestaurantTables> TableInfo = new List<RestaurantTables>();
                IQueryable<TblRestaurantDetails> restaurantTableCount;
                restaurantTableCount = search_Repository.GetTableDetails(restaurantID);
                foreach (var item in restaurantTableCount)
                {
                    RestaurantTables table = new RestaurantTables
                    {
                        restaurant_Name = item.TblRestaurant.Name,
                        table_Capacity = item.TableCapacity,
                        total_Count = item.TableCount
                    };
                    TableInfo.Add(table);
                }
                return TableInfo.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<RestaurantInformation> SearchRestaurantByLocation(BusinessEntities.LocationDetails locationDetails)
        {
            try
            {
                List<RestaurantInformation> restaurant_Info = new List<RestaurantInformation>();
                IQueryable<RestaurantSearchDetails> searched_Restaurant;
                DataLayer.DataEntity.LocationDetails location_Details = new DataLayer.DataEntity.LocationDetails
                {
                    distance = locationDetails.distance,
                    restaurant_Name = locationDetails.restaurant_Name,
                    xaxis = locationDetails.xaxis,
                    yaxis = locationDetails.yaxis
                };

                searched_Restaurant = search_Repository.GetRestaurantsBasedOnLocation(location_Details);
                foreach (var restaurants in searched_Restaurant)
                {
                    RestaurantInformation restaurant_Details = new RestaurantInformation
                    {
                        restaurant_ID = restaurants.restaurant_ID,
                        restaurant_Name = restaurants.restaurant_Name,
                        restaurant_Address = restaurants.restaurant_Address,
                        restaurant_ContactNo = restaurants.restaurant_PhoneNumber,
                        closing_Time = restaurants.closing_Time,
                        opening_Time = restaurants.opening_Time,
                        website = restaurants.restraurant_Website,
                        xaxis = restaurants.xaxis,
                        yaxis = restaurants.yaxis
                    };
                    restaurant_Info.Add(restaurant_Details);
                }
                return restaurant_Info.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<RestaurantInformation> GetRestaurantsBasedOnMenu(AdditionalFeatureForSearch additionalFeatureForSearch)
        {
            try
            {
                List<RestaurantInformation> restaurant_Info = new List<RestaurantInformation>();
                IQueryable<RestaurantSearchDetails> searched_Restaurant;
                DataLayer.DataEntity.AddtitionalFeatureForSearch searchCritera = new DataLayer.DataEntity.AddtitionalFeatureForSearch
                {
                    cuisine = (string.IsNullOrEmpty(additionalFeatureForSearch.cuisine) ? "" : additionalFeatureForSearch.cuisine),
                    Menu = (string.IsNullOrEmpty(additionalFeatureForSearch.Menu) ? "" : additionalFeatureForSearch.Menu),
                    rating = (additionalFeatureForSearch.rating != null && additionalFeatureForSearch.rating > 0) ? additionalFeatureForSearch.rating : 0
                };

                searched_Restaurant = search_Repository.GetRestaurantsBasedOnMenu(searchCritera);
                if (searched_Restaurant != null)
                {
                    foreach (var restaurants in searched_Restaurant)
                    {
                        RestaurantInformation restaurant_Details = new RestaurantInformation
                        {
                            restaurant_ID = restaurants.restaurant_ID,
                            restaurant_Name = restaurants.restaurant_Name,
                            restaurant_Address = restaurants.restaurant_Address,
                            restaurant_ContactNo = restaurants.restaurant_PhoneNumber,
                            closing_Time = restaurants.closing_Time,
                            opening_Time = restaurants.opening_Time,
                            website = restaurants.restraurant_Website,
                            xaxis = restaurants.xaxis,
                            yaxis = restaurants.yaxis
                        };
                        restaurant_Info.Add(restaurant_Details);
                    }
                }
                return restaurant_Info.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<RestaurantInformation> SearchForRestaurant(SearchForRestaurant searchDetails)
        {
            try
            {
                List<RestaurantInformation> RestaurantInfo = new List<RestaurantInformation>(0);

                // SearchForRestautrant searchInfo = new SearchForRestautrant();
                var searchinfo = _mapper.Map<SearchForRestautrant>(searchDetails);

                var Restaurant = search_Repository.MultiParameterSearchForRestaurant(searchinfo).ToList();
                foreach (var item in Restaurant)
                {
                    var RestaurantInformation = _mapper.Map<RestaurantInformation>(item);
                    RestaurantInfo.Add(RestaurantInformation);
                }

                return RestaurantInfo.AsQueryable();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Recording the customer rating the restaurants
        /// </summary>
        /// <param name=""></param>
        public void RestaurantRating(RestaurantRating restaurantRating)
        {
            if (restaurantRating != null)
            {
                TblRating rating = new TblRating()
                {
                    Rating = restaurantRating.rating,
                    TblRestaurantId = restaurantRating.RestaurantId,
                    Comments = restaurantRating.user_Comments,
                    TblCustomerId = restaurantRating.customerId
                };

                search_Repository.RestaurantRating(rating);
            }
        }
        public int ItemInStock(int restaurantID, int menuID)
        {
            RestaurantMenu menuObj = new RestaurantMenu();
            TblMenu menu = search_Repository.ItemInStock(restaurantID, menuID);
            menuObj.quantity = menu.quantity;
            return menuObj.quantity;
        }

        public List<Menu> UpdateStockCount(List<StockInformation> stocks)
        {
            List<Menu> items = new List<Menu>();
            List<TblMenu> menus = search_Repository.UpdatestockCount(stocks);
            foreach (var menu in menus)
            {
                StockInformation stock = new StockInformation();
                stock.MenuId = menu.Id;
                stock.OrderId = stocks.FirstOrDefault().OrderId;
                stock.Quantity = menu.quantity;
                if (stock.Quantity == 0)
                {
                    _serviceBusSenderOutOfStock.SendMessage(stock);
                }
                var item = _mapper.Map<Menu>(menu);
                items.Add(item);
            }
            return items;
        }

        public int UpdateStockPrice(StockPrice stock)
        {
            int result = search_Repository.UpdateStockPrice(stock);
            return result;
        }
    }
}
