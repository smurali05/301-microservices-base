using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using MT.OnlineRestaurant.BusinessEntities;
using MT.OnlineRestaurant.BusinessLayer.interfaces;
using MT.OnlineRestaurant.RestuarantReviewAPI.ModelValidators;

namespace MT.OnlineRestaurant.RestuarantReviewAPI.Controllers
{
    /// <summary>
    /// RestuarantReview Controller
    /// </summary>
    [Produces("application/json")]
    [Route("api")]
    public class RestuarantReviewController : Controller
    {
        private readonly IRestaurantReviewAction _restaurantReviewAction;

        /// <summary>
        /// Inject buisiness layer dependency
        /// </summary>
        /// <param name="restaurantReviewAction"></param>
        public RestuarantReviewController(IRestaurantReviewAction restaurantReviewAction)
        {
            _restaurantReviewAction = restaurantReviewAction;
        }

        /// <summary>
        /// Add restuarant review
        /// </summary>
        /// <param name="restaurantReviewEntity">Restuarant Review details</param>
        /// <returns>Restuarant Review Add status</returns>
        [HttpPost]
        [Route("AddRestuarantReview")]
        public IActionResult AddRestuarantReview([FromBody]RestaurantReviewEntity restaurantReviewEntity)
        {
            RestuarantReviewEntityValidator restuarantReviewEntityValidator = new RestuarantReviewEntityValidator();
            ValidationResult validationResult = restuarantReviewEntityValidator.Validate(restaurantReviewEntity);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ToString("; "));
            }
            else
            {
                var result = _restaurantReviewAction.AddRestuarantReview(restaurantReviewEntity);
                if (result == 0)
                {
                    return BadRequest("Resturant Review add failed, Please try again later");
                }
            }
            return Ok("Resturant Review add is successful");
        }

        /// <summary>
        /// Update restuarant review by review Id
        /// </summary>
        /// <param name="restaurantReviewEntity">Restuarant Review details</param>
        /// <returns>Restuarant Review Add status</returns>
        [HttpPut]
        [Route("UpdateRestuarantReview")]
        public IActionResult UpdateRestuarantReview([FromBody]RestaurantReviewEntity restaurantReviewEntity)
        {
            UpdateRestuarantReviewEntityValidator restuarantReviewEntityValidator = new UpdateRestuarantReviewEntityValidator();
            ValidationResult validationResult = restuarantReviewEntityValidator.Validate(restaurantReviewEntity);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ToString("; "));
            }
            else
            {
                _restaurantReviewAction.UpdateRestuarantReview(restaurantReviewEntity);
            }
            return Ok("Restuarant Review updated successfully");
        }

        /// <summary>
        /// Get restuarant reviews by restuarant ID
        /// </summary>
        /// <param name="restuarantId">RestuarantId</param>
        /// <returns>Restuarant review list by restuarant Id</returns>
        [HttpGet]
        [Route("GetRestuarantReviews")]
        public IActionResult GetRestuarantReviews([FromQuery]int restuarantId)
        {
            var restuarantReviews = _restaurantReviewAction.GetRestuarantReviewByRestuarantId(restuarantId);
            return Ok(restuarantReviews);            
        }
    }
}
