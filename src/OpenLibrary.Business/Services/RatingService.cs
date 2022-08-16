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
    public class RatingService : BaseService, IRatingService
    {
        private readonly IRatingRepository _ratingRepository;

        public RatingService(IRatingRepository ratingRepository, INotifier notifier) : base(notifier)
        {
            _ratingRepository = ratingRepository;
        }

        public async Task<Rating> GetById(Guid id)
        {
            var rating = await _ratingRepository.GetById(id);

            var totalRate = CalcTotalRate(rating.UserRatings);

            rating.TotalRating = totalRate;

            return rating;
        }

        private int CalcTotalRate(IEnumerable<UserRating> userRating)
        {
            var totalRate = userRating.Sum(x => x.Rate) / userRating.Count();

            return totalRate;
        }

        public void Dispose()
        {
            _ratingRepository?.Dispose();
        }
    }
}
