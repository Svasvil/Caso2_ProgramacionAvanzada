using Caso2.API.BussinessLogic_Services_.Interfaces.Tickets;
using Caso2.API.DataAccess_Repository_.Interfaces.Tickets;
using Caso2.API.DTos.Tickets;
using Caso2.API.Models.Tickets;
using System.Linq;

namespace Caso2.API.BussinessLogic_Services_.Logic.Tickets
{
    public class TicketBL : I_TicketBL
    {
        private readonly ICreateTicketDA _createTicketDA;

        public TicketBL(ICreateTicketDA ticket)
        {
            _createTicketDA = ticket;
        }

        public async Task<TicketDTO> CreateTicket(TicketDTO dto)
        {
            var ticketModel = new TicketModel
            {
                Id = dto.Id,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Estado = dto.Estado,
                UserId = dto.UserId,
                Dificultad = dto.Dificultad
            };

            await _createTicketDA.CrearTicket(ticketModel);

            var newTicket = await _createTicketDA.ObtenerTicket_ID(ticketModel.Id);

            if (newTicket == null)
                throw new InvalidOperationException("Ticket creation failed.");

            return new TicketDTO(
                newTicket.Id,
                newTicket.Nombre,
                newTicket.Descripcion,
                newTicket.Estado,
                newTicket.UserId,
                newTicket.AsignadoA?.Nombre,
                newTicket.Dificultad
            );
        }
        public async Task<bool> AdvanceStateAsync(int id)
        {
            var ticket = await _createTicketDA.ObtenerTicket_ID(id);
            if (ticket == null) return false;

            switch (ticket.Estado)
            {
                case Status.Abierto: ticket.Estado = Status.Abierto; break;
                case Status.Cerrado: ticket.Estado = Status.Cerrado; break;
                case Status.EnProceso: ticket.Estado = Status.EnProceso; break;
                default: return false;
            }

            await _createTicketDA.UpdateTicket(ticket);
            return true;
        }

        public async Task<List<TicketDTO>> GetAllTickets()
        {
            var list = await _createTicketDA.ObtenerTickets();
            return list.Select(t => new TicketDTO(
                t.Id,
                t.Nombre,
                t.Descripcion,
                t.Estado,
                t.UserId,
                t.AsignadoA != null ? $"{t.AsignadoA.Nombre} {t.AsignadoA.Apellidos}" : null,
                t.Dificultad
            )).ToList();
        }

        public async Task<TicketDTO?> GetTicketById(int id)
        {
            var t = await _createTicketDA.ObtenerTicket_ID(id);
            return t == null ? null : new TicketDTO(
                t.Id,
                t.Nombre,
                t.Descripcion,
                t.Estado,
                t.UserId,
                t.AsignadoA != null ? $"{t.AsignadoA.Nombre} {t.AsignadoA.Apellidos}" : null,
                t.Dificultad
            );
        }
    }
}