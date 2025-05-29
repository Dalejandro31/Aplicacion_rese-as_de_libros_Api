using API.BackEnd.DTOS.Reseñas;
using MODELOS.Shared;

namespace API.BackEnd.Interfaces
{
    public interface IReseñaService
    {
        Task<ResenaDto> GetResenaByIdAsync(int resenaId);
        Task<IEnumerable<ResenaDto>> GetResenasByLibroAsync(int libroId);
        Task<Reseña> CreateResenaAsync(CreateResenaDto nuevaResena);
        Task<bool> DeleteResenaAsync(int resenaId);
    }
}
