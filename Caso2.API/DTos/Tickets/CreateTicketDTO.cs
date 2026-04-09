using Caso2.API.Models;
using Caso2.API.Models.Tickets;

namespace Caso2.API.DTos.Tickets
{
    public record CreateTicketDTO
    (
         int Id,
         string Nombre,
         string Descripcion,
         Status Estado,      
         int UserId,
         int Dificultad,
        string? Prioridad
    );
}