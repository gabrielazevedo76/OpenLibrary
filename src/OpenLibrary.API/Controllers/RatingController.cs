using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OpenLibrary.API.ViewModels;
using OpenLibrary.Business.Interfaces;
using OpenLibrary.Business.Interfaces.Repository;
using OpenLibrary.Business.Interfaces.Services;
using OpenLibrary.Business.Models;

namespace OpenLibrary.API.Controllers
{
    [Route("api/rating")]
    public class RatingController : MainController
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IMapper _mapper;

        public RatingController(IRatingRepository ratingRepository, IMapper mapper, INotifier notifier) : base(notifier)
        {
            _ratingRepository = ratingRepository;
            _mapper = mapper;
        } 

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rating>>> GetAll()
        {
            var ratings = _mapper.Map<IEnumerable<RatingViewModel>>(await _ratingRepository.GetAll());

            return CustomResponse(ratings);
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<Rating>> Get(Guid id)
        {
            var rating = _mapper.Map<RatingViewModel>(await _ratingRepository.GetByIdWithRelation(id));

            return CustomResponse(rating);
        }

        [HttpPost]
        public async Task<ActionResult<Rating>> Post([FromBody] RatingViewModel ratingViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _ratingRepository.Insert(_mapper.Map<Rating>(ratingViewModel));

            return CustomResponse(ratingViewModel);
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<RatingViewModel>> Put([FromBody] RatingViewModel ratingViewModel, Guid id)
        {
            if (id != ratingViewModel.Id) return BadRequest(new { success = false, result = "The sended Id is diferent from the Rating Id informed in the query" });

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _ratingRepository.Update(_mapper.Map<Rating>(ratingViewModel));

            return CustomResponse(ratingViewModel);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<RatingViewModel>> Delete(Guid id)
        {
            var ratingViewModel = _mapper.Map<RatingViewModel>(await _ratingRepository.GetById(id));

            if(ratingViewModel == null) return BadRequest(new { success = false, result = $"Rating with Id {id} dont exists in the database" });

            await _ratingRepository.Remove(id);

            return CustomResponse(ratingViewModel);
        }
    }
}
