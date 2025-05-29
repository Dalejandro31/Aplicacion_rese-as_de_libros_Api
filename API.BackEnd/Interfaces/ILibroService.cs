using API.BackEnd.DTOS.Libros;
using MODELOS.Shared;

namespace API.BackEnd.Interfaces
{
    public interface ILibroService
    {
        Task<LibroDto> GetLibroByIdAsync(int libroId);
        Task<IEnumerable<LibroDto>> GetAllLibrosAsync();
        Task<IEnumerable<LibroDto>> SearchLibrosAsync(string titulo = null, string autor = null, int? categoriaId = null);
        Task<LibroDto> CreateLibroAsync(CreateLibroDto nuevoLibro);
        Task<bool> DeleteLibroAsync(int libroId);
    }
}
