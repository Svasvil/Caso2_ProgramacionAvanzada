
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Caso2.API.Models.Tickets
{
    public enum Status{ Abierto,Cerrado, EnProceso }
    public class TicketModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // vi que era para que se genere solo el id 
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Status Estado { get; set; } = Status.Abierto;
        public int UserId { get; set; }
        public UserModel? AsignadoA { get; set; }
        public int Dificultad { get; set; }
        public string Prioridad { get; set; } = "Media";

    }
}
