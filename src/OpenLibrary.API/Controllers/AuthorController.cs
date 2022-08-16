using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OpenLibrary.API.ViewModels;
using OpenLibrary.Business.Interfaces;
using OpenLibrary.Business.Interfaces.Repository;
using OpenLibrary.Business.Models;

namespace OpenLibrary.API.Controllers
{
    [Route("api/author")]
    public class AuthorController : MainController
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorController(IAuthorRepository authorRepository, IMapper mapper, INotifier notifier) : base(notifier)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorViewModel>>> GetAll()
        {
            var authors = _mapper.Map<IEnumerable<AuthorViewModel>>(await _authorRepository.GetAll());
            return CustomResponse(authors);
        }

        [HttpGet("id")]
        public async Task<ActionResult<AuthorViewModel>> GetById(Guid id)
        {
            var author = _mapper.Map<AuthorViewModel>(await _authorRepository.GetAuthorWithBooks(id));
            return CustomResponse(author);
        }

        [HttpPost]
        public async Task<ActionResult<AuthorViewModel>> Post([FromBody] AuthorViewModel authorViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _authorRepository.Insert(_mapper.Map<Author>(authorViewModel));

            return CustomResponse(authorViewModel);
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<AuthorViewModel>> Put([FromBody] AuthorViewModel authorViewModel, Guid id)
        {
            if (id != authorViewModel.Id) return BadRequest(new { success = false, result = "The sended Id is diferent from the Author Id informed in the query" });

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _authorRepository.Update(_mapper.Map<Author>(authorViewModel));

            return CustomResponse(authorViewModel);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<AuthorViewModel>> Delete(Guid id)
        {
            var authorViewModel = _mapper.Map<AuthorViewModel>(await _authorRepository.GetById(id));

            if(authorViewModel == null) return BadRequest(new { success = false, result = $"Author with Id {id} dont exists in the database" });

            await _authorRepository.Remove(id);

            return CustomResponse(authorViewModel);
        }
    }
}
