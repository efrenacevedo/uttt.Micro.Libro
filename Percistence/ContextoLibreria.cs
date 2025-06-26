using Microsoft.EntityFrameworkCore;
using uttt.Micro.Libro.Models;

namespace uttt.Micro.Libro.Percistence
{
    public class ContextoLibreria : DbContext
    {
        public ContextoLibreria(DbContextOptions<ContextoLibreria> options) : base(options) {
            
        }
        public DbSet<LibreriaMaterial> LibreriaMateriales { get; set; }
    }
}
