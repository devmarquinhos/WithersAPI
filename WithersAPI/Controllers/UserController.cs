using Microsoft.AspNetCore.Mvc;
using WithersAPI.Models;
using WithersAPI.DTO;
using WithersAPI.Services.Interfaces;

namespace WithersAPI.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserResponse>> Get() =>
            Ok(_userService.GetAll());

        [HttpGet("{id}")]
        public ActionResult<UserResponse> Get(int id)
        {
            var user = _userService.GetById(id);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost]
        public ActionResult<UserResponse> Post(User user)
        {
            if (user == null) return BadRequest();
            var created = _userService.Create(user);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, User user)
        {
            return _userService.Update(id, user) ? NoContent() : BadRequest();
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, UserUpdateDto dto)
        {
            return _userService.PartialUpdate(id, dto) ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return _userService.Delete(id) ? NoContent() : NotFound();
        }
    }
}
