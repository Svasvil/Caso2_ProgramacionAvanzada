using Caso2.Models;

namespace Caso2.PrograAvanzada.Services.Users
{
    public interface I_UserApiCall
    {
        Task<List<UserModel>> GetAllAsync(CancellationToken canc = default);
        Task CreateUserAsync(string Nombre, string Apellidos, CancellationToken cancellation = default);
    }
}