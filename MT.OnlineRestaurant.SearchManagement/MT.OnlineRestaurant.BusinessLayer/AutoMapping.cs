using AutoMapper;
using MT.OnlineRestaurant.BusinessEntities;
using MT.OnlineRestaurant.DataLayer.DataEntity;
using MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MT.OnlineRestaurant.BusinessLayer
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<BusinessEntities.LocationDetails, DataLayer.DataEntity.LocationDetails>().ReverseMap();
            CreateMap<BusinessEntities.AdditionalFeatureForSearch, DataLayer.DataEntity.AddtitionalFeatureForSearch>().ReverseMap();
            CreateMap<SearchForRestaurant, DataLayer.DataEntity.SearchForRestautrant>().ReverseMap();

            CreateMap<RestaurantInformation, RestaurantSearchDetails>().ReverseMap();
            CreateMap<TblMenu, BusinessEntities.MenuSearchDetails>().ReverseMap();
        }
    }
}