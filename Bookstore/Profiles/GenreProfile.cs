using AutoMapper;

namespace Bookstore.Profiles
{
    public class GenreProfile : Profile
    {
        public GenreProfile() { 
            CreateMap<Entities.Genres, Models.GenresDTO>();
        }
    }
}
