using Caso2.API.BussinessLogic_Services_.Interfaces.Tickets;
using Caso2.API.DTos.Tickets;
using Microsoft.AspNetCore.Mvc;

namespace Caso2.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly I_TicketBL _ticketBL;

        public TicketController(I_TicketBL ticketBL) => _ticketBL = ticketBL;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _ticketBL.GetAllTickets();
            return Ok(result ?? new List<TicketDTO>());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ticket = await _ticketBL.GetTicketById(id);
            if (ticket is null) return NotFound();
            return Ok(ticket);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket(CreateTicketDTO model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

          
            var result = await _ticketBL.CreateTicket(model);

            return Ok(result);
        }

        [HttpPost("{id}/advance")]
        public async Task<IActionResult> AdvanceState(int id)
        {
            var ok = await _ticketBL.AdvanceStateAsync(id);
            if (!ok) return BadRequest();
            return Ok();
        }
    }
}