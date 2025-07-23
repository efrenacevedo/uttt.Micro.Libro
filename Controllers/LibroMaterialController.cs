using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using uttt.Micro.Libro.Aplicacion;

namespace uttt.Micro.Libro.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LibroMaterialController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LibroMaterialController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("crear")]
        public async Task<ActionResult<Unit>> Crear([FromBody] Nuevo.Ejecuta data) { 
            
            return await _mediator.Send(data); 
        
        }

        [HttpGet]
        public async Task<ActionResult<List<LibreriaMaterialDto>>> GetLibros()
        {
            
            return await _mediator.Send(new Consulta.Ejecuta());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LibreriaMaterialDto>> GetLibro(Guid id)
        {
            return await _mediator.Send(new ConsultaFiltro.LibroUnico { LibroId = id });
        }
        //[HttpPut("{id}")]
        //public async Task<ActionResult<Unit>> Editar(Guid id, [FromBody] Editar.Ejecuta data)
        //{
        //    data.LibreriaMaterialId = id;
        //    return await _mediator.Send(data);
        //}

        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Unit>> Eliminar(Guid id)
        //{
        //    return await _mediator.Send(new Eliminar.Ejecuta { Id = id });
        //}
    }
}
