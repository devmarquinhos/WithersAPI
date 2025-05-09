using Microsoft.AspNetCore.Mvc;
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
        public ActionResult<IEnumerable<Character>> Get()
        {
            return Ok(_context.Characters.ToList());
        }

        [HttpPost]
        public ActionResult<Character> Post(Character character)
        {
            _context.Characters.Add(character);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = character.Id }, character);

        }

        [HttpGet("{id}")]
        public ActionResult<Character> Get(int id) {
            var character = _context.Characters.Find(id);

            if (character == null) {
                return NotFound();
            }
            return Ok(character);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Character character) {
            if (id != character.Id) return BadRequest();
            _context.Entry(character).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            var character = _context.Characters.Find(id);
            if (character == null) return NotFound();
            _context.Characters.Remove(character);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, CharacterUpdateDto characterDto)
        {
            var character = _context.Characters.Find(id);
            if (character == null) return NotFound();

            _mapper.Map(character, characterDto);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
