using System;
using System.Collections.Generic;

namespace Modelos;

public partial class Libro
{
    public int LibroId { get; set; }

    public string Titulo { get; set; } = null!;

    public string? Resumen { get; set; }

    public DateOnly? FechaPublicacion { get; set; }

    public virtual ICollection<Reseña> Reseñas { get; set; } = new List<Reseña>();

    public virtual ICollection<Autore> Autors { get; set; } = new List<Autore>();

    public virtual ICollection<Categoria> Categoria { get; set; } = new List<Categoria>();
}
