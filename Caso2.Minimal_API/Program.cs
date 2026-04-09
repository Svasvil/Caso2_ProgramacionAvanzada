var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var valoresDificultad = new[] { "Alta", "Media", "Baja" };
app.MapGet("/api/estimate", () =>
{
    var random = new Random();
    var value = valoresDificultad[random.Next(valoresDificultad.Length)];
    return Results.Ok(value);
});

app.MapPost("/api/prioridad", (PriorityRequest request) =>
{
    var desc = request.Descripcion?.ToLower() ?? string.Empty;
    Console.WriteLine($"Descripcion recibida: {desc}"); 

    bool esAlta = desc.Contains("error") ||
                   desc.Contains("caído") ||
                   desc.Contains("no funciona");

    bool esMedia = desc.Contains("lento") ||
                   desc.Contains("intermitente");

    bool esBaja = desc.Contains("consulta") ||
                   desc.Contains("duda");

    string prioridad;
    if (esAlta) prioridad = "Alta";
    else if (esMedia) prioridad = "Media";
    else if (esBaja) prioridad = "Baja";
    else prioridad = "Media";

    Console.WriteLine($"Prioridad asignada: {prioridad}"); 

    return Results.Ok(new { Descripcion = request.Descripcion, Prioridad = prioridad });
});

record PriorityRequest(string Descripcion);