using Caso2.API.Models.Tickets;

namespace Caso2.API.DTos.Tickets
{
    public record TicketDTO(
        int Id,
        string Nombre,
        string Descripcion,
        Status Estado,
        int UserId,
        string? AsignadoA,
        int Dificultad
    );
}