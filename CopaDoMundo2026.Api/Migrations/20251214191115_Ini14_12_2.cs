using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CopaDoMundo2026.Api.Migrations
{
    /// <inheritdoc />
    public partial class Ini14_12_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Jogos",
                columns: new[] { "Id", "BandeiraSelecaoA", "BandeiraSelecaoB", "DataHora", "Estadio", "Fase", "Grupo", "QtdGolsSelecaoA", "QtdGolsSelecaoB", "Rodada", "SelecaoA", "SelecaoB" },
                values: new object[] { 1, "https://s.sde.globo.com/media/organizations/2019/07/15/Argentina.svg", "https://s.sde.globo.com/media/organizations/2019/09/02/Australia.svg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", (short)0, "Grupo A", null, null, "1ª RODADA", "Argentina", "Australia" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Jogos",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
