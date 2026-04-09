using System.ComponentModel.DataAnnotations;

namespace Caso2.API.Models
{
    public class UserModel
    {
        [Required] public int Id { get; set; }
        [Required][MaxLength(25)] public string Nombre { get; set; }
        [Required][MaxLength(50)] public string Apellidos { get; set; }
    }
}
