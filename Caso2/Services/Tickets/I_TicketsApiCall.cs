using Caso2.Models;

namespace Caso2.PrograAvanzada.Services
{
    public interface I_TicketApiCall
    {
        Task<List<TicketViewModel>> GetAllAsync(CancellationToken canc = default);
        Task<TicketViewModel> CreateTicketAsync(string Nombre, string Descripcion, int userId, int Dificultad, CancellationToken cancellation = default);
        Task NextTicketAsync(int id, CancellationToken cancellation = default);
    }
}