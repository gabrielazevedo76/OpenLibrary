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
    public class AuthorService : BaseService, IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository, INotifier notifier) : base(notifier)
        {
            _authorRepository = authorRepository;
        }

        public async Task<bool> Insert(Author author)
        {
            if (!ExecuteValidation(new AuthorValidation(), author)) return false;

            if(_authorRepository.Search(a => a.Name == author.Name).Result.Any())
            {
                Notify($"The author {author.Name} already exists in the database!");
                return false;
            }

            await _authorRepository.Insert(author);
            return true;
        }

        public void Dispose()
        {
            _authorRepository?.Dispose();
        }

    }
}
