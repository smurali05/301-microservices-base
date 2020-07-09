using FluentValidation;
using MT.OnlineRestaurant.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MT.OnlineRestaurant.RestuarantReviewAPI.ModelValidators
{
    /// <summary>
    /// Update Restuarant Review Validator
    /// </summary>
    public class UpdateRestuarantReviewEntityValidator : AbstractValidator<RestaurantReviewEntity>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public UpdateRestuarantReviewEntityValidator()
        {
            RuleFor(m => m.ReviewId)
                .NotEmpty()
                .NotNull()
                .Must(BeAValidReview).When(p => p.ReviewId == 0).WithMessage("Invalid restuarant Id");

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
        /// <returns>Boolean whether specified review is valid or invalid</returns>
        private bool BeAValidReview(int Id)
        {
            bool IsValidReview = false;

            return IsValidReview;
        }
    }
}


