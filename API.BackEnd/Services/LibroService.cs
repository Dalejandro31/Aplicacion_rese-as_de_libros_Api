using API.BackEnd.DTOS.Libros;
using API.BackEnd.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MODELOS.Shared;

namespace API.BackEnd.Services
{
    public class LibroService : ILibroService
    {
        private readonly ReseniasLibrosContext _context;
        private readonly IMapper _mapper;

        public LibroService(ReseniasLibrosContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LibroDto>> GetAllLibrosAsync()
        {
            var libros = await _context.Libros
                .Include(l => l.Autors)
                .Include(l => l.Categoria)
                .Include(l => l.Reseñas)
                    .ThenInclude(r => r.Usuario)
                .ToListAsync();

            return _mapper.Map<IEnumerable<LibroDto>>(libros);
        }

        public async Task<LibroDto> GetLibroByIdAsync(int libroId)
        {
            var libro = await _context.Libros
                .Include(l => l.Autors)
                .Include(l => l.Categoria)
                .Include(l => l.Reseñas)
                    .ThenInclude(r => r.Usuario)
                .FirstOrDefaultAsync(l => l.LibroId == libroId);

            return libro == null
                ? null
                : _mapper.Map<LibroDto>(libro);
        }

        public async Task<IEnumerable<LibroDto>> SearchLibrosAsync(string titulo = null, string autor = null, int? categoriaId = null)
        {
            var query = _context.Libros
                .Include(l => l.Autors)
                .Include(l => l.Categoria)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(titulo))
            {
                var t = titulo.Trim().ToLower();
                query = query.Where(l => l.Titulo.ToLower().Contains(t));
            }

            if (!string.IsNullOrWhiteSpace(autor))
            {
                var a = autor.Trim().ToLower();
                query = query.Where(l => l.Autors.Any(x => x.Nombre.ToLower().Contains(a)));
            }

            if (categoriaId.HasValue)
            {
                query = query.Where(l => l.Categoria.Any(c => c.CategoriaId == categoriaId.Value));
            }

            var resultados = await query.ToListAsync();
            return _mapper.Map<IEnumerable<LibroDto>>(resultados);
        }

        public async Task<LibroDto> CreateLibroAsync(CreateLibroDto dto)
        {
            var libro = _mapper.Map<Libro>(dto);

            _context.Libros.Add(libro);

            if (dto.AutorIds != null)
            {
                foreach (var autorId in dto.AutorIds.Distinct())
                {
                    var autor = await _context.Autores.FindAsync(autorId);
                    if (autor != null)
                        libro.Autors.Add(autor);
                }
            }
            if (dto.CategoriaIds != null)
            {
                foreach (var catId in dto.CategoriaIds.Distinct())
                {
                    var categoria = await _context.Categorias.FindAsync(catId);
                    if (categoria != null)
                        libro.Categoria.Add(categoria);
                }
            }

            await _context.SaveChangesAsync();
            return await GetLibroByIdAsync(libro.LibroId);
        }

        public async Task<bool> DeleteLibroAsync(int libroId)
        {
            var libro = await _context.Libros.FindAsync(libroId);
            if (libro == null)
                return false;

            _context.Libros.Remove(libro);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
