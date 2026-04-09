using Caso2.API.DataAccess_Repository_.Interfaces.Tickets;
using Caso2.API.DataBases;
using Caso2.API.Models.Tickets;
using Microsoft.EntityFrameworkCore;

namespace Caso2.API.DataAccess_Repository_.Logics.Tickets
{
    public class CreateTicketDA : ICreateTicketDA
    {
        private readonly ObjContexto _context;
        public CreateTicketDA(ObjContexto context)=>_context = context;

        //get all 
        public async Task<List<TicketModel>> ObtenerTickets() => await _context.Ticket
                .Include(t => t.AsignadoA)
                .AsNoTracking()
                .ToListAsync();

        //get id 
        public async Task<TicketModel?> ObtenerTicket_ID(int id) => await _context.Ticket.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);


        //create 
        public async Task<TicketModel> CrearTicket(TicketModel task)
        {
            _context.Ticket.Add(task);
            await _context.SaveChangesAsync();
            return task; 
        }

        //update
        public async Task UpdateTicket(TicketModel task)
        {
            _context.Ticket.Update(task);
            await _context.SaveChangesAsync();
        }
    }
}
