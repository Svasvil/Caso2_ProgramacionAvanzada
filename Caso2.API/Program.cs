using Caso2.API.DataBases;
using Caso2.API.BussinessLogic_Services_.Interfaces.Tickets;
using Caso2.API.BussinessLogic_Services_.Logic.Tickets;
using Caso2.API.DataAccess_Repository_.Interfaces.Tickets;
using Caso2.API.DataAccess_Repository_.Logics.Tickets;
using Caso2.API.BussinessLogic_Services_.Interfaces.Users;
using Caso2.API.BussinessLogic_Services_.Logic.Users;
using Caso2.API.DataAccess_Repository_.Interfaces.Users;
using Caso2.API.DataAccess_Repository_.Logics.Users;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ObjContexto>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Tickets
builder.Services.AddScoped<I_TicketBL, TicketBL>();
builder.Services.AddScoped<ICreateTicketDA, CreateTicketDA>();

// Users
builder.Services.AddScoped<I_UsersBL, UsersBL>();
builder.Services.AddScoped<ICreateUserDA, CreateUserDA>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();