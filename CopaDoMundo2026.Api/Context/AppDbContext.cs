

using CopaDoMundo.Models.Models;
using CopaMundo2026.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace CopaMundo2026.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Jogo> Jogo { get; set; }
        public DbSet<Palpite> Palpite { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.NomeUsuario).IsRequired().HasMaxLength(50);
                entity.Property(e => e.SenhaHash).IsRequired();
                entity.Property(e => e.DataCriacao).IsRequired();
                entity.HasIndex(e => e.NomeUsuario).IsUnique();
            });

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
    }
}
