using OpenLibrary.Business.Interfaces;
using OpenLibrary.Business.Interfaces.Repository;
using OpenLibrary.Business.Interfaces.Services;
using OpenLibrary.Business.Models;
using OpenLibrary.Business.Models.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLibrary.Business.Services
{
    public class BookService : BaseService, IBookService
    {

        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository, INotifier notifier) : base(notifier)
        {
            _bookRepository = bookRepository;
        }

        public async Task<bool> Insert(Book book)
        {
            if (!ExecuteValidation(new BookValidation(), book)) return false;

            if(_bookRepository.Search(b => b.Name == book.Name).Result.Any())
            {
                Notify($"The author {book.Name} already exists in the database!");
                return false;
            }

            await _bookRepository.Insert(book);
            return true;
        }

        public void Dispose()
        {
            _bookRepository?.Dispose();
        }
    }
}
