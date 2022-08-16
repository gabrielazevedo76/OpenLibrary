using AutoMapper;
using OpenLibrary.API.ViewModels;
using OpenLibrary.Business.Models;

namespace OpenLibrary.API.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Author, AuthorViewModel>().ReverseMap();
            CreateMap<Book, BookViewModel>().ReverseMap();
            CreateMap<Category, CategoryViewModel>().ReverseMap();
            CreateMap<Rating, RatingViewModel>().ReverseMap();
            CreateMap<UserRating, UserRatingViewModel>().ReverseMap();
        }
    }
}
