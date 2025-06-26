using AutoMapper;
using uttt.Micro.Libro.Models;

namespace uttt.Micro.Libro.Aplicacion
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<LibreriaMaterial, LibreriaMaterialDto>();
        }
    }
}
