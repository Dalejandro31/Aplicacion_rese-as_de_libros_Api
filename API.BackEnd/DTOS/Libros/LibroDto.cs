using API.BackEnd.DTOS.Autores;
using API.BackEnd.DTOS.Categorias;
using API.BackEnd.DTOS.Reseñas;

namespace API.BackEnd.DTOS.Libros
{
    public class LibroDto
    {
        public int LibroId { get; set; }
        public string Titulo { get; set; }
        public string Resumen { get; set; }
        public DateOnly? FechaPublicacion { get; set; }
        public List<AutorDto> Autores { get; set; }
        public List<CategoriaDto> Categorias { get; set; }
        public List<ResenaDto> Resenas { get; set; }
    }
}
