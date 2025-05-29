using API.BackEnd.DTOS.Autores;
using API.BackEnd.DTOS.Usuarios;
using AutoMapper;
using MODELOS.Shared;

namespace API.BackEnd.Profiles
{
    public class AutorProfile : Profile
    {
        public AutorProfile()
        {
            CreateMap<CreateAutorDto, Autore>();
            CreateMap<Autore, AutorDto>();
        }

    }
}
