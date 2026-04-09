using Caso2.PrograAvanzada.Services;
using Caso2.PrograAvanzada.Services.Users;

var builder = WebApplication.CreateBuilder(args);

// 🔥 MVC
builder.Services.AddControllersWithViews();

// 🔥 HttpClient para Tickets
builder.Services.AddHttpClient<I_TicketApiCall, TicketApiCall>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7275/");
});

// 🔥 HttpClient para Users
builder.Services.AddHttpClient<I_UserApiCall, UserApiCall>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7275/");
});

var app = builder.Build();

// 🔥 Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// 🔥 Rutas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();