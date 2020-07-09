using MT.OnlineRestaurant.BusinessEntities;
using MT.OnlineRestaurant.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using System.Diagnostics.CodeAnalysis;

namespace MT.OnlineRestaurant.BusinessLayer
{
    [ExcludeFromCodeCoverage]
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<RestaurantReviewEntity, TblRestaurantReview>()
                .ForMember(dest => dest.TblRestaurantId, opt => opt.MapFrom(src => src.RestaurantId))
                .ForMember(dest => dest.TblCustomerId, opt => opt.MapFrom(src => src.CustomerId))
                .ForMember(dest => dest.TblReviewId, opt => opt.MapFrom(src => src.ReviewId))
                .ForMember(dest => dest.TblUserComments, opt => opt.MapFrom(src => src.Comments))
                .ForMember(dest => dest.TblRating, opt => opt.MapFrom(src => src.Rating)).ReverseMap();
        }
    }
}
