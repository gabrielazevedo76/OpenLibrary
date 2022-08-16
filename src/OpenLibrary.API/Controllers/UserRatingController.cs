using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OpenLibrary.API.ViewModels;
using OpenLibrary.Business.Interfaces;
using OpenLibrary.Business.Interfaces.Repository;
using OpenLibrary.Business.Interfaces.Services;
using OpenLibrary.Business.Models;

namespace OpenLibrary.API.Controllers
{
    [Route("api/user-rating")]
    public class UserRatingController : MainController
    {
        private readonly IUserRatingRepository _userRatingRepository;
        private readonly IMapper _mapper;

        public UserRatingController(IUserRatingRepository userRatingRepository, IMapper mapper, INotifier notifier) : base(notifier)
        {
            _userRatingRepository = userRatingRepository;
            _mapper = mapper;
        } 

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserRating>>> GetAll()
        {
            var userRatings = _mapper.Map<IEnumerable<UserRatingViewModel>>(await _userRatingRepository.GetAll());

            return CustomResponse(userRatings);
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<UserRating>> Get(Guid id)
        {
            var userRating = _mapper.Map<UserRating>(await _userRatingRepository.GetById(id));
            return CustomResponse(userRating);
        }

        [HttpPost]
        public async Task<ActionResult<UserRating>> Post([FromBody] UserRatingViewModel userRatingViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _userRatingRepository.Insert(_mapper.Map<UserRating>(userRatingViewModel));

            return CustomResponse(userRatingViewModel);
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<UserRatingViewModel>> Put([FromBody] UserRatingViewModel userRatingViewModel, Guid id)
        {
            if (id != userRatingViewModel.Id) return BadRequest(new { success = false, result = "The sended Id is diferent from the Rating Id informed in the query" });

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _userRatingRepository.Update(_mapper.Map<UserRating>(userRatingViewModel));

            return CustomResponse(userRatingViewModel);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<UserRatingViewModel>> Delete(Guid id)
        {
            var userRatingViewModel = _mapper.Map<UserRatingViewModel>(await _userRatingRepository.GetById(id));

            if(userRatingViewModel == null) return BadRequest(new { success = false, result = $"Rating with Id {id} dont exists in the database" });

            await _userRatingRepository.Remove(id);

            return CustomResponse(userRatingViewModel);
        }
    }
}
