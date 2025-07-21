using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace uttt.Micro.Libro.Percistence
{
    public class ContextoLibreriaFactory : IDesignTimeDbContextFactory<ContextoLibreria>
    {
        public ContextoLibreria CreateDbContext(string[] args)
        {
            // Carga la configuración desde el appsettings.json
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Asegúrate que estás en la raíz del proyecto
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<ContextoLibreria>();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseNpgsql(connectionString); // o UseSqlServer si usas SQL Server

            return new ContextoLibreria(builder.Options);
        }
    }
}
