using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLibrary.Business.Models.Validations
{
    public class RatingValidation : AbstractValidator<Rating>
    {
        public RatingValidation()
        {
        }
    }
}
