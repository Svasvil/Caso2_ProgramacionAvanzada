using Caso2.API.Models.Tickets;

namespace Caso2.API.DataAccess_Repository_.Interfaces.Tickets
{
    public interface ICreateTicketDA
    {
        //methods 
            Task<List<TicketModel>> ObtenerTickets();
        Task<TicketModel?> ObtenerTicket_ID(int id);
        Task<TicketModel> CrearTicket(TicketModel task);
        Task UpdateTicket(TicketModel task);

    }
}
