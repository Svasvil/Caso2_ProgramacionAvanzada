using Caso2.API.BussinessLogic_Services_.Interfaces.Users;
using Caso2.API.DTos.Users;
using Microsoft.AspNetCore.Mvc;

namespace Caso2.API.Controllers
{
    [ApiController]
    [Route("api/Users")]
    public class UserController : ControllerBase
    {
        private readonly I_UsersBL _usersBL;

        public UserController(I_UsersBL usersBL) => _usersBL = usersBL;

        [HttpGet]
        public async Task<ActionResult<List<CreateUserDTO>>> GetAll()
        {
            var result = await _usersBL.GetAllUsers();
            return Ok(result ?? new List<CreateUserDTO>());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _usersBL.GetUserById(id);
            if (user is null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDTO model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _usersBL.CreateUser(model);
            return Ok();
        }
    }
}