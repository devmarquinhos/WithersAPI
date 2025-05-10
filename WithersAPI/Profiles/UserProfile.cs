using AutoMapper;
using WithersAPI.Models;
using WithersAPI.DTO;

namespace WithersAPI.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserResponse>();
            CreateMap<Character, CharacterBasicDto>();
        }
    }
}
