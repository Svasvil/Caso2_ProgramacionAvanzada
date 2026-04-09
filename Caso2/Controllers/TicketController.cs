using Caso2.PrograAvanzada.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace Caso2.PrograAvanzada.Controllers
{
    public class TicketController : Controller
    {
        private readonly I_TicketApiCall _theCall;
        private readonly I_UserApiCall _usersCall;

        public TicketController(I_TicketApiCall theCall, I_UserApiCall usersCall)
        {
            _theCall = theCall;
            _usersCall = usersCall;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _theCall.GetAllAsync();
            var users = await _usersCall.GetAllAsync();

            ViewBag.Users = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(users, "Id", "Nombre");
            ViewData["ColorAbierto"] = "bg-primary";
            ViewData["ColorEnProceso"] = "bg-warning";
            ViewData["ColorCerrado"] = "bg-success";

            return View(list.OrderBy(t => t.Id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket(string Nombre, string Descripcion, int UserId, CancellationToken cancellation)
        {
            if (string.IsNullOrWhiteSpace(Nombre) || string.IsNullOrWhiteSpace(Descripcion))
                return BadRequest("Campos Requeridos");

            var ticketCreado = await _theCall.CreateTicketAsync(Nombre, Descripcion, UserId, 0, cancellation);

            if (ticketCreado.Dificultad > 7)
            {
                TempData["AltaDificultad"] = $"El ticket '{Nombre}' tiene una estimación alta: {ticketCreado.Dificultad}";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> NextTicket(int id)
        {
            await _theCall.NextTicketAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}