using Caso2.PrograAvanzada.Services;
using Caso2.PrograAvanzada.Services.Users;
using Caso2.Minimal_API.Services; 

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();


builder.Services.AddSingleton<TestService>();

builder.Services.AddHttpClient<I_TicketApiCall, TicketApiCall>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7275/");
});

builder.Services.AddHttpClient<I_UserApiCall, UserApiCall>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7275/");
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

 app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();