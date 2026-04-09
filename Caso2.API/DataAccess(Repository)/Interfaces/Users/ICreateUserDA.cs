using Caso2.API.Models;

namespace Caso2.API.DataAccess_Repository_.Interfaces.Users
{
    public interface ICreateUserDA
    {
        Task CrearUsuario(UserModel user);
        Task<List<UserModel>> GetUsuarios();
        Task<UserModel?> GetUsuario_ID(int id);


    }
}
