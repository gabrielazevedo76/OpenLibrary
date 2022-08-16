using OpenLibrary.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLibrary.Business.Interfaces.Services
{
    public interface IBookService : IDisposable
    {
        Task<bool> Insert(Book book);
    }
}
