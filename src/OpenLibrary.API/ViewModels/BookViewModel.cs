using System.ComponentModel.DataAnnotations;

namespace OpenLibrary.API.ViewModels
{
    public class BookViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(2000, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 10)]
        public string Sinopsis { get; set; }

        public string ImagemUpload { get; set; }

        public string Imagem { get; set; }

        public DateTime ReleaseDate { get; set; }

        /* EF Relations */
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid CategoryId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid AuthorId { get; set; }
    }
}
