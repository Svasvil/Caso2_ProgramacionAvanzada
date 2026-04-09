using Caso2.Models;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Caso2.PrograAvanzada.Services.Users
{
    public class UserApiCall : I_UserApiCall
    {
        public readonly HttpClient _Conexion;

        public UserApiCall(HttpClient conexion) => _Conexion = conexion;

        public async Task<List<UserModel>> GetAllAsync(CancellationToken canc = default)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };

            return await _Conexion.GetFromJsonAsync<List<UserModel>>("api/Users", options, canc)
                   ?? new List<UserModel>();
        }

        public async Task CreateUserAsync(string Nombre, string Apellidos, CancellationToken cancellation = default)
        {
            var nuevoUsuario = new { Nombre, Apellidos };
            await _Conexion.PostAsJsonAsync("api/Users", nuevoUsuario, cancellation);
        }
    }
}