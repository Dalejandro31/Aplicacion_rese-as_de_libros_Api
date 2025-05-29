using API.BackEnd.DTOS.Usuarios;
using API.BackEnd.Interfaces;
using API.BackEnd.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioServices _usuarioServices;
        public UsuarioController(IUsuarioServices usuarioServices)
        {
            _usuarioServices = usuarioServices;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDto>>> GetAll()
        {
            try
            {
                var usuarios = await _usuarioServices.GetAllUsuariosAsync();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UsuarioDto>> GetById(int id)
        {
            try
            {
                var usuario = await _usuarioServices.GetUsuarioByIdAsync(id);
                if (usuario == null)
                {
                    return NotFound();
                }
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioDto>> Create([FromBody] CreateUsuarioDto nuevoUsuario)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var creado = await _usuarioServices.CreateUsuarioAsync(nuevoUsuario);
                // Devuelve 201 Created con cabecera Location apuntando a GET api/usuarios/{id}
                return CreatedAtAction(nameof(GetById), new { id = creado.UsuarioId }, creado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<UsuarioDto>> Update(int id, [FromBody] UpdateUsuarioDto datosActualizados)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var actualizado = await _usuarioServices.UpdateUsuarioAsync(id, datosActualizados);
                if (actualizado == null)
                {
                    return NotFound();
                }
                return Ok(actualizado);
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
                var eliminado = await _usuarioServices.DeleteUsuarioAsync(id);
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
