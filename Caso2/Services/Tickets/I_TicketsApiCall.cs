using Caso2.Models;

public interface I_TicketApiCall
{
    Task<List<TicketModel>> GetAllAsync(CancellationToken canc = default);
    Task<TicketModel> CreateTicketAsync(string Nombre, string Descripcion, int userId, int Dificultad, CancellationToken cancellation = default);
    Task NextTicketAsync(int id, CancellationToken cancellation = default);
}