using Caso2.Models;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Caso2.PrograAvanzada.Services
{
    public class TicketApiCall : I_TicketApiCall
    {
        private readonly HttpClient _Conexion;

        public TicketApiCall(HttpClient conexion) => _Conexion = conexion;

        public async Task<List<TicketModel>> GetAllAsync(CancellationToken canc = default)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };

            return await _Conexion.GetFromJsonAsync<List<TicketModel>>("api/Ticket", options, canc)
                   ?? new List<TicketModel>();
        }

        public async Task<TicketModel> CreateTicketAsync(string Nombre, string Descripcion, int userId, int dificultad, CancellationToken cancellation = default)
        {
            var response = await _Conexion.PostAsJsonAsync("api/Ticket", new
            {
                Nombre = Nombre,
                Descripcion = Descripcion,
                UserId = userId,
                Dificultad = dificultad
            }, cancellation);

            response.EnsureSuccessStatusCode();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };

            var resultado = await response.Content.ReadFromJsonAsync<TicketModel>(options, cancellation);
            return resultado ?? new TicketModel();
        }

        public async Task NextTicketAsync(int id, CancellationToken cancellation = default)
        {
            await _Conexion.PostAsync($"api/Ticket/{id}/advance", null, cancellation);
        }
    }
}