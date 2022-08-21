using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLibrary.Business.Models.Validations
{
    public class UserRatingValidation : AbstractValidator<UserRating>
    {

        public UserRatingValidation()
        {
            RuleFor(x => x.Comment)
                .Length(2, 2000)
                .WithMessage("The field {PropertyName} must be between {MinLenght} and {MaxLenght} characters");

            RuleFor(x => x.Rate)
                .LessThanOrEqualTo(5)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Rate must be between 1 and 5!");
        }
    }
}
