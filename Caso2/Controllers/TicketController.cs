using Caso2.PrograAvanzada.Services;
using Caso2.PrograAvanzada.Services.Users;
using Caso2.Minimal_API.Services; 
using Microsoft.AspNetCore.Mvc;

namespace Caso2.PrograAvanzada.Controllers
{
    public class TicketController : Controller
    {
        private readonly I_TicketApiCall _theCall;
        private readonly I_UserApiCall _usersCall;
        private readonly TestService _prioridadService;

        public TicketController(I_TicketApiCall theCall, I_UserApiCall usersCall, TestService prioridadService)
        {
            _theCall = theCall;
            _usersCall = usersCall;
            _prioridadService = prioridadService;
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
        public async Task<IActionResult> CreateTicket(string Nombre, string Descripcion, int UserId, int Dificultad, CancellationToken cancellation)
        {
            
            string prioridadCalculada = _prioridadService.ObtenerPrioridad(Descripcion);

           
            var ticketCreado = await _theCall.CreateTicketAsync(
                Nombre,
                Descripcion,
                UserId,
                Dificultad,
                prioridadCalculada,
                cancellation);

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