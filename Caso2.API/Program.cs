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


// 🔥 DB CONTEXT
builder.Services.AddDbContext<ObjContexto>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// 🔥 DEPENDENCIAS - TICKETS
builder.Services.AddScoped<I_TicketBL, TicketBL>();
builder.Services.AddScoped<ICreateTicketDA, CreateTicketDA>();


// 🔥 DEPENDENCIAS - USERS
builder.Services.AddScoped<I_UsersBL, UsersBL>();
builder.Services.AddScoped<ICreateUserDA, CreateUserDA>();


// 🔥 CONTROLADORES
builder.Services.AddControllers();


// 🔥 SWAGGER
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();


// 🔥 MIDDLEWARE
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();