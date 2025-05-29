using API.BackEnd.DTOS.Autores;
using API.BackEnd.DTOS.Usuarios;
using API.BackEnd.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MODELOS.Shared;

namespace API.BackEnd.Services
{
    public class AutorService : IAutorServices
    {
        private readonly ReseniasLibrosContext _context;
        private readonly IMapper _mapper;

        public AutorService(ReseniasLibrosContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AutorDto> CreateAutorAsync(CreateAutorDto nuevoAutor)
        {
            var autoreEntity = _mapper.Map<Autore>(nuevoAutor);
            _context.Autores.Add(autoreEntity);
            await _context.SaveChangesAsync();

            return _mapper.Map<AutorDto>(autoreEntity);
        }

        public async Task<bool> DeleteAutorAsync(int autorId)
        {
            var autor = await _context.Autores.FindAsync(autorId);
            if (autor == null) return false;
            _context.Autores.Remove(autor);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<AutorDto>> GetAllAutoresAsync()
        {
            var autores = await _context.Autores.ToListAsync();
            return _mapper.Map<IEnumerable<AutorDto>>(autores);
        }

        public async Task<AutorDto> GetAutorByIdAsync(int autorId)
        {
            var autor = await _context.Autores.FindAsync(autorId);
            if (autor == null) return null;
            return _mapper.Map<AutorDto>(autor);
        }
    }
}
