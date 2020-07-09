using FluentValidation;
using MT.OnlineRestaurant.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MT.OnlineRestaurant.RestuarantReviewAPI.ModelValidators
{
    /// <summary>
    /// Restuarant review entity validator
    /// </summary>
    public class RestuarantReviewEntityValidator : AbstractValidator<RestaurantReviewEntity>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public RestuarantReviewEntityValidator()
        {
            RuleFor(m => m.RestaurantId)
                .NotEmpty()
                .NotNull()
                .Must(BeAValidReview).When(p => p.RestaurantId == 0).WithMessage("Invalid restuarant Id");

            RuleFor(m => m.CustomerId)
                .NotEmpty()
                .NotNull()
                .Must(BeAValidReview).When(p => p.CustomerId == 0).WithMessage("Invalid Customer Id");

            RuleFor(m => m.Comments)
                .NotEmpty()
                .NotNull();                

            RuleFor(m => m.Rating)
                .NotEmpty()
                .NotNull();
        }

        /// <summary>
        /// Make a service call to fetch all resturants and validate between them
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>Boolean whether specified Review is valid or invalid</returns>
        private bool BeAValidReview(int Id)
        {
            bool IsValidReview = false;

            return IsValidReview;
        }
    }
}
