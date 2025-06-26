using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using uttt.Micro.Libro.Models;
using uttt.Micro.Libro.Percistence;

namespace uttt.Micro.Libro.Aplicacion
{
    public class ConsultaFiltro
    {
        public class LibroUnico : IRequest<LibreriaMaterialDto>
        {
            public Guid? LibroId { get; set; }
        }
        public class Manejador : IRequestHandler<LibroUnico,LibreriaMaterialDto> { 
            private readonly ContextoLibreria _contexto;
            private readonly IMapper _mapper;
            public Manejador(ContextoLibreria contexto, IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }
            public async Task<LibreriaMaterialDto> Handle(LibroUnico request, CancellationToken cancellationToken)
            {
                var libro = await _contexto.LibreriaMateriales.Where(x => x.LibreriaMaterialId == request.LibroId).FirstOrDefaultAsync();
                if (libro == null)
                {
                    throw new Exception("No se encontro el libro");
                }    
                var libroDto = _mapper.Map<LibreriaMaterial,LibreriaMaterialDto>(libro);
                return libroDto;
            }
        }
    }
}
