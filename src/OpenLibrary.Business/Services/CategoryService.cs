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
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository, INotifier notifier) : base(notifier)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<bool> Insert(Category category)
        {
            //Validate entity
            if (!ExecuteValidation(new CategoryValidation(), category)) return false;

            if (_categoryRepository.Search(c => c.Name == category.Name).Result.Any())
            {
                Notify($"The category {category.Name} already exists in the database!");
                return false;
            }

            await _categoryRepository.Insert(category);
            return true;
        }

        public void Dispose()
        {
            _categoryRepository?.Dispose();
        }
    }
}
