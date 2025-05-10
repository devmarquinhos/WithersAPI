using Microsoft.AspNetCore.Mvc;
using WithersAPI.Models;
using WithersAPI.DTO;
using WithersAPI.Services.Interfaces;

namespace WithersAPI.Controllers
{
    [Route("api/v1/characters")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CharacterResponseDto>> Get() =>
            Ok(_characterService.GetAll());

        [HttpGet("{id}")]
        public ActionResult<CharacterResponseDto> Get(int id)
        {
            var character = _characterService.GetById(id);
            return character == null ? NotFound() : Ok(character);
        }

        [HttpPost]
        public ActionResult<CharacterResponseDto> Post(Character character)
        {
            var created = _characterService.Create(character);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Character character)
        {
            return _characterService.Update(id, character) ? NoContent() : BadRequest();
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, CharacterUpdateDto dto)
        {
            return _characterService.PartialUpdate(id, dto) ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return _characterService.Delete(id) ? NoContent() : NotFound();
        }
    }
}
