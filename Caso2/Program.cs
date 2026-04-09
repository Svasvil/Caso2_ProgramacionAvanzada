using Caso2.PrograAvanzada.Services;
using Caso2.PrograAvanzada.Services.Users;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// HttpClient + Services
builder.Services.AddHttpClient<I_TicketApiCall, TicketApiCall>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7275/"); // puerto de tu API
});

builder.Services.AddHttpClient<I_UserApiCall, UserApiCall>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7275/"); // puerto de tu API
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();