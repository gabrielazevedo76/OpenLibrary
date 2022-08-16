using OpenLibrary.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLibrary.Business.Interfaces.Repository
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<Author> GetAuthorWithBooks(Guid id);
    }
}
