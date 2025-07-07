using Microsoft.EntityFrameworkCore;
using uttt.Micro.Libro.Extensions;

var builder = WebApplication.CreateBuilder(args);

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
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// QUITAR este middleware manual porque puede causar problemas:
// app.Use(async (context, next) =>
// {
//     context.Response.Headers.Append("Access-Control-Allow-Origin", "*");
//     context.Response.Headers.Append("Access-Control-Allow-Methods", "*");
//     context.Response.Headers.Append("Access-Control-Allow-Headers", "*");

//     if (context.Request.Method == "OPTIONS")
//     {
//         context.Response.StatusCode = 200;
//         return;
//     }

//     await next();
// });

app.UseHttpsRedirection();

// Activar CORS ANTES de cualquier middleware que use las solicitudes
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
