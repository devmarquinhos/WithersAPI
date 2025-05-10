using AutoMapper;
using WithersAPI.Models;
using WithersAPI.DTO;

namespace WithersAPI.Profiles
{
    public class CharacterProfile : Profile
    {
        public CharacterProfile()
        {
            CreateMap<Character, CharacterResponseDto>();
        }
    }
}
