using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OpenLibrary.API.ViewModels;
using OpenLibrary.Business.Interfaces;
using OpenLibrary.Business.Interfaces.Repository;
using OpenLibrary.Business.Models;

namespace OpenLibrary.API.Controllers
{
    [Route("api/book")]
    public class BookController : MainController
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookController(IBookRepository bookRepository, IMapper mapper, INotifier notifier) : base(notifier)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetAll()
        {
            var books = _mapper.Map<IEnumerable<BookViewModel>>(await _bookRepository.GetAll());

            return CustomResponse(books);
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<Book>> Get(Guid id)
        {
            var book = _mapper.Map<Book>(await _bookRepository.GetById(id));
            return CustomResponse(book);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> Post([FromBody] BookViewModel bookViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var imageName = bookViewModel.Imagem + "_" + Guid.NewGuid();

            if (!ImageUpload(bookViewModel.ImagemUpload, imageName))
            {
                return CustomResponse(bookViewModel);
            }

            bookViewModel.Imagem = imageName;

            await _bookRepository.Insert(_mapper.Map<Book>(bookViewModel));

            return CustomResponse(bookViewModel);
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<BookViewModel>> Put([FromBody] BookViewModel bookViewModel, Guid id)
        {
            if (id != bookViewModel.Id)
            {
                NotifyError("The sended Id is diferent from the Book Id informed in the query");
                return CustomResponse();
            }

            var bookUpdate = await _bookRepository.GetById(id);
            bookViewModel.Imagem = bookUpdate.Imagem;
            
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if(bookViewModel.ImagemUpload != null)
            {
                var imageName = bookViewModel.Imagem + "_" + Guid.NewGuid();
                if (!ImageUpload(bookViewModel.ImagemUpload, imageName))
                {
                    return CustomResponse(ModelState);
                }

                bookUpdate.Imagem = imageName;
            }

            bookUpdate.Name = bookViewModel.Name;
            bookUpdate.Sinopsis = bookViewModel.Sinopsis;

            await _bookRepository.Update(_mapper.Map<Book>(bookViewModel));

            return CustomResponse(bookViewModel);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<BookViewModel>> Delete(Guid id)
        {
            var bookViewModel = _mapper.Map<BookViewModel>(await _bookRepository.GetById(id));

            if(bookViewModel == null) return BadRequest(new { success = false, result = $"Book with Id {id} dont exists in the database" });

            await _bookRepository.Remove(id);

            return CustomResponse(bookViewModel);
        }

        private bool ImageUpload(string image, string imageName)
        {
            var imageDataByteArray = Convert.FromBase64String(image);

            if (image == null || image.Length <= 0)
            {
                NotifyError("You need to give an image for this book");
                return false;
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imageName);

            if (System.IO.File.Exists(filePath))
            {
                NotifyError("Already exists an image with this name");
                return false;
            }

            System.IO.File.WriteAllBytes(filePath, imageDataByteArray);
            return true;
        }
    }
}
