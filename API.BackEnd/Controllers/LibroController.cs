using API.BackEnd.DTOS.Libros;
using API.BackEnd.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly ILibroService _libroService;

        public LibroController(ILibroService libroService)
        {
            _libroService = libroService;
        }

        /// <summary>
        /// Devuelve todos los libros.
        /// GET: api/libros
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibroDto>>> GetAll()
        {
            try
            {
                var libros = await _libroService.GetAllLibrosAsync();
                return Ok(libros);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { mensaje = "Ocurrió un error al obtener los libros.", detalle = ex.Message });
            }
        }

        /// <summary>
        /// Devuelve un libro por su ID.
        /// GET: api/libros/{id}
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<LibroDto>> GetById(int id)
        {
            try
            {
                var libro = await _libroService.GetLibroByIdAsync(id);
                if (libro == null)
                    return NotFound();
                return Ok(libro);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { mensaje = "Ocurrió un error al obtener el libro.", detalle = ex.Message });
            }
        }

        /// <summary>
        /// Busca libros por título, autor o categoría.
        /// GET: api/libros/search?titulo=...&autor=...&categoriaId=...
        /// </summary>
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<LibroDto>>> Search(
            [FromQuery] string titulo = null,
            [FromQuery] string autor = null,
            [FromQuery] int? categoriaId = null)
        {
            try
            {
                var resultados = await _libroService.SearchLibrosAsync(titulo, autor, categoriaId);
                return Ok(resultados);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { mensaje = "Ocurrió un error al buscar los libros.", detalle = ex.Message });
            }
        }

        /// <summary>
        /// Crea un nuevo libro.
        /// POST: api/libros
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<LibroDto>> Create([FromBody] CreateLibroDto nuevoLibro)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var creado = await _libroService.CreateLibroAsync(nuevoLibro);
                // Devuelve 201 Created y cabecera Location
                return CreatedAtAction(nameof(GetById), new { id = creado.LibroId }, creado);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { mensaje = "Ocurrió un error al crear el libro.", detalle = ex.Message });
            }
        }

        /// <summary>
        /// Elimina un libro existente.
        /// DELETE: api/libros/{id}
        /// </summary>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var eliminado = await _libroService.DeleteLibroAsync(id);
                if (!eliminado)
                    return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { mensaje = "Ocurrió un error al eliminar el libro.", detalle = ex.Message });
            }
        }
    }
}
