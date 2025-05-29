using API.BackEnd.DTOS.Autores;
using API.BackEnd.DTOS.Categorias;
using API.BackEnd.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;
        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDto>>> GetAll()
        {
            try
            {
                var categorias = await _categoriaService.GetAllCategoriasAsync();
                return Ok(categorias);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoriaDto>> GetById(int id)
        {
            try
            {
                var categoria = await _categoriaService.GetCategoriaByIdAsync(id);
                if (categoria == null)
                {
                    return NotFound();
                }
                return Ok(categoria);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<CategoriaDto>> Create([FromBody] CreateCategoriaDto nuevaCategoria)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var creado = await _categoriaService.CreateCategoriaAsync(nuevaCategoria);

                return CreatedAtAction(nameof(GetById), new { id = creado.CategoriaId }, creado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var eliminado = await _categoriaService.DeleteCategoriaAsync(id);
                if (!eliminado)
                {
                    return NotFound();
                }
                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
