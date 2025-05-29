using API.BackEnd.DTOS.Autores;
using API.BackEnd.DTOS.Categorias;
using AutoMapper;
using MODELOS.Shared;

namespace API.BackEnd.Profiles
{
    public class CategoriaProfile : Profile
    {
        public CategoriaProfile()
        {
            CreateMap<CreateCategoriaDto, Categoria>();
            CreateMap<Categoria, CategoriaDto>();
        }
    }
}
