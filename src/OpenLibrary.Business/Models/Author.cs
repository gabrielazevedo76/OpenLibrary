using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLibrary.Business.Models
{
    public class Author : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Birthdate { get; set; }

        /* EF Relation */
        public IEnumerable<Book> Books { get; set; }
    }
}
