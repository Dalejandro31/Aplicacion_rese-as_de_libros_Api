using API.BackEnd.DTOS.Reseñas;
using AutoMapper;
using MODELOS.Shared;

namespace API.BackEnd.Profiles
{
    public class ReseñaProfile : Profile
    {
        public ReseñaProfile()
        {
            // De entidad a DTO
            CreateMap<Reseña, ResenaDto>()
                .ForMember(dest => dest.Username,
                           opt => opt.MapFrom(src => src.Usuario.Username))
                .ForMember(dest => dest.FechaResena,
                        opt => opt.MapFrom(src => src.FechaReseña));


            // De DTO de creación a entidad
            CreateMap<CreateResenaDto, Reseña>()
                .ForMember(d => d.Calificacion,
                       o => o.MapFrom(s => (int)s.Calificacion));
        }
    }
}
