using Caso2.API.BussinessLogic_Services_.Interfaces.Tickets;
using Caso2.API.DataAccess_Repository_.Interfaces.Tickets;
using Caso2.API.DTos.Tickets;
using Caso2.API.Models.Tickets;
using System.Net.Http.Json;
using System.Text.Json;

namespace Caso2.API.BussinessLogic_Services_.Logic.Tickets
{
    public record PriorityResult(string Descripcion, string Prioridad);

    public class TicketBL : I_TicketBL
    {
        private readonly ICreateTicketDA _createTicketDA;

        public TicketBL(ICreateTicketDA ticket)
        {
            _createTicketDA = ticket;
        }

        public async Task<TicketDTO> CreateTicket(TicketDTO dto)
        {
            using var http = new HttpClient();
            string prioridad = "Media";

            try
            {
                var response = await http.PostAsJsonAsync("https://localhost:5285/api/prioridad",
                    new { Descripcion = dto.Descripcion });

                if (response.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var result = await response.Content.ReadFromJsonAsync<PriorityResult>(options);
                    prioridad = result?.Prioridad ?? "Media";
                }
                else
                {
                    // Ver qué error retorna el MinimalAPI
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error MinimalAPI: {response.StatusCode} - {error}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción llamando MinimalAPI: {ex.Message}");
            }

            var ticketModel = new TicketModel
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Estado = dto.Estado,
                UserId = dto.UserId,
                Dificultad = dto.Dificultad,
                Prioridad = prioridad
            };

            var newTicket = await _createTicketDA.CrearTicket(ticketModel);

            return new TicketDTO(
                newTicket.Id,
                newTicket.Nombre,
                newTicket.Descripcion,
                newTicket.Estado,
                newTicket.UserId,
                newTicket.AsignadoA?.Nombre,
                newTicket.Dificultad,
                newTicket.Prioridad
            );
        }

        public async Task<bool> AdvanceStateAsync(int id)
        {
            var ticket = await _createTicketDA.ObtenerTicket_ID(id);
            if (ticket == null) return false;

            switch (ticket.Estado)
            {
                case Status.Abierto: ticket.Estado = Status.EnProceso; break;
                case Status.EnProceso: ticket.Estado = Status.Cerrado; break;
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
                t.Dificultad,
                t.Prioridad  
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
                t.Dificultad,
                t.Prioridad
            );
        }
    }
}