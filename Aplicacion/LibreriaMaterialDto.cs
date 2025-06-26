namespace uttt.Micro.Libro.Aplicacion
{
    public class LibreriaMaterialDto
    {
        public Guid? LibreriaMaterialId { get; set; }
        public String Titulo { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public Guid? AutorLibro { get; set; }
    }
}
