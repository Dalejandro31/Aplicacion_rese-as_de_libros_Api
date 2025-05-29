using System;
using System.Collections.Generic;

namespace Modelos;

public partial class Reseña
{
    public int ReseñaId { get; set; }

    public int UsuarioId { get; set; }

    public int LibroId { get; set; }

    public byte Calificación { get; set; }

    public string? Comentario { get; set; }

    public DateTime FechaReseña { get; set; }

    public virtual Libro Libro { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
