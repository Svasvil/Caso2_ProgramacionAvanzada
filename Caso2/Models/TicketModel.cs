namespace Caso2.Models
{
    public enum Status { Abierto, Cerrado, EnProceso }
    public class TicketModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Status Estado { get; set; } = Status.Abierto;
        public int UserId { get; set; }
        public UserModel? AsignadoA { get; set; }
        public int Dificultad { get; set; }
    }
}
