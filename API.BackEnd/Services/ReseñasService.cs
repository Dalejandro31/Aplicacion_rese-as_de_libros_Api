using API.BackEnd.DTOS.Reseñas;
using API.BackEnd.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MODELOS.Shared;

namespace API.BackEnd.Services
{
    public class ReseñasService : IReseñaService
    {
        private readonly ReseniasLibrosContext _context;    
        private readonly IMapper _mapper;

        public ReseñasService(ReseniasLibrosContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResenaDto> GetResenaByIdAsync(int resenaId)
        {
            var reseña = await _context.Reseñas
                .Include(r => r.Usuario)
                .FirstOrDefaultAsync(r => r.ReseñaId == resenaId);

            return reseña == null
                ? null
                : _mapper.Map<ResenaDto>(reseña);
        }

        public async Task<IEnumerable<ResenaDto>> GetResenasByLibroAsync(int libroId)
        {
            var reseñas = await _context.Reseñas
                .Where(r => r.LibroId == libroId)
                .Include(r => r.Usuario)
                .OrderByDescending(r => r.FechaReseña)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ResenaDto>>(reseñas);
        }

        public async Task<Reseña> CreateResenaAsync(CreateResenaDto nuevaResena)
        {
            // Validar existencia de usuario y libro
            var usuario = await _context.Usuarios.FindAsync(nuevaResena.UsuarioId);
            var libro = await _context.Libros.FindAsync(nuevaResena.LibroId);
            if (usuario == null || libro == null)
                throw new ArgumentException("Usuario o Libro no encontrado.");

            // Mapear CreateResenaDto a entidad Reseña
            var entidad = _mapper.Map<Reseña>(nuevaResena);
            entidad.FechaReseña = DateTime.Now; // Asignar fecha actual
            _context.Reseñas.Add(entidad);
            await _context.SaveChangesAsync();

            return entidad;
        }

        public async Task<bool> DeleteResenaAsync(int resenaId)
        {
            var reseña = await _context.Reseñas.FindAsync(resenaId);
            if (reseña == null)
                return false;

            _context.Reseñas.Remove(reseña);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
