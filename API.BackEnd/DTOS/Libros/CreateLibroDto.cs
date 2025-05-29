namespace API.BackEnd.DTOS.Libros
{
    public class CreateLibroDto
    {
        public string Titulo { get; set; }
        public string Resumen { get; set; }
        public DateOnly? FechaPublicacion { get; set; }
        public List<int> AutorIds { get; set; }
        public List<int> CategoriaIds { get; set; }
    }
}
