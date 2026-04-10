using Caso2.API.DTos.Tickets;

namespace Caso2.API.BussinessLogic_Services_.Interfaces.Tickets
{
    public interface I_TicketBL
    {
        Task<List<TicketDTO>> GetAllTickets();
        Task<TicketDTO?> GetTicketById(int id);
        Task<TicketDTO> CreateTicket(CreateTicketDTO model);
        Task<bool> AdvanceStateAsync(int id);
    }
}
