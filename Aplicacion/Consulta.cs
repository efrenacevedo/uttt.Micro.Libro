using AutoMapper;
using MediatR;
using uttt.Micro.Libro.Models;
using uttt.Micro.Libro.Percistence;
using Microsoft.EntityFrameworkCore;

namespace uttt.Micro.Libro.Aplicacion
{
    public class Consulta
    {
        public class Ejecuta : IRequest<List<LibreriaMaterialDto>>
        {
            public Ejecuta()
            {
            }
        }
        public class Manejador : IRequestHandler<Ejecuta,List<LibreriaMaterialDto>> 
        {
            private readonly ContextoLibreria _contexto;
            private readonly IMapper _mapper;
            public Manejador(ContextoLibreria contexto, IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }
            public async Task<List<LibreriaMaterialDto>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var libros = await _contexto.LibreriaMateriales.ToListAsync();
                var librosDto = _mapper.Map<List<LibreriaMaterial>, List<LibreriaMaterialDto>>(libros);
                return librosDto;
            }
        }
    }
}
