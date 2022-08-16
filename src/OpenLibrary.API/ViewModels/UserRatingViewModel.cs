using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLibrary.Business.Models
{
    public class UserRatingViewModel  : Entity
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid RatingId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Rate { get; set; }

        [StringLength(2000, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string? Comment { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime Created { get; set; }
    }
}
