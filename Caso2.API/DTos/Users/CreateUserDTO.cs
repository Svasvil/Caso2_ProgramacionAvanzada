namespace Caso2.API.DTos.Users
{
    public record CreateUserDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
    }
}
