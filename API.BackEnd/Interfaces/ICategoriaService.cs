using API.BackEnd.DTOS.Categorias;
using MODELOS.Shared;

namespace API.BackEnd.Interfaces
{
    public interface ICategoriaService
    {
        Task<CategoriaDto> GetCategoriaByIdAsync(int categoriaId);
        Task<IEnumerable<CategoriaDto>> GetAllCategoriasAsync();
        Task<CategoriaDto> CreateCategoriaAsync(CreateCategoriaDto nuevaCategoria);
        Task<bool> DeleteCategoriaAsync(int categoriaId);
    }
}
