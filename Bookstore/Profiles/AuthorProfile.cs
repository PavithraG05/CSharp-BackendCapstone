using AutoMapper;

namespace Bookstore.Profiles
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile() { 
            CreateMap<Entities.Authors, Models.AuthorsDTO>();
        }
    }
}
