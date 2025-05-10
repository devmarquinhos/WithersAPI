using WithersAPI.DTO;
using WithersAPI.Models;

namespace WithersAPI.Services.Interfaces
{
    public interface ICharacterService
    {
        IEnumerable<CharacterResponseDto> GetAll();
        CharacterResponseDto? GetById(int id);
        CharacterResponseDto Create(Character character);
        bool Update(int id, Character character);
        bool PartialUpdate(int id, CharacterUpdateDto dto);
        bool Delete(int id);
    }
}
