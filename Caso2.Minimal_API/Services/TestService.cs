namespace Caso2.Minimal_API.Services
{
    public class TestService
    {
        public string ObtenerPrioridad(string? descripcion)
        {
            if (string.IsNullOrWhiteSpace(descripcion))
            {
                return "Media";
            }

            string desc = descripcion.ToLower();

            if (desc.Contains("error") || desc.Contains("caído") || desc.Contains("no funciona"))
            {
                return "Alta";
            }

            if (desc.Contains("lento") || desc.Contains("intermitente"))
            {
                return "Media";
            }

     
            if (desc.Contains("consulta") || desc.Contains("duda"))
            {
                return "Baja";
            }

         
            return "Media";
        }
    }
}

