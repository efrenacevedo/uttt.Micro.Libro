using Microsoft.EntityFrameworkCore;
using uttt.Micro.Libro.Extensions;
using MediatR;
using System.Reflection;
using uttt.Micro.Libro.Percistence; // Ajusta según tu proyecto

var builder = WebApplication.CreateBuilder(args);

// Registrar DbContext con PostgreSQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ContextoLibreria>(options =>
    options.UseNpgsql(connectionString));  // Usa UseNpgsql para PostgreSQL

// Registrar MediatR
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// Política CORS
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins, policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Servicios MVC y Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();  // Mejor debug en desarrollo
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Usar CORS antes de otros middlewares que usen solicitudes
app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
