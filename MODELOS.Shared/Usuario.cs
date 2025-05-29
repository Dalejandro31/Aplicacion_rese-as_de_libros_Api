using System;
using System.Collections.Generic;

namespace MODELOS.Shared;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public virtual ICollection<Reseña> Reseñas { get; set; } = new List<Reseña>();
}
