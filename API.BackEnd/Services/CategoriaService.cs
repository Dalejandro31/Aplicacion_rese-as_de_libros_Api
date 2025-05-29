using API.BackEnd.DTOS.Autores;
using API.BackEnd.DTOS.Categorias;
using API.BackEnd.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MODELOS.Shared;

namespace API.BackEnd.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ReseniasLibrosContext _context;
        private readonly IMapper _mapper;

        public CategoriaService(ReseniasLibrosContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CategoriaDto> CreateCategoriaAsync(CreateCategoriaDto nuevaCategoria)
        {
            var categoriaEntity = _mapper.Map<Categoria>(nuevaCategoria);
            _context.Categorias.Add(categoriaEntity);
            await _context.SaveChangesAsync();

            return _mapper.Map<CategoriaDto>(categoriaEntity);
        }

        public async Task<bool> DeleteCategoriaAsync(int categoriaId)
        {
            var categoria = await _context.Categorias.FindAsync(categoriaId);
            if (categoria == null) return false;
            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CategoriaDto>> GetAllCategoriasAsync()
        {
            var categorias = await _context.Categorias.ToListAsync();
            return _mapper.Map<IEnumerable<CategoriaDto>>(categorias);
        }

        public async Task<CategoriaDto> GetCategoriaByIdAsync(int categoriaId)
        {
            var categoria = await _context.Categorias.FindAsync(categoriaId);
            if (categoria == null) return null;
            return _mapper.Map<CategoriaDto>(categoria);
        }
    }
}
