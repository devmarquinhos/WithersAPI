using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WithersAPI.DTO;
using WithersAPI.Data;
using WithersAPI.Models;

// TODO - Generate the migration for Users table
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
        public ActionResult<IEnumerable<User>> Get()
        {
            return Ok(_context.Users.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);

        }

        [HttpPost]
        public ActionResult<User> Post(User user)
        {
            if (_context.Users.Any(u => u.Email == user.Email))
            {
                return Conflict("E-mail ja esta em uso.");
            }

            _context.Users.Add(user);
            _context.SaveChanges();
            return CreatedAtAction(nameof(User), new { id = user.Id }, user);
        }

        [HttpPut]
        public IActionResult Put(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            _context.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            _context.Users.Remove(user);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPatch]
        public IActionResult Patch(int id, UserUpdateDto userDto)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            _mapper.Map(user, userDto);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
