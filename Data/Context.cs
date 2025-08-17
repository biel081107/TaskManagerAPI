namespace Notiom.Data;

using Microsoft.EntityFrameworkCore;
using Notiom.Models;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options) { }

    public DbSet<Usuarios> Usuarios { get; set; }
    public DbSet<Tarefas> Tarefas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuarios>(
            entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(300);
                entity.Property(e => e.Senha).IsRequired().HasMaxLength(300);
            }
        );
        modelBuilder.Entity<Tarefas>(
            entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Titulo).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Descricao).IsRequired().HasMaxLength(500);
                entity.Property(e => e.DataCriacao).HasColumnType("Date");
                entity.Property(e => e.Status).IsRequired().HasDefaultValue(0);
            }
        );

        modelBuilder.Entity<Tarefas>()
            .HasOne<Usuarios>()
            .WithMany()
            .HasForeignKey(t => t.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        
    } 
}