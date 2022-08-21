using OpenLibrary.API.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLibrary.Business.Models
{
    public class RatingViewModel  : Entity
    {
        [Key]
        public Guid Id { get; set; }
        public int TotalRating { get; set; }

        public IEnumerable<UserRatingViewModel> UserRatings { get; set; }
    }
}
