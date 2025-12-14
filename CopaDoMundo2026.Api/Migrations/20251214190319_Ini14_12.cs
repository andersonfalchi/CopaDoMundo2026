using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CopaDoMundo2026.Api.Migrations
{
    /// <inheritdoc />
    public partial class Ini14_12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jogos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Fase = table.Column<short>(type: "smallint", nullable: false),
                    Rodada = table.Column<string>(type: "text", nullable: false),
                    Grupo = table.Column<string>(type: "text", nullable: false),
                    Estadio = table.Column<string>(type: "text", nullable: false),
                    DataHora = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    SelecaoA = table.Column<string>(type: "text", nullable: false),
                    BandeiraSelecaoA = table.Column<string>(type: "text", nullable: false),
                    SelecaoB = table.Column<string>(type: "text", nullable: false),
                    BandeiraSelecaoB = table.Column<string>(type: "text", nullable: false),
                    QtdGolsSelecaoA = table.Column<int>(type: "integer", nullable: true),
                    QtdGolsSelecaoB = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jogos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NomeUsuario = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SenhaHash = table.Column<string>(type: "text", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UltimoAcesso = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Pago = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Palpites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    JogoId = table.Column<int>(type: "integer", nullable: false),
                    QtdGolsSelecaoA = table.Column<int>(type: "integer", nullable: false),
                    QtdGolsSelecaoB = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Palpites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Palpites_Jogos_JogoId",
                        column: x => x.JogoId,
                        principalTable: "Jogos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Palpites_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Palpites_JogoId",
                table: "Palpites",
                column: "JogoId");

            migrationBuilder.CreateIndex(
                name: "IX_Palpites_UsuarioId",
                table: "Palpites",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_NomeUsuario",
                table: "Usuarios",
                column: "NomeUsuario",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Palpites");

            migrationBuilder.DropTable(
                name: "Jogos");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
