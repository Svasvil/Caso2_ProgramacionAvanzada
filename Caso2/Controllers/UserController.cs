using Caso2.PrograAvanzada.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace Caso2.PrograAvanzada.Controllers
{
    public class UsersController : Controller
    {
        private readonly I_UserApiCall _theCall;

        public UsersController(I_UserApiCall theCall) => _theCall = theCall;

        public async Task<IActionResult> Index()
        {
            var userList = await _theCall.GetAllAsync();
            return View(userList.OrderBy(u => u.Id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(string Nombre, string Apellidos, CancellationToken cancellation)
        {
            if (string.IsNullOrWhiteSpace(Nombre) || string.IsNullOrWhiteSpace(Apellidos))
                return BadRequest("Campos Requeridos");

            await _theCall.CreateUserAsync(Nombre, Apellidos, cancellation);
            return RedirectToAction(nameof(Index));
        }
    }
}