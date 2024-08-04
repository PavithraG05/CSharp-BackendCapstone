using AutoMapper;

namespace Bookstore.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile() { 
            CreateMap<Entities.Users, Models.UsersDTO>();
        }
    }
}
