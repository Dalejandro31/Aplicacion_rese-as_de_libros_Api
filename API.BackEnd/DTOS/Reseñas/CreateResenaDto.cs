namespace API.BackEnd.DTOS.Reseñas
{
    public class CreateResenaDto
    {
        public int UsuarioId { get; set; }
        public int LibroId { get; set; }
        public int Calificacion { get; set; }
        public string Comentario { get; set; }
    }
}
