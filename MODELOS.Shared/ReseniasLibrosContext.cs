using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MODELOS.Shared;

public partial class ReseniasLibrosContext : DbContext
{
    public ReseniasLibrosContext()
    {
    }

    public ReseniasLibrosContext(DbContextOptions<ReseniasLibrosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Autore> Autores { get; set; }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Libro> Libros { get; set; }

    public virtual DbSet<Reseña> Reseñas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:servidornuevo2.database.windows.net,1433;Initial Catalog=LibrosResenas2;Persist Security Info=False;User ID=admin1;Password=Brunoymegan123*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Autore>(entity =>
        {
            entity.HasKey(e => e.AutorId).HasName("PK__Autores__F58AE92951A8E8B2");

            entity.Property(e => e.Biografia).IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.CategoriaId).HasName("PK__Categori__F353C1E5668BC0DE");

            entity.HasIndex(e => e.Nombre, "UQ__Categori__75E3EFCF2CEA1E1B").IsUnique();

            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Libro>(entity =>
        {
            entity.HasKey(e => e.LibroId).HasName("PK__Libros__35A1ECED573C1A75");

            entity.Property(e => e.Resumen).IsUnicode(false);
            entity.Property(e => e.Titulo)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasMany(d => d.Autors).WithMany(p => p.Libros)
                .UsingEntity<Dictionary<string, object>>(
                    "LibroAutor",
                    r => r.HasOne<Autore>().WithMany()
                        .HasForeignKey("AutorId")
                        .HasConstraintName("FK_LibroAutor_Autor"),
                    l => l.HasOne<Libro>().WithMany()
                        .HasForeignKey("LibroId")
                        .HasConstraintName("FK_LibroAutor_Libro"),
                    j =>
                    {
                        j.HasKey("LibroId", "AutorId").HasName("PK__LibroAut__BAF9427F3178F940");
                        j.ToTable("LibroAutor");
                    });

            entity.HasMany(d => d.Categoria).WithMany(p => p.Libros)
                .UsingEntity<Dictionary<string, object>>(
                    "LibroCategorium",
                    r => r.HasOne<Categoria>().WithMany()
                        .HasForeignKey("CategoriaId")
                        .HasConstraintName("FK_LibroCategoria_Categoria"),
                    l => l.HasOne<Libro>().WithMany()
                        .HasForeignKey("LibroId")
                        .HasConstraintName("FK_LibroCategoria_Libro"),
                    j =>
                    {
                        j.HasKey("LibroId", "CategoriaId").HasName("PK__LibroCat__7A94D0F33557BADF");
                        j.ToTable("LibroCategoria");
                    });
        });

        modelBuilder.Entity<Reseña>(entity =>
        {
            entity.HasKey(e => e.ReseñaId).HasName("PK__Reseñas__B17323A6A1DCEF6B");

            entity.Property(e => e.Comentario).IsUnicode(false);
            entity.Property(e => e.FechaReseña).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.Libro).WithMany(p => p.Reseñas)
                .HasForeignKey(d => d.LibroId)
                .HasConstraintName("FK_Reseñas_Libro");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Reseñas)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK_Reseñas_Usuario");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuarios__2B3DE7B80F8CBA8A");

            entity.HasIndex(e => e.Username, "UQ__Usuarios__536C85E4A2574EA7").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Usuarios__A9D10534E6832394").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
