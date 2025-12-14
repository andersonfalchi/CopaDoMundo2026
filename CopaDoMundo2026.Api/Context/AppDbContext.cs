

using CopaDoMundo.Models.Models;
using CopaMundo2026.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace CopaMundo2026.Context;

public class AppDbContext : DbContext
{
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Jogo> Jogos { get; set; }
    public DbSet<Palpite> Palpites { get; set; }

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

        modelBuilder.Seed();

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
}

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Jogo>().HasData(new Jogo[]
        {
                new Jogo()
               {
                   Id = 1,
                   Fase = JogoEnum.PartidaEnum.FaseDeGrupos,
                   Rodada = "1ª RODADA",
                   Grupo = "Grupo A",
                   Estadio= "",
                   DataHora = default,
                   SelecaoA = Constantes.ArgentinaSelecao,
                   BandeiraSelecaoA = Constantes.ArgentinaBandeira,
                   SelecaoB = Constantes.AustraliaSelecao,
                   BandeiraSelecaoB = Constantes.AustraliaBandeira,
                   QtdGolsSelecaoA = default,
                   QtdGolsSelecaoB = default
               }

        });
    }
}
