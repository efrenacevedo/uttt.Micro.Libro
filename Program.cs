using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using uttt.Micro.Libro.Extensions;
using MediatR;
using System.Reflection;
using uttt.Micro.Libro.Percistence;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ?? Configurar JWT
var key = builder.Configuration["Jwt:Key"];
var issuer = builder.Configuration["Jwt:Issuer"];
var keyBytes = Encoding.UTF8.GetBytes(key);

// Agregar autenticación JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes)
        };
    });

// ?? Registrar DbContext con PostgreSQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ContextoLibreria>(options =>
    options.UseNpgsql(connectionString));

// ?? Registrar MediatR
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// ?? CORS
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins, policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// ?? Servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ?? Orden correcto de middlewares
app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthentication(); // ?? MUY IMPORTANTE: antes de Authorization
app.UseAuthorization();

app.MapControllers();

app.Run();
