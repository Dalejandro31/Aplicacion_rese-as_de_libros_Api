using API.BackEnd.DTOS.Usuarios;
using AutoMapper;
using MODELOS.Shared;

namespace API.BackEnd.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<CreateUsuarioDto, Usuario>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
            // De UpdateUsuarioDto → Usuario
            CreateMap<UpdateUsuarioDto, Usuario>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            // De Usuario → UsuarioDto (o como se llame tu DTO de salida)
            CreateMap<Usuario, UsuarioDto>();
        }
    }
}
