using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using uttt.Micro.Libro.Aplicacion;
using uttt.Micro.Libro.Percistence;

namespace uttt.Micro.Libro.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration) {
            services.AddControllers().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Nuevo>());
            //services.AddDbContext<ContextoLibreria>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<ContextoLibreria>(options =>   options.UseMySQL(configuration.GetConnectionString("DefaultConnection")));


            services.AddCors(options =>
            {
                options.AddPolicy("",
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                        builder.AllowAnyMethod();
                        builder.AllowAnyHeader();
                    });
            });
            services.AddMediatR(typeof(Nuevo.Manejador).Assembly);
            services.AddAutoMapper(typeof(Consulta.Manejador));
            return services;
        }
    }
}
