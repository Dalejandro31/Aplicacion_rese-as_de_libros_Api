using API.BackEnd.DTOS.Usuarios;
using API.BackEnd.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MODELOS.Shared;

namespace API.BackEnd.Services
{
    public class UsuarioService : IUsuarioServices
    {
        private readonly ReseniasLibrosContext _context;
        private readonly IMapper _mapper;

        public UsuarioService(ReseniasLibrosContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UsuarioDto> CreateUsuarioAsync(CreateUsuarioDto nuevoUsuario)
        {
            var usuarioEntity = _mapper.Map<Usuario>(nuevoUsuario);
            usuarioEntity.PasswordHash = BCrypt.Net.BCrypt.HashPassword(nuevoUsuario.Password);
            _context.Usuarios.Add(usuarioEntity);
            usuarioEntity.FechaCreacion = DateTime.UtcNow; // Asignar fecha de creación
            await _context.SaveChangesAsync();

            return _mapper.Map<UsuarioDto>(usuarioEntity);
        }

        public async Task<bool> DeleteUsuarioAsync(int usuarioId)
        {
            var usuario = await _context.Usuarios.FindAsync(usuarioId);
            if (usuario == null) return false;
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<UsuarioDto>> GetAllUsuariosAsync()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return _mapper.Map<IEnumerable<UsuarioDto>>(usuarios);
        }

        public async Task<UsuarioDto> GetUsuarioByIdAsync(int usuarioId)
        {
            var usuario = await _context.Usuarios.FindAsync(usuarioId);
            if (usuario == null) return null;
            return _mapper.Map<UsuarioDto>(usuario);
        }

        public async Task<UsuarioDto> UpdateUsuarioAsync(int usuarioId, UpdateUsuarioDto datosActualizados)
        {
            var usuario = await _context.Usuarios.FindAsync(usuarioId);
            if (usuario == null) return null;

           
            usuario.Email = datosActualizados.Email;
            

            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();

            return _mapper.Map<UsuarioDto>(usuario);
        }


    }

}
