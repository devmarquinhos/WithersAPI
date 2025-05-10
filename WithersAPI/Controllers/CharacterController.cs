using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WithersAPI.Data;
using WithersAPI.Models;
using WithersAPI.DTO;
using AutoMapper;

namespace WithersAPI.Controllers
{
    [Route("api/v1/characters")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly WithersContext _context;
        private readonly IMapper _mapper;

        public CharacterController(WithersContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CharacterResponseDto>> Get()
        {
            var characters = _context.Characters.ToList();
            var charactersDto = _mapper.Map<IEnumerable<CharacterResponseDto>>(characters);
            return Ok(charactersDto);
        }

        [HttpGet("{id}")]
        public ActionResult<CharacterResponseDto> Get(int id)
        {
            var character = _context.Characters.Find(id);
            if (character == null) return NotFound();

            var characterDto = _mapper.Map<CharacterResponseDto>(character);
            return Ok(characterDto);
        }

        [HttpPost]
        public ActionResult<CharacterResponseDto> Post(Character character)
        {
            _context.Characters.Add(character);
            _context.SaveChanges();

            var characterDto = _mapper.Map<CharacterResponseDto>(character);
            return CreatedAtAction(nameof(Get), new { id = character.Id }, characterDto);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Character character)
        {
            if (id != character.Id) return BadRequest();

            _context.Entry(character).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, CharacterUpdateDto characterDto)
        {
            var character = _context.Characters.Find(id);
            if (character == null) return NotFound();

            _mapper.Map(characterDto, character);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var character = _context.Characters.Find(id);
            if (character == null) return NotFound();

            _context.Characters.Remove(character);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
