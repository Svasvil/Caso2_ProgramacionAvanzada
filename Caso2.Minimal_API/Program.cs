var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger(); // This now works with the correct using directives
app.UseSwaggerUI();

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

    return Results.Ok(new { Descripcion = request.Descripcion, Prioridad = prioridad });
});

app.Run();

record PriorityRequest(string Descripcion);