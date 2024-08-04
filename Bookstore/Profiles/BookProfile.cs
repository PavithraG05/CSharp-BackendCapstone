using AutoMapper;

namespace Bookstore.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile() {
            CreateMap<Entities.Books, Models.BooksDTO>();
            CreateMap<Entities.Books, Models.BooksDTO>().ReverseMap();

        }

    }
}
