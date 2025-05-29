namespace API.BackEnd.DTOS.Usuarios
{
    public class UsuarioDto
    {
        public int UsuarioId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
