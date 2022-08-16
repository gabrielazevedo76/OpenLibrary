using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLibrary.Business.Models.Validations
{
    public class CategoryValidation : AbstractValidator<Category>
    {

        public CategoryValidation()
        {
            RuleFor(b => b.Name)
            .NotEmpty()
            .WithMessage(" campo {PropertyName} precisa ser fornecido")
            .Length(2, 100)
            .WithMessage("O campo {PropertyName} precisa ter entre {MinLenght} e {MaxLenght} caracteres");
        }
    }
}
