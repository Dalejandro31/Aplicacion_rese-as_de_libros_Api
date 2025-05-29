using API.BackEnd.DTOS.Autores;
using API.BackEnd.DTOS.Usuarios;
using API.BackEnd.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly IAutorServices _autorServices;
        public AutorController(IAutorServices autorServices)
        {
            _autorServices = autorServices;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AutorDto>>> GetAll()
        {
            try
            {
                var autores = await _autorServices.GetAllAutoresAsync();
                return Ok(autores);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AutorDto>> GetById(int id)
        {
            try
            {
                var autor = await _autorServices.GetAutorByIdAsync(id);
                if (autor == null)
                {
                    return NotFound();
                }
                return Ok(autor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<AutorDto>> Create([FromBody] CreateAutorDto nuevoAutor)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var creado = await _autorServices.CreateAutorAsync(nuevoAutor);
               
                return CreatedAtAction(nameof(GetById), new { id = creado.AutorId }, creado);
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
                var eliminado = await _autorServices.DeleteAutorAsync(id);
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
