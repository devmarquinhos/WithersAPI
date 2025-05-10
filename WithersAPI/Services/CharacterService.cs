using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WithersAPI.Data;
using WithersAPI.DTO;
using WithersAPI.Models;
using WithersAPI.Services.Interfaces;

namespace WithersAPI.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly WithersContext _context;
        private readonly IMapper _mapper;

        public CharacterService(WithersContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<CharacterResponseDto> GetAll()
        {
            var characters = _context.Characters.ToList();
            return _mapper.Map<IEnumerable<CharacterResponseDto>>(characters);
        }

        public CharacterResponseDto? GetById(int id)
        {
            var character = _context.Characters.Find(id);
            return character == null ? null : _mapper.Map<CharacterResponseDto>(character);
        }

        public CharacterResponseDto Create(Character character)
        {
            _context.Characters.Add(character);
            _context.SaveChanges();
            return _mapper.Map<CharacterResponseDto>(character);
        }

        public bool Update(int id, Character character)
        {
            if (id != character.Id) return false;

            _context.Entry(character).State = EntityState.Modified;
            _context.SaveChanges();
            return true;
        }

        public bool PartialUpdate(int id, CharacterUpdateDto dto)
        {
            var character = _context.Characters.Find(id);
            if (character == null) return false;

            _mapper.Map(dto, character);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var character = _context.Characters.Find(id);
            if (character == null) return false;

            _context.Characters.Remove(character);
            _context.SaveChanges();
            return true;
        }
    }
}
