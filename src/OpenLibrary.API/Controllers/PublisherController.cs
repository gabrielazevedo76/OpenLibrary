using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OpenLibrary.API.ViewModels;
using OpenLibrary.Business.Interfaces;
using OpenLibrary.Business.Interfaces.Repository;
using OpenLibrary.Business.Interfaces.Services;
using OpenLibrary.Business.Models;

namespace OpenLibrary.API.Controllers
{
    [Route("api/publisher")]
    public class PublisherController : MainController
    {
        private readonly IPublisherRepository _publisherRepository;
        private readonly IMapper _mapper;

        public PublisherController(IPublisherRepository publisherRepository, IMapper mapper, INotifier notifier) : base(notifier)
        {
            _publisherRepository = publisherRepository;
            _mapper = mapper;
        } 

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Publisher>>> GetAll()
        {
            var publishers = _mapper.Map<IEnumerable<PublisherViewModel>>(await _publisherRepository.GetAll());

            return CustomResponse(publishers);
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<Publisher>> Get(Guid id)
        {
            var publisher = _mapper.Map<PublisherViewModel>(await _publisherRepository.GetById(id));
            return CustomResponse(publisher);
        }

        [HttpPost]
        public async Task<ActionResult<Publisher>> Post([FromBody] PublisherViewModel publisherViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _publisherRepository.Insert(_mapper.Map<Publisher>(publisherViewModel));

            return CustomResponse(publisherViewModel);
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<PublisherViewModel>> Put([FromBody] PublisherViewModel publisherViewModel, Guid id)
        {
            if (id != publisherViewModel.Id) return BadRequest(new { success = false, result = "The sended Id is diferent from the Category Id informed in the query" });

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _publisherRepository.Update(_mapper.Map<Publisher>(publisherViewModel));

            return CustomResponse(publisherViewModel);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<PublisherViewModel>> Delete(Guid id)
        {
            var publisherViewModel = _mapper.Map<PublisherViewModel>(await _publisherRepository.GetById(id));

            if(publisherViewModel == null) return BadRequest(new { success = false, result = $"Category with Id {id} dont exists in the database" });

            await _publisherRepository.Remove(id);

            return CustomResponse(publisherViewModel);
        }
    }
}
