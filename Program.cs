using Microsoft.EntityFrameworkCore;
using uttt.Micro.Libro.Extensions;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Registrar MediatR (asegúrate que los handlers estén en este ensamblado o ajusta)
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// Nombre de la política CORS
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins, policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:5173",
                "https://vista-libros-autores-xge2-efrenacevedos-projects.vercel.app"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Resto de servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuración del pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();  // Para mostrar errores detallados en desarrollo
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Activar CORS ANTES de cualquier middleware que maneje las solicitudes
app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
