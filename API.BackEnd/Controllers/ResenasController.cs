using API.BackEnd.DTOS.Reseñas;
using API.BackEnd.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResenasController : ControllerBase
    {
        private readonly IReseñaService _reseñaService;
        public ResenasController(IReseñaService reseñaService)
        {
            _reseñaService = reseñaService;
        }
        /// <summary>
        /// Obtiene una reseña por su ID.
        /// GET: api/reseñas/{id}
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ResenaDto>> GetById(int id)
        {
            try
            {
                var dto = await _reseñaService.GetResenaByIdAsync(id);
                if (dto == null)
                    return NotFound();
                return Ok(dto);
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex, $"Error al obtener la reseña {id}");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }

        /// <summary>
        /// Obtiene todas las reseñas de un libro.
        /// GET: api/reseñas/libro/{libroId}
        /// </summary>
        [HttpGet("libro/{libroId:int}")]
        public async Task<ActionResult<IEnumerable<ResenaDto>>> GetByLibro(int libroId)
        {
            try
            {
                var lista = await _reseñaService.GetResenasByLibroAsync(libroId);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex, $"Error al obtener reseñas para el libro {libroId}");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }

        /// <summary>
        /// Crea una nueva reseña.
        /// POST: api/reseñas
        /// </summary>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ResenaDto>> Create([FromBody] CreateResenaDto nuevaResena)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var entidad = await _reseñaService.CreateResenaAsync(nuevaResena);

                // Mapear la entidad recién creada a DTO para la respuesta
                var dto = await _reseñaService.GetResenaByIdAsync(entidad.ReseñaId);

                return CreatedAtAction(nameof(GetById), new { id = dto.ResenaId }, dto);
            }
            catch (ArgumentException aex)
            {
                // Parámetros inválidos (usuario o libro no existe)
                return BadRequest(aex.Message);
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex, "Error al crear reseña");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }

        /// <summary>
        /// Elimina una reseña.
        /// DELETE: api/reseñas/{id}
        /// </summary>
        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var ok = await _reseñaService.DeleteResenaAsync(id);
                if (!ok)
                    return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex, $"Error al eliminar la reseña {id}");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }
    }
}
