

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
            #region 1° Rodada
            new Jogo()
            {
                Id = 1,
                Fase = JogoEnum.PartidaEnum.FaseDeGrupos,
                Rodada = "1ª RODADA",
                Grupo = Constantes.GrupoA,
                Estadio= Constantes.BanorteEstadio,
                DataHora = new DateTime(2026, 6, 11, 16, 0, 0, DateTimeKind.Utc),
                SelecaoA = Constantes.MexicoSelecao,
                BandeiraSelecaoA = Constantes.MexicoBandeira,
                SelecaoB = Constantes.AfricaDoSulSelecao,
                BandeiraSelecaoB = Constantes.AfricaDoSulBandeira,
                QtdGolsSelecaoA = default,
                QtdGolsSelecaoB = default
            },
            new Jogo()
            {
                Id = 2,
                Fase = JogoEnum.PartidaEnum.FaseDeGrupos,
                Rodada = "1ª RODADA",
                Grupo = Constantes.GrupoA,
                Estadio= Constantes.AkronEstadio,
                DataHora = new DateTime(2026, 6, 11, 23, 0, 0, DateTimeKind.Utc),
                SelecaoA = Constantes.CoreiaDoSulSelecao,
                BandeiraSelecaoA = Constantes.CoreiaDoSulBandeira,
                SelecaoB = Constantes.EuropaDSelecao,
                BandeiraSelecaoB = Constantes.EuropaDBandeira,
                QtdGolsSelecaoA = default,
                QtdGolsSelecaoB = default
            },
            new Jogo()
            {
                Id = 3,
                Fase = JogoEnum.PartidaEnum.FaseDeGrupos,
                Rodada = "1ª RODADA",
                Grupo = Constantes.GrupoB,
                Estadio= Constantes.BMOEstadio,
                DataHora = new DateTime(2026, 6, 12, 16, 0, 0, DateTimeKind.Utc),
                SelecaoA = Constantes.CanadaSelecao,
                BandeiraSelecaoA = Constantes.CanadaBandeira,
                SelecaoB = Constantes.EuropaASelecao,
                BandeiraSelecaoB = Constantes.EuropaDBandeira,
                QtdGolsSelecaoA = default,
                QtdGolsSelecaoB = default
            },
            new Jogo()
            {
                Id = 4,
                Fase = JogoEnum.PartidaEnum.FaseDeGrupos,
                Rodada = "1ª RODADA",
                Grupo = Constantes.GrupoB,
                Estadio= Constantes.LevisEstadio,
                DataHora = new DateTime(2026, 6, 13, 16, 0, 0, DateTimeKind.Utc),
                SelecaoA = Constantes.CatarSelecao,
                BandeiraSelecaoA = Constantes.CatarBandeira,
                SelecaoB = Constantes.SuicaSelecao,
                BandeiraSelecaoB = Constantes.SuicaBandeira,
                QtdGolsSelecaoA = default,
                QtdGolsSelecaoB = default
            },
            new Jogo()
            {
                Id = 5,
                Fase = JogoEnum.PartidaEnum.FaseDeGrupos,
                Rodada = "1ª RODADA",
                Grupo = Constantes.GrupoC,
                Estadio= Constantes.MetLifeEstadio,
                DataHora = new DateTime(2026, 6, 13, 19, 0, 0, DateTimeKind.Utc),
                SelecaoA = Constantes.BrasilSelecao,
                BandeiraSelecaoA = Constantes.BrasilBandeira,
                SelecaoB = Constantes.MarrocosSelecao,
                BandeiraSelecaoB = Constantes.MarrocosBandeira,
                QtdGolsSelecaoA = default,
                QtdGolsSelecaoB = default
            },
            new Jogo()
            {
                Id = 6,
                Fase = JogoEnum.PartidaEnum.FaseDeGrupos,
                Rodada = "1ª RODADA",
                Grupo = Constantes.GrupoC,
                Estadio= Constantes.GilletteEstadio,
                DataHora = new DateTime(2026, 6, 13, 22, 0, 0, DateTimeKind.Utc),
                SelecaoA = Constantes.HaitiSelecao,
                BandeiraSelecaoA = Constantes.HaitiBandeira,
                SelecaoB = Constantes.EscociaSelecao,
                BandeiraSelecaoB = Constantes.EscociaBandeira,
                QtdGolsSelecaoA = default,
                QtdGolsSelecaoB = default
            },
            new Jogo()
            {
                Id = 7,
                Fase = JogoEnum.PartidaEnum.FaseDeGrupos,
                Rodada = "1ª RODADA",
                Grupo = Constantes.GrupoD,
                Estadio= Constantes.SoFiEstadio,
                DataHora = new DateTime(2026, 6, 12, 22, 0, 0, DateTimeKind.Utc),
                SelecaoA = Constantes.EstadosUnidosSelecao,
                BandeiraSelecaoA = Constantes.EstadosUnidosBandeira,
                SelecaoB = Constantes.ParaguaiSelecao,
                BandeiraSelecaoB = Constantes.ParaguaiBandeira,
                QtdGolsSelecaoA = default,
                QtdGolsSelecaoB = default
            },
            new Jogo()
            {
                Id = 8,
                Fase = JogoEnum.PartidaEnum.FaseDeGrupos,
                Rodada = "1ª RODADA",
                Grupo = Constantes.GrupoD,
                Estadio= Constantes.BCPlaceEstadio,
                DataHora = new DateTime(2026, 6, 13, 1, 0, 0, DateTimeKind.Utc),
                SelecaoA = Constantes.AustraliaSelecao,
                BandeiraSelecaoA = Constantes.AfricaDoSulBandeira,
                SelecaoB = Constantes.EuropaCSelecao,
                BandeiraSelecaoB = Constantes.EuropaDBandeira,
                QtdGolsSelecaoA = default,
                QtdGolsSelecaoB = default
            },
            new Jogo()
            {
                Id = 9,
                Fase = JogoEnum.PartidaEnum.FaseDeGrupos,
                Rodada = "1ª RODADA",
                Grupo = Constantes.GrupoE,
                Estadio= Constantes.NRGEstadio,
                DataHora = new DateTime(2026, 6, 14, 14, 0, 0, DateTimeKind.Utc),
                SelecaoA = Constantes.AlemanhaSelecao,
                BandeiraSelecaoA = Constantes.AlemanhaBandeira,
                SelecaoB = Constantes.CuracaoSelecao,
                BandeiraSelecaoB = Constantes.CuracaoBandeira,
                QtdGolsSelecaoA = default,
                QtdGolsSelecaoB = default
            },
            new Jogo()
            {
                Id = 10,
                Fase = JogoEnum.PartidaEnum.FaseDeGrupos,
                Rodada = "1ª RODADA",
                Grupo = Constantes.GrupoE,
                Estadio= Constantes.LincolnEstadio,
                DataHora = new DateTime(2026, 6, 14, 20, 0, 0, DateTimeKind.Utc),
                SelecaoA = Constantes.CostaDoMarfimSelecao,
                BandeiraSelecaoA = Constantes.CostaDoMarfimBandeira,
                SelecaoB = Constantes.EquadorSelecao,
                BandeiraSelecaoB = Constantes.EquadorBandeira,
                QtdGolsSelecaoA = default,
                QtdGolsSelecaoB = default
            },
            new Jogo()
            {
                Id = 11,
                Fase = JogoEnum.PartidaEnum.FaseDeGrupos,
                Rodada = "1ª RODADA",
                Grupo = Constantes.GrupoF,
                Estadio= Constantes.ATTEstadio,
                DataHora = new DateTime(2026, 6, 14, 17, 0, 0, DateTimeKind.Utc),
                SelecaoA = Constantes.HolandaSelecao,
                BandeiraSelecaoA = Constantes.HolandaBandeira,
                SelecaoB = Constantes.JapaoSelecao,
                BandeiraSelecaoB = Constantes.JapaoBandeira,
                QtdGolsSelecaoA = default,
                QtdGolsSelecaoB = default
            },
            new Jogo()
            {
                Id = 12,
                Fase = JogoEnum.PartidaEnum.FaseDeGrupos,
                Rodada = "1ª RODADA",
                Grupo = Constantes.GrupoF,
                Estadio= Constantes.BBVAEstadio,
                DataHora = new DateTime(2026, 6, 14, 23, 0, 0, DateTimeKind.Utc),
                SelecaoA = Constantes.EuropaBSelecao,
                BandeiraSelecaoA = Constantes.EuropaDBandeira,
                SelecaoB = Constantes.TunisiaSelecao,
                BandeiraSelecaoB = Constantes.TunisiaBandeira,
                QtdGolsSelecaoA = default,
                QtdGolsSelecaoB = default
            },
            new Jogo()
            {
                Id = 13,
                Fase = JogoEnum.PartidaEnum.FaseDeGrupos,
                Rodada = "1ª RODADA",
                Grupo = Constantes.GrupoG,
                Estadio= Constantes.LumenEstadio,
                DataHora = new DateTime(2026, 6, 15, 16, 0, 0, DateTimeKind.Utc),
                SelecaoA = Constantes.BelgicaSelecao,
                BandeiraSelecaoA = Constantes.BelgicaBandeira,
                SelecaoB = Constantes.EgitoSelecao,
                BandeiraSelecaoB = Constantes.EgitoBandeira,
                QtdGolsSelecaoA = default,
                QtdGolsSelecaoB = default
            },
            new Jogo()
            {
                Id = 14,
                Fase = JogoEnum.PartidaEnum.FaseDeGrupos,
                Rodada = "1ª RODADA",
                Grupo = Constantes.GrupoG,
                Estadio= Constantes.SoFiEstadio,
                DataHora = new DateTime(2026, 6, 15, 22, 0, 0, DateTimeKind.Utc),
                SelecaoA = Constantes.IraSelecao,
                BandeiraSelecaoA = Constantes.IraBandeira,
                SelecaoB = Constantes.NovaZelandiaSelecao,
                BandeiraSelecaoB = Constantes.NovaZelandiaBandeira,
                QtdGolsSelecaoA = default,
                QtdGolsSelecaoB = default
            },
            new Jogo()
            {
                Id = 15,
                Fase = JogoEnum.PartidaEnum.FaseDeGrupos,
                Rodada = "1ª RODADA",
                Grupo = Constantes.GrupoH,
                Estadio= Constantes.MercedesEstadio,
                DataHora = new DateTime(2026, 6, 15, 13, 0, 0, DateTimeKind.Utc),
                SelecaoA = Constantes.EspanhaSelecao,
                BandeiraSelecaoA = Constantes.EspanhaBandeira,
                SelecaoB = Constantes.CaboVerdeSelecao,
                BandeiraSelecaoB = Constantes.CaboVerdeBandeira,
                QtdGolsSelecaoA = default,
                QtdGolsSelecaoB = default
            },
            new Jogo()
            {
                Id = 16,
                Fase = JogoEnum.PartidaEnum.FaseDeGrupos,
                Rodada = "1ª RODADA",
                Grupo = Constantes.GrupoH,
                Estadio= Constantes.HardRockEstadio,
                DataHora = new DateTime(2026, 6, 15, 19, 0, 0, DateTimeKind.Utc),
                SelecaoA = Constantes.ArabiaSauditaSelecao,
                BandeiraSelecaoA = Constantes.ArabiaSauditaBandeira,
                SelecaoB = Constantes.UruguaiSelecao,
                BandeiraSelecaoB = Constantes.UruguaiBandeira,
                QtdGolsSelecaoA = default,
                QtdGolsSelecaoB = default
            },
            new Jogo()
            {
                Id = 17,
                Fase = JogoEnum.PartidaEnum.FaseDeGrupos,
                Rodada = "1ª RODADA",
                Grupo = Constantes.GrupoI,
                Estadio= Constantes.MetLifeEstadio,
                DataHora = new DateTime(2026, 6, 16, 16, 0, 0, DateTimeKind.Utc),
                SelecaoA = Constantes.FrancaSelecao,
                BandeiraSelecaoA = Constantes.FrancaBandeira,
                SelecaoB = Constantes.SenegalSelecao,
                BandeiraSelecaoB = Constantes.SenegalBandeira,
                QtdGolsSelecaoA = default,
                QtdGolsSelecaoB = default
            },
            new Jogo()
            {
                Id = 18,
                Fase = JogoEnum.PartidaEnum.FaseDeGrupos,
                Rodada = "1ª RODADA",
                Grupo = Constantes.GrupoI,
                Estadio= Constantes.GilletteEstadio,
                DataHora = new DateTime(2026, 6, 16, 19, 0, 0, DateTimeKind.Utc),
                SelecaoA = Constantes.Intercontinental2Selecao,
                BandeiraSelecaoA = Constantes.Intercontinental2Bandeira,
                SelecaoB = Constantes.NoruegaSelecao,
                BandeiraSelecaoB = Constantes.NoruegaBandeira,
                QtdGolsSelecaoA = default,
                QtdGolsSelecaoB = default
            },
            new Jogo()
            {
                Id = 19,
                Fase = JogoEnum.PartidaEnum.FaseDeGrupos,
                Rodada = "1ª RODADA",
                Grupo = Constantes.GrupoJ,
                Estadio= Constantes.ArrowheadEstadio,
                DataHora = new DateTime(2026, 6, 16, 22, 0, 0, DateTimeKind.Utc),
                SelecaoA = Constantes.ArgentinaSelecao,
                BandeiraSelecaoA = Constantes.ArgentinaBandeira,
                SelecaoB = Constantes.ArgeliaSelecao,
                BandeiraSelecaoB = Constantes.ArgeliaBandeira,
                QtdGolsSelecaoA = default,
                QtdGolsSelecaoB = default
            },
            new Jogo()
            {
                Id = 20,
                Fase = JogoEnum.PartidaEnum.FaseDeGrupos,
                Rodada = "1ª RODADA",
                Grupo = Constantes.GrupoJ,
                Estadio= Constantes.LevisEstadio,
                DataHora = new DateTime(2026, 6, 17, 1, 0, 0, DateTimeKind.Utc),
                SelecaoA = Constantes.AustriaSelecao,
                BandeiraSelecaoA = Constantes.AustriaBandeira,
                SelecaoB = Constantes.JordaniaSelecao,
                BandeiraSelecaoB = Constantes.JordaniaBandeira,
                QtdGolsSelecaoA = default,
                QtdGolsSelecaoB = default
            },
            new Jogo()
            {
                Id = 21,
                Fase = JogoEnum.PartidaEnum.FaseDeGrupos,
                Rodada = "1ª RODADA",
                Grupo = Constantes.GrupoK,
                Estadio= Constantes.NRGEstadio,
                DataHora = new DateTime(2026, 6, 17, 14, 0, 0, DateTimeKind.Utc),
                SelecaoA = Constantes.PortugalSelecao,
                BandeiraSelecaoA = Constantes.PortugalBandeira,
                SelecaoB = Constantes.Intercontinental1Selecao,
                BandeiraSelecaoB = Constantes.Intercontinental2Bandeira,
                QtdGolsSelecaoA = default,
                QtdGolsSelecaoB = default
            },
            new Jogo()
            {
                Id = 22,
                Fase = JogoEnum.PartidaEnum.FaseDeGrupos,
                Rodada = "1ª RODADA",
                Grupo = Constantes.GrupoK,
                Estadio= Constantes.AztecaEstadio,
                DataHora = new DateTime(2026, 6, 17, 23, 0, 0, DateTimeKind.Utc),
                SelecaoA = Constantes.UzbequistaoSelecao,
                BandeiraSelecaoA = Constantes.UzbequistaoBandeira,
                SelecaoB = Constantes.ColombiaSelecao,
                BandeiraSelecaoB = Constantes.ColombiaBandeira,
                QtdGolsSelecaoA = default,
                QtdGolsSelecaoB = default
            },
            new Jogo()
            {
                Id = 23,
                Fase = JogoEnum.PartidaEnum.FaseDeGrupos,
                Rodada = "1ª RODADA",
                Grupo = Constantes.GrupoL,
                Estadio= Constantes.ATTEstadio,
                DataHora = new DateTime(2026, 6, 17, 17, 0, 0, DateTimeKind.Utc),
                SelecaoA = Constantes.InglaterraSelecao,
                BandeiraSelecaoA = Constantes.InglaterraBandeira,
                SelecaoB = Constantes.CroaciaSelecao,
                BandeiraSelecaoB = Constantes.CroaciaBandeira,
                QtdGolsSelecaoA = default,
                QtdGolsSelecaoB = default
            },
            new Jogo()
            {
                Id = 24,
                Fase = JogoEnum.PartidaEnum.FaseDeGrupos,
                Rodada = "1ª RODADA",
                Grupo = Constantes.GrupoL,
                Estadio= Constantes.BMOEstadio,
                DataHora = new DateTime(2026, 6, 17, 20, 0, 0, DateTimeKind.Utc),
                SelecaoA = Constantes.GanaSelecao,
                BandeiraSelecaoA = Constantes.GanaBandeira,
                SelecaoB = Constantes.PanamaSelecao,
                BandeiraSelecaoB = Constantes.PanamaBandeira,
                QtdGolsSelecaoA = default,
                QtdGolsSelecaoB = default
            },
            #endregion
        });
    }
}
