using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using MoreLinq;
using MT.OnlineRestaurant.DataLayer.DataEntity;
using MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessEntities = MT.OnlineRestaurant.BusinessEntities;

namespace MT.OnlineRestaurant.DataLayer.Repository
{
    public class SearchRepository : ISearchRepository
    {
        private RestaurantManagementContext db;
        public SearchRepository(RestaurantManagementContext connection)
        {
            db = connection;
        }

        #region Interface Methods
        public IQueryable<MenuDetails> GetRestaurantMenu(int restaurantID)
        {
            List<MenuDetails> menudetails = new List<MenuDetails>();
            try
            {
                if (db != null)
                {
                    var menudetail = (from offer in db.TblOffer
                                      join menu in db.TblMenu
                                      on offer.TblMenuId equals menu.Id into TableMenu
                                      from menu in TableMenu.ToList()
                                      join cuisine in db.TblCuisine on menu.TblCuisineId equals cuisine.Id
                                      where offer.TblRestaurantId == restaurantID
                                      select new MenuDetails
                                      {
                                          tbl_Offer = offer,
                                          tbl_Cuisine = cuisine,
                                          tbl_Menu = menu

                                      }).ToList();
                    foreach (var item in menudetail)
                    {
                        MenuDetails menuitem = new MenuDetails
                        {
                            tbl_Cuisine = item.tbl_Cuisine,
                            tbl_Menu = item.tbl_Menu,
                            tbl_Offer = item.tbl_Offer
                        };
                        menudetails.Add(menuitem);
                    }
                }
                return menudetails.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<TblRating> GetRestaurantRating(int restaurantID)
        {
            // List<TblRating> restaurant_Rating = new List<TblRating>();
            try
            {
                if (db != null)
                {
                    return (from rating in db.TblRating
                            join restaurant in db.TblRestaurant on
                            rating.TblRestaurantId equals restaurant.Id
                            where rating.TblRestaurantId == restaurantID
                            select new TblRating
                            {
                                Rating = rating.Rating,
                                Comments = rating.Comments,
                                TblRestaurant = restaurant,
                            }).AsQueryable();
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TblRestaurant GetResturantDetails(int restaurantID)
        {
            TblRestaurant resturantInformation = new TblRestaurant();

            try
            {
                if (db != null)
                {
                    resturantInformation = (from restaurant in db.TblRestaurant
                                            join location in db.TblLocation on restaurant.TblLocationId equals location.Id
                                            where restaurant.Id == restaurantID
                                            select new TblRestaurant
                                            {
                                                Id = restaurant.Id,
                                                Name = restaurant.Name,
                                                Address = restaurant.Address,
                                                ContactNo = restaurant.ContactNo,
                                                TblLocation = location,
                                                CloseTime = restaurant.CloseTime,
                                                OpeningTime = restaurant.OpeningTime,
                                                Website = restaurant.Website
                                            }).FirstOrDefault();

                }

                return resturantInformation;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public IQueryable<TblRestaurantDetails> GetTableDetails(int restaurantID)
        {
            try
            {
                if (db != null)
                {
                    return (from restaurantDetails in db.TblRestaurantDetails
                            join restaurant in db.TblRestaurant
                            on restaurantDetails.TblRestaurantId equals restaurant.Id
                            where restaurantDetails.TblRestaurantId == restaurantID
                            select new TblRestaurantDetails
                            {
                                TableCapacity = restaurantDetails.TableCapacity,
                                TableCount = restaurantDetails.TableCount,
                                TblRestaurant = restaurant
                            }).AsQueryable();

                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<RestaurantSearchDetails> GetRestaurantsBasedOnLocation(LocationDetails location_Details)
        {
            List<RestaurantSearchDetails> restaurants = new List<RestaurantSearchDetails>();
            try
            {
                restaurants = GetRetaurantBasedOnLocationAndName(location_Details);
                return restaurants.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public IQueryable<RestaurantSearchDetails> GetRestaurantsBasedOnMenu(AddtitionalFeatureForSearch searchDetails)
        {
            List<RestaurantSearchDetails> restaurants = new List<RestaurantSearchDetails>();
            try
            {
                restaurants = GetRestaurantDetailsBasedOnRating(searchDetails);
                return restaurants.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public IQueryable<RestaurantSearchDetails> SearchForRestaurant(SearchForRestautrant searchDetails)
        {
            List<RestaurantSearchDetails> searchedRestaurantBasedOnRating = new List<RestaurantSearchDetails>();
            searchedRestaurantBasedOnRating = GetRestaurantDetailsBasedOnRating(searchDetails.search);

            List<RestaurantSearchDetails> restaurantsBasedOnLocation = new List<RestaurantSearchDetails>();
            restaurantsBasedOnLocation = GetRetaurantBasedOnLocationAndName(searchDetails.location);

            List<RestaurantSearchDetails> restaurantInfo = new List<RestaurantSearchDetails>();
            restaurantInfo = restaurantsBasedOnLocation.Intersect(searchedRestaurantBasedOnRating).ToList<RestaurantSearchDetails>();

            return restaurantInfo.AsQueryable();
        }

        /// <summary>
        /// Multiparameter Search for restuarant
        /// </summary>
        /// <param name="searchDetails"></param>
        /// <returns>IQueryable<RestaurantSearchDetails></returns>
        public IQueryable<RestaurantSearchDetails> MultiParameterSearchForRestaurant(SearchForRestautrant searchDetails)
        {
            List<RestaurantSearchDetails> restaurants = new List<RestaurantSearchDetails>();
            try
            {
                var res = db.TblRestaurant.Include(x => x.TblRating);

                var restaurantFilter = (from restaurant in res
                                        join location in db.TblLocation on restaurant.TblLocationId equals location.Id
                                        select new { TblRestaurant = restaurant, TblLocation = location });


                if (searchDetails.search != null)
                {
                    if (!string.IsNullOrEmpty(searchDetails.search.cuisine))
                    {
                        restaurantFilter = (from filteredRestaurant in restaurantFilter
                                            join offer in db.TblOffer on filteredRestaurant.TblRestaurant.Id equals offer.TblRestaurantId
                                            join menu in db.TblMenu on offer.TblMenuId equals menu.Id
                                            join cuisine in db.TblCuisine on menu.TblCuisineId equals cuisine.Id
                                            where cuisine.Cuisine.Contains(searchDetails.search.cuisine)
                                            select filteredRestaurant).Distinct();
                    }
                    if (!string.IsNullOrEmpty(searchDetails.search.Menu))
                    {
                        restaurantFilter = (from filteredRestaurant in restaurantFilter
                                            join offer in db.TblOffer on filteredRestaurant.TblRestaurant.Id equals offer.TblRestaurantId
                                            join menu in db.TblMenu on offer.TblMenuId equals menu.Id
                                            where menu.Item.Contains(searchDetails.search.Menu)
                                            select filteredRestaurant).Distinct();
                    }

                    if (searchDetails.search.budget > 0)

                    {
                        restaurantFilter = (from filteredRestaurant in restaurantFilter
                                            join offer in db.TblOffer on filteredRestaurant.TblRestaurant.Id equals offer.TblRestaurantId
                                            join menu in db.TblMenu on offer.TblMenuId equals menu.Id
                                            where offer.Price > searchDetails.search.budget
                                            select filteredRestaurant).Distinct();
                    }
                    if (searchDetails.search.rating > 0)
                    {
                        restaurantFilter = (from filteredRestaurant in restaurantFilter
                                            join rating in db.TblRating on filteredRestaurant.TblRestaurant.Id equals rating.TblRestaurantId
                                            where rating.Rating.Contains(searchDetails.search.rating.ToString())
                                            select filteredRestaurant).Distinct();
                    }
                }
                if (searchDetails.location != null)
                {
                    if (!string.IsNullOrEmpty(searchDetails.location.restaurant_Name))
                    {
                        restaurantFilter = restaurantFilter.Where(a => a.TblRestaurant.Name.Contains(searchDetails.location.restaurant_Name));

                    }

                    if (!(searchDetails.location.xaxis <= 0) || (searchDetails.location.yaxis < 0))
                    {
                        if (restaurantFilter != null)
                        {
                            foreach (var place in restaurantFilter)
                            {
                                double distance = Distance(searchDetails.location.xaxis, searchDetails.location.yaxis, (double)place.TblLocation.X, (double)place.TblLocation.Y);
                                if (distance < int.Parse(searchDetails.location.distance.ToString()))
                                {
                                    RestaurantSearchDetails tblRestaurant = new RestaurantSearchDetails
                                    {
                                        restaurant_ID = place.TblRestaurant.Id,
                                        restaurant_Name = place.TblRestaurant.Name,
                                        restaurant_Address = place.TblRestaurant.Address,
                                        restaurant_PhoneNumber = place.TblRestaurant.ContactNo,
                                        restraurant_Website = place.TblRestaurant.Website,
                                        closing_Time = place.TblRestaurant.CloseTime,
                                        opening_Time = place.TblRestaurant.OpeningTime,
                                        xaxis = (double)place.TblLocation.X,
                                        yaxis = (double)place.TblLocation.Y
                                    };
                                    try
                                    {
                                        tblRestaurant.rating = place.TblRestaurant.TblRating.Average(x => Convert.ToDecimal(x.Rating));
                                    }
                                    catch
                                    {
                                        tblRestaurant.rating = 0;
                                    }
                                    restaurants.Add(tblRestaurant);
                                }
                            }
                        }

                    }
                    else
                    {
                        if (restaurantFilter != null)
                        {
                            foreach (var item in restaurantFilter)
                            {
                                RestaurantSearchDetails tblRestaurant = new RestaurantSearchDetails
                                {
                                    restaurant_ID = item.TblRestaurant.Id,
                                    restaurant_Name = item.TblRestaurant.Name,
                                    restaurant_Address = item.TblRestaurant.Address,
                                    restaurant_PhoneNumber = item.TblRestaurant.ContactNo,
                                    restraurant_Website = item.TblRestaurant.Website,
                                    closing_Time = item.TblRestaurant.CloseTime,
                                    opening_Time = item.TblRestaurant.OpeningTime,
                                    xaxis = (double)item.TblLocation.X,
                                    yaxis = (double)item.TblLocation.Y

                                };

                                try
                                {
                                    tblRestaurant.rating = item.TblRestaurant.TblRating.Average(x => Convert.ToDecimal(x.Rating));
                                }
                                catch
                                {
                                    tblRestaurant.rating = 0;
                                }
                                restaurants.Add(tblRestaurant);
                            }
                        }
                    }
                }
                
                return restaurants.DistinctBy(x => x.restaurant_Name).OrderByDescending(x => x.rating).AsQueryable();

            }
            catch (Exception ex)
            {
                return null;
            }
        }


        /// <summary>
        /// Recording the customer rating the restaurants
        /// </summary>
        /// <param name="tblRating"></param>
        public void RestaurantRating(TblRating tblRating)
        {
            //tblRating.UserCreated = ,
            //tblRating.UserModified=,
            tblRating.RecordTimeStampCreated = DateTime.Now;

            db.Set<TblRating>().Add(tblRating);
            db.SaveChanges();

        }
        public TblMenu ItemInStock(int restaurantID, int menuID)
        {
            try
            {
                TblMenu menuObj = new TblMenu();
                if (db != null)
                {
                    //    menuObj = (from m in db.TblMenu
                    //               join offer in db.TblOffer on m.Id equals offer.TblMenuId
                    //               join restaurant in db.TblRestaurantDetails on offer.TblRestaurantId equals restaurant.TblRestaurantId
                    //               where restaurant.TblRestaurantId == restaurantID && m.Id == menuID
                    //               select new TblMenu
                    //               {
                    //                   quantity = m.quantity
                    //               }).FirstOrDefault();                   
                    //}
                    menuObj = (from offer in db.TblOffer
                               join menu in db.TblMenu
                               on offer.TblMenuId equals menu.Id
                               join rest in db.TblRestaurantDetails
                               on offer.TblRestaurantId equals rest.TblRestaurantId
                               where rest.TblRestaurantId == restaurantID && menu.Id == menuID
                               select new TblMenu
                               {
                                   quantity = menu.quantity
                               }).FirstOrDefault();
                }
                return menuObj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TblMenu> UpdatestockCount(List<BusinessEntities.StockInformation> stocks)
        {
            List<TblMenu> tblMenus = new List<TblMenu>();

            var optionsBuilder = new DbContextOptionsBuilder<RestaurantManagementContext>();
            optionsBuilder.UseSqlServer("Server=tcp:301mcsmurali.database.windows.net,1433;Initial Catalog=RestaurantManagement;Persist Security Info=False;User ID=muralidb;Password=Welcome123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;",
               b => b.MigrationsAssembly("MT.OnlineRestaurant.DataLayer"));

            using (var context = new RestaurantManagementContext(optionsBuilder.Options))
            {
                foreach (var stock in stocks)
                {

                    //var menuObj = (from offer in db.TblOffer
                    //           join menu in db.TblMenu
                    //           on offer.TblMenuId equals menu.Id
                    //           where menu.Id == stock.MenuId
                    //           select new TblMenu
                    //           {
                    //               quantity = menu.quantity
                    //           }).FirstOrDefault();
                    var menuObj = db.TblMenu.Where(p => p.Id == stock.MenuId).FirstOrDefault();
                    menuObj.quantity = menuObj.quantity - stock.Quantity;
                    if (menuObj.quantity < 0)
                    {
                        menuObj.quantity = 0;
                    }
                    db.TblMenu.Update(menuObj);
                    db.SaveChanges();
                    tblMenus.Add(menuObj);
                }
            }
            
            return tblMenus;
        }


        public int UpdateStockPrice(BusinessEntities.StockPrice stock)
        {
            TblOffer OfferObj = new TblOffer();
            if (db != null)
            {
                OfferObj = db.TblOffer.Where(p => p.TblMenuId == stock.MenuId && p.TblRestaurantId ==stock.RestuarantId).FirstOrDefault();
                if (OfferObj != null)
                {
                    OfferObj.Price = stock.ChangedPrice;
                    db.TblOffer.Update(OfferObj);
                    return db.SaveChanges();
                }
            }
            return 0;
        }

        #endregion

        #region private methods
        private List<RestaurantSearchDetails> GetRestaurantDetailsBasedOnRating(AddtitionalFeatureForSearch searchList)
        {
            List<RestaurantSearchDetails> restaurants = new List<RestaurantSearchDetails>();
            try
            {
                var restaurantFilter = (from restaurant in db.TblRestaurant
                                        join location in db.TblLocation on restaurant.TblLocationId equals location.Id
                                        select new { TblRestaurant = restaurant, TblLocation = location });

                if (!string.IsNullOrEmpty(searchList.cuisine))
                {
                    restaurantFilter = (from filteredRestaurant in restaurantFilter
                                        join offer in db.TblOffer on filteredRestaurant.TblRestaurant.Id equals offer.TblRestaurantId
                                        join menu in db.TblMenu on offer.TblMenuId equals menu.Id
                                        join cuisine in db.TblCuisine on menu.TblCuisineId equals cuisine.Id
                                        where cuisine.Cuisine.Contains(searchList.cuisine)
                                        select filteredRestaurant).Distinct();
                }
                if (!string.IsNullOrEmpty(searchList.Menu))
                {
                    restaurantFilter = (from filteredRestaurant in restaurantFilter
                                        join offer in db.TblOffer on filteredRestaurant.TblRestaurant.Id equals offer.TblRestaurantId
                                        join menu in db.TblMenu on offer.TblMenuId equals menu.Id
                                        where menu.Item.Contains(searchList.Menu)
                                        select filteredRestaurant).Distinct();
                }

                if (searchList.rating > 0)
                {
                    restaurantFilter = (from filteredRestaurant in restaurantFilter
                                        join rating in db.TblRating on filteredRestaurant.TblRestaurant.Id equals rating.TblRestaurantId
                                        where rating.Rating.Contains(searchList.rating.ToString())
                                        select filteredRestaurant).Distinct();
                }
                foreach (var item in restaurantFilter)
                {
                    RestaurantSearchDetails restaurant = new RestaurantSearchDetails
                    {
                        restaurant_ID = item.TblRestaurant.Id,
                        restaurant_Name = item.TblRestaurant.Name,
                        restaurant_Address = item.TblRestaurant.Address,
                        restaurant_PhoneNumber = item.TblRestaurant.ContactNo,
                        restraurant_Website = item.TblRestaurant.Website,
                        closing_Time = item.TblRestaurant.CloseTime,
                        opening_Time = item.TblRestaurant.OpeningTime,
                        xaxis = (double)item.TblLocation.X,
                        yaxis = (double)item.TblLocation.Y
                    };
                    restaurants.Add(restaurant);
                }
                return restaurants;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<RestaurantSearchDetails> GetRetaurantBasedOnLocationAndName(LocationDetails location_Details)
        {
            List<RestaurantSearchDetails> restaurants = new List<RestaurantSearchDetails>();
            try
            {

                var restaurantInfo = (from restaurant in db.TblRestaurant
                                      join location in db.TblLocation on restaurant.TblLocationId equals location.Id
                                      select new { TblRestaurant = restaurant, TblLocation = location });

                if (!string.IsNullOrEmpty(location_Details.restaurant_Name))
                {
                    restaurantInfo = restaurantInfo.Where(a => a.TblRestaurant.Name.Contains(location_Details.restaurant_Name));

                }

                if (!(double.IsNaN(location_Details.xaxis)) && !(double.IsNaN(location_Details.yaxis)))
                {
                    foreach (var place in restaurantInfo)
                    {
                        double distance = Distance(location_Details.xaxis, location_Details.yaxis, (double)place.TblLocation.X, (double)place.TblLocation.Y);
                        if (distance < int.Parse(location_Details.distance.ToString()))
                        {
                            RestaurantSearchDetails tblRestaurant = new RestaurantSearchDetails
                            {
                                restaurant_ID = place.TblRestaurant.Id,
                                restaurant_Name = place.TblRestaurant.Name,
                                restaurant_Address = place.TblRestaurant.Address,
                                restaurant_PhoneNumber = place.TblRestaurant.ContactNo,
                                restraurant_Website = place.TblRestaurant.Website,
                                closing_Time = place.TblRestaurant.CloseTime,
                                opening_Time = place.TblRestaurant.OpeningTime,
                                xaxis = (double)place.TblLocation.X,
                                yaxis = (double)place.TblLocation.Y
                            };
                            restaurants.Add(tblRestaurant);
                        }
                    }

                }
                return restaurants;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private double Distance(double currentLatitude, double currentLongitude, double latitude, double longitude)
        {
            double theta = currentLatitude - latitude;
            double dist = Math.Sin(GetRadius(currentLatitude)) * Math.Sin(GetRadius(longitude)) + Math.Cos(GetRadius(currentLatitude)) * Math.Cos(GetRadius(latitude)) * Math.Cos(GetRadius(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = (dist * 60 * 1.1515) / 0.6213711922;          //miles to kms
            return (dist);
        }

        private double rad2deg(double dist)
        {
            return (dist * Math.PI / 180.0);
        }

        private double GetRadius(double Latitude)
        {
            return (Latitude * 180.0 / Math.PI);
        }
        #endregion
    }
}
