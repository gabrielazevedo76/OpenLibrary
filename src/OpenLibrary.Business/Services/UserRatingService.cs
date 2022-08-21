using OpenLibrary.Business.Interfaces;
using OpenLibrary.Business.Interfaces.Repository;
using OpenLibrary.Business.Interfaces.Services;
using OpenLibrary.Business.Models;
using OpenLibrary.Business.Models.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLibrary.Business.Services
{
    public class UserRatingService : BaseService, IUserRatingService
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IUserRatingRepository _userRatingRepository;

        public UserRatingService(IRatingRepository ratingRepository, IUserRatingRepository userRatingRepository, INotifier notifier) : base(notifier)
        {
            _ratingRepository = ratingRepository;
            _userRatingRepository = userRatingRepository;
        }

        public async Task<bool> Insert(UserRating userRating)
        {
            if (!ExecuteValidation(new UserRatingValidation(), userRating)) return false;

            /* validate if the user already have an rating to this book 
            if (_userRatingRepository.Search(x => x.RatingId == userRating.RatingId).Result.Any()){}
            */
            
            var rating = await _ratingRepository.GetByIdWithRelation(userRating.RatingId);
            
            var totalRate = CalcTotalRate(rating.UserRatings);

            rating.TotalRating = totalRate;

            await _userRatingRepository.Insert(userRating);
            
            await _ratingRepository.Update(rating);

            return true;
        }

        private int CalcTotalRate(IEnumerable<UserRating> userRating)
        {
            if(userRating == null) return 0;
            
            if (userRating.Count() != 0)
            {
                var totalRate = userRating.Sum(x => x.Rate) / userRating.Count();
                
                return totalRate;
            }
            
            return userRating.First().Rate;
        }

        public void Dispose()
        {
            _ratingRepository?.Dispose();
        }
    }
}
