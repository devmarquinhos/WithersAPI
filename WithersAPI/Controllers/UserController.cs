using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using WithersAPI.DTO;
using WithersAPI.Data;
using WithersAPI.Models;

namespace WithersAPI.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly WithersContext _context;
        private readonly IMapper _mapper;

        public UserController(WithersContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserResponse>> Get()
        {
            var users = _context.Users
                .Include(u => u.Characters)
                .ToList();

            var usersDto = _mapper.Map<IEnumerable<UserResponse>>(users);
            return Ok(usersDto);
        }

        [HttpGet("{id}")]
        public ActionResult<UserResponse> Get(int id)
        {
            var user = _context.Users
                .Include(u => u.Characters)
                .FirstOrDefault(u => u.Id == id);

            if (user == null) return NotFound();

            var userDto = _mapper.Map<UserResponse>(user);
            return Ok(userDto);
        }

        [HttpPost]
        public ActionResult<UserResponse> Post(User user)
        {
            if (_context.Users.Any(u => u.Email == user.Email))
            {
                return Conflict("E-mail já está em uso.");
            }

            _context.Users.Add(user);
            _context.SaveChanges();

            var userDto = _mapper.Map<UserResponse>(user);
            return CreatedAtAction(nameof(Get), new { id = user.Id }, userDto);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, User user)
        {
            if (id != user.Id) return BadRequest();

            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, UserUpdateDto userDto)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();

            _mapper.Map(userDto, user);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();

            _context.Users.Remove(user);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
