using API.BackEnd.DTOS.Autores;
using API.BackEnd.DTOS.Categorias;
using API.BackEnd.DTOS.Libros;
using API.BackEnd.DTOS.Reseñas;
using AutoMapper;
using MODELOS.Shared;

namespace API.BackEnd.Profiles
{
    public class LibroProfile : Profile
    {
        public LibroProfile()
        {
            // Mapeo de creación: ignoramos las colecciones
            CreateMap<CreateLibroDto, Libro>()
                .ForMember(dest => dest.Autors, opt => opt.Ignore())
                .ForMember(dest => dest.Categoria, opt => opt.Ignore());

            // Mapeos de Autor y Categoría para que la colección se convierta en DTOs
            CreateMap<Autore, AutorDto>();
            CreateMap<Categoria, CategoriaDto>();

            // Mapeo de Reseña
            CreateMap<Reseña, ResenaDto>()
                .ForMember(dest => dest.Username,
                           opt => opt.MapFrom(src => src.Usuario.Username));

            // Finalmente, Libro → LibroDto
            CreateMap<Libro, LibroDto>()
                // Autors (entidades) → Autores (DTOs)
                .ForMember(dest => dest.Autores,
                           opt => opt.MapFrom(src => src.Autors))
                // Categoria (entidades) → Categorias (DTOs)
                .ForMember(dest => dest.Categorias,
                           opt => opt.MapFrom(src => src.Categoria))
                // Reseñas ordenadas
                .ForMember(dest => dest.Resenas,
                           opt => opt.MapFrom(src => src.Reseñas
                                                         .OrderByDescending(r => r.FechaReseña)));
        }   
    }
}
