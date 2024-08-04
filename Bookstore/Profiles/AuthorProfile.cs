using AutoMapper;

namespace Bookstore.Profiles
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile() { 
            CreateMap<Entities.Authors, Models.AuthorsDTO>();
            CreateMap<Entities.Authors, Models.AuthorsDTO>().ReverseMap();

        }
    }
}
