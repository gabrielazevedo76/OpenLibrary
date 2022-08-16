using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OpenLibrary.API.ViewModels;
using OpenLibrary.Business.Interfaces;
using OpenLibrary.Business.Interfaces.Repository;
using OpenLibrary.Business.Interfaces.Services;
using OpenLibrary.Business.Models;

namespace OpenLibrary.API.Controllers
{
    [Route("api/category")]
    public class CategoryController : MainController
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, ICategoryService categoryService, IMapper mapper, INotifier notifier) : base(notifier)
        {
            _categoryRepository = categoryRepository;
            _categoryService = categoryService;
            _mapper = mapper;
        } 

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAll()
        {
            var categories = _mapper.Map<IEnumerable<CategoryViewModel>>(await _categoryRepository.GetAll());

            return CustomResponse(categories);
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<Category>> Get(Guid id)
        {
            var category = _mapper.Map<Category>(await _categoryRepository.GetById(id));
            return CustomResponse(category);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> Post([FromBody] CategoryViewModel categoryViewModel)
        {
            if (!ModelState.IsValid) return BadRequest(new { success = false, result = ModelState });

            await _categoryService.Insert(_mapper.Map<Category>(categoryViewModel));

            return CustomResponse(categoryViewModel);
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<CategoryViewModel>> Put([FromBody] CategoryViewModel categoryViewModel, Guid id)
        {
            if (id != categoryViewModel.Id) return BadRequest(new { success = false, result = "The sended Id is diferent from the Category Id informed in the query" });

            if (!ModelState.IsValid) return BadRequest(new { success = false, result = ModelState });

            await _categoryRepository.Update(_mapper.Map<Category>(categoryViewModel));

            return CustomResponse(categoryViewModel);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<CategoryViewModel>> Delete(Guid id)
        {
            var categoryViewModel = _mapper.Map<CategoryViewModel>(await _categoryRepository.GetById(id));

            if(categoryViewModel == null) return BadRequest(new { success = false, result = $"Category with Id {id} dont exists in the database" });

            await _categoryRepository.Remove(id);

            return CustomResponse(categoryViewModel);
        }
    }
}
