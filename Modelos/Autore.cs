using System;
using System.Collections.Generic;

namespace Modelos;

public partial class Autore
{
    public int AutorId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Biografia { get; set; }

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
