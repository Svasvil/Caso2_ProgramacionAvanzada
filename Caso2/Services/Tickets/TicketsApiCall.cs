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

        public async Task<List<TicketViewModel>> GetAllAsync(CancellationToken canc = default)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };

            return await _Conexion.GetFromJsonAsync<List<TicketViewModel>>("api/Ticket", options, canc)
                   ?? new List<TicketViewModel>();
        }

        public async Task<TicketViewModel> CreateTicketAsync(string Nombre, string Descripcion, int userId, int dificultad, string prioridad, CancellationToken cancellation = default)
        {
     
            var response = await _Conexion.PostAsJsonAsync("api/Ticket", new
            {
                Nombre,
                Descripcion,
                UserId = userId,
                Dificultad = dificultad,
                Prioridad = prioridad 
            }, cancellation);

            response.EnsureSuccessStatusCode();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };

            var resultado = await response.Content.ReadFromJsonAsync<TicketViewModel>(options, cancellation);
            return resultado ?? new TicketViewModel();
        }

        public async Task NextTicketAsync(int id, CancellationToken cancellation = default)
        {
            await _Conexion.PostAsync($"api/Ticket/{id}/advance", null, cancellation);
        }
    }
}