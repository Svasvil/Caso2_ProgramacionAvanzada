using Caso2.Minimal_API.Services; 

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<TestService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

var valoresDificultad = new[] { "Alta", "Media", "Baja" };

//apis
app.MapGet("/api/estimate", () =>
{
    var random = new Random();
    var value = valoresDificultad[random.Next(valoresDificultad.Length)];
    return Results.Ok(value);
});

app.MapPost("/api/prioridad", (PriorityRequest request, TestService service) =>
{
    var prioridad = service.ObtenerPrioridad(request.Descripcion);

    return Results.Ok(new
    {
        Descripcion = request.Descripcion,
        Prioridad = prioridad
    });
});

app.Run();

record PriorityRequest(string Descripcion);