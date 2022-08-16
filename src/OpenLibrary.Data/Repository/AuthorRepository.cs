using Microsoft.EntityFrameworkCore;

using OpenLibrary.Business.Interfaces.Repository;
using OpenLibrary.Business.Models;
using OpenLibrary.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLibrary.Data.Repository
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(OpenLibraryDbContext context) : base(context){}

        public async Task<Author> GetAuthorWithBooks(Guid id)
        {
             var authorWithBooks = await Db.Authors.AsNoTracking()
            .Include(a => a.Books)
            .FirstOrDefaultAsync(a => a.Id == id);

            return authorWithBooks;
        }
    }
}
