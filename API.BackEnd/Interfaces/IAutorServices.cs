using API.BackEnd.DTOS.Autores;
using MODELOS.Shared;

namespace API.BackEnd.Interfaces
{
    public interface IAutorServices
    {
        Task<AutorDto> GetAutorByIdAsync(int autorId);
        Task<IEnumerable<AutorDto>> GetAllAutoresAsync();
        Task<AutorDto> CreateAutorAsync(CreateAutorDto nuevoAutor);
        Task<bool> DeleteAutorAsync(int autorId);
    }
}
