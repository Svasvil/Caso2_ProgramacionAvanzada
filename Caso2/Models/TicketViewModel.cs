namespace Caso2.Models
{
    public class TicketViewModel

    {
        public enum EstadoTicket
        {
            Abierto = 0,
            EnProceso = 1,
            Cerrado = 2
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public EstadoTicket Estado { get; set; }
        public int UserId { get; set; }
        public string? AsignadoA { get; set; }
        public int Dificultad { get; set; }
        public string? Prioridad { get; set; }
    }
}