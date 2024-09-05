using MovieAPI.Dtos;
using AutoMapper;
using MovieAPI.Models;

namespace MovieAPI.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie, MovieDto>();
            CreateMap<CreateMovieDto, Movie>();
            CreateMap<UpdateMovieDto, Movie>();
        }
    }
}
