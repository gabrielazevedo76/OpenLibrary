using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLibrary.Business.Models
{
    public class Book : Entity
    {
        public Guid AuthorId { get; set; }
        public Guid CategoryId { get; set; }

        public string Name { get; set; }
        public string Sinopsis { get; set; }
        public string Imagem { get; set; }
        public Rating Rating { get; set; }
        public DateTime ReleaseDate { get; set; }

        /* EF Relation */
        public Author Author { get; set; }
        public Category Category { get; set; }
    }
}
