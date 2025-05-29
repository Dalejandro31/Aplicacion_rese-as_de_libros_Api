namespace API.BackEnd.DTOS.Reseñas
{
    public class ResenaDto
    {
        public int ResenaId { get; set; }
        public int UsuarioId { get; set; }
        public int LibroId { get; set; }
        public int Calificacion { get; set; }
        public string Comentario { get; set; }
        public DateTime FechaResena { get; set; }
        public string Username { get; set; } // para mostrar autor de la reseña
    }
}
 