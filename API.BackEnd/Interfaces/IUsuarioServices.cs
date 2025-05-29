using API.BackEnd.DTOS.Usuarios;
using MODELOS.Shared;

namespace API.BackEnd.Interfaces
{
    public interface IUsuarioServices
    {
        Task<UsuarioDto> GetUsuarioByIdAsync(int usuarioId);
        Task<IEnumerable<UsuarioDto>> GetAllUsuariosAsync();
        Task<UsuarioDto> CreateUsuarioAsync(CreateUsuarioDto nuevoUsuario);
        Task<UsuarioDto> UpdateUsuarioAsync(int usuarioId, UpdateUsuarioDto datosActualizados);
        Task<bool> DeleteUsuarioAsync(int usuarioId);

    }
}
