using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CopaDoMundo2026.Api.Migrations
{
    /// <inheritdoc />
    public partial class Ini18_12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Jogos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BandeiraSelecaoA", "BandeiraSelecaoB", "DataHora", "Estadio", "SelecaoA", "SelecaoB" },
                values: new object[] { "https://s.sde.globo.com/media/organizations/2019/07/15/M%C3%A9xico.svg", "https://s.sde.globo.com/media/organizations/2019/09/09/África_do_Sul.svg", new DateTime(2026, 6, 11, 16, 0, 0, 0, DateTimeKind.Utc), "Banorte Stadium, Cidade do México", "Mexico", "África do Sul" });

            migrationBuilder.InsertData(
                table: "Jogos",
                columns: new[] { "Id", "BandeiraSelecaoA", "BandeiraSelecaoB", "DataHora", "Estadio", "Fase", "Grupo", "QtdGolsSelecaoA", "QtdGolsSelecaoB", "Rodada", "SelecaoA", "SelecaoB" },
                values: new object[,]
                {
                    { 2, "https://s.sde.globo.com/media/organizations/2019/09/01/Coreia_do_Sul.svg", "https://s.sde.globo.com/media/organizations/2025/12/03/Bandeira_UEFA_1NolDy2.svg", new DateTime(2026, 6, 11, 23, 0, 0, 0, DateTimeKind.Utc), "Estádio Akron, Zapopan", (short)0, "Grupo A", null, null, "1ª RODADA", "CoreiaDoSul", "Europa D" },
                    { 3, "https://s.sde.globo.com/media/organizations/2019/07/15/Canad%C3%A1.svg", "https://s.sde.globo.com/media/organizations/2025/12/03/Bandeira_UEFA_1NolDy2.svg", new DateTime(2026, 6, 12, 16, 0, 0, 0, DateTimeKind.Utc), "BMO Field, Toronto", (short)0, "Grupo B", null, null, "1ª RODADA", "Canada", "Europa A" },
                    { 4, "https://s.sde.globo.com/media/organizations/2019/09/06/Catar.svg", "https://storage.googleapis.com/bkt-gglobo-sde-hdg-prd-sdeadmin_prod/media/organizations/2019/09/15/Su%C3%AD%C3%A7a.svg", new DateTime(2026, 6, 13, 16, 0, 0, 0, DateTimeKind.Utc), "Levi's Stadium, Santa Clara", (short)0, "Grupo B", null, null, "1ª RODADA", "Catar", "Suica" },
                    { 5, "https://s.sde.globo.com/media/organizations/2019/07/16/Brasil_rgYHF6Z.svg", "https://s.sde.globo.com/media/organizations/2019/09/10/Marrocos.svg", new DateTime(2026, 6, 13, 19, 0, 0, 0, DateTimeKind.Utc), "MetLife Stadium, East Rutherford", (short)0, "Grupo C", null, null, "1ª RODADA", "Brasil", "Marrocos" },
                    { 6, "https://s.sde.globo.com/media/organizations/2019/07/14/Haiti.svg", "https://s.sde.globo.com/media/organizations/2019/09/15/Escócia.svg", new DateTime(2026, 6, 13, 22, 0, 0, 0, DateTimeKind.Utc), "Gillette Stadium, Foxborough", (short)0, "Grupo C", null, null, "1ª RODADA", "Haiti", "Escócia" },
                    { 7, "https://s.sde.globo.com/media/organizations/2019/07/16/Estados_Unidos.svg", "https://s.sde.globo.com/media/organizations/2019/07/15/Paraguai.svg", new DateTime(2026, 6, 12, 22, 0, 0, 0, DateTimeKind.Utc), "SoFi Stadium, Inglewood", (short)0, "Grupo D", null, null, "1ª RODADA", "EstadosUnidos", "Paraguai" },
                    { 8, "https://s.sde.globo.com/media/organizations/2019/09/09/África_do_Sul.svg", "https://s.sde.globo.com/media/organizations/2025/12/03/Bandeira_UEFA_1NolDy2.svg", new DateTime(2026, 6, 13, 1, 0, 0, 0, DateTimeKind.Utc), "BC Place, Vancouver", (short)0, "Grupo D", null, null, "1ª RODADA", "Australia", "Europa C" },
                    { 9, "https://s.sde.globo.com/media/organizations/2019/09/03/Alemanha.svg", "https://s.sde.globo.com/media/organizations/2019/07/14/Curaçao.svg", new DateTime(2026, 6, 14, 14, 0, 0, 0, DateTimeKind.Utc), "NRG Stadium, Houston", (short)0, "Grupo E", null, null, "1ª RODADA", "Alemanha", "Curaçao" },
                    { 10, "https://s.sde.globo.com/media/organizations/2019/09/03/Costa_do_Marfim.svg", "https://s.sde.globo.com/media/organizations/2019/07/15/Equador.svg", new DateTime(2026, 6, 14, 20, 0, 0, 0, DateTimeKind.Utc), "Lincoln Financial Field, Filadélfia", (short)0, "Grupo E", null, null, "1ª RODADA", "Costa do Marfim", "Equador" },
                    { 11, "https://s.sde.globo.com/media/organizations/2019/09/16/Holanda.svg", "https://s.sde.globo.com/media/organizations/2019/09/03/Japao.svg", new DateTime(2026, 6, 14, 17, 0, 0, 0, DateTimeKind.Utc), "AT&T Stadium, Arlington", (short)0, "Grupo F", null, null, "1ª RODADA", "Holanda", "Japao" },
                    { 12, "https://s.sde.globo.com/media/organizations/2025/12/03/Bandeira_UEFA_1NolDy2.svg", "https://s.sde.globo.com/media/organizations/2018/03/10/tunisia.svg", new DateTime(2026, 6, 14, 23, 0, 0, 0, DateTimeKind.Utc), "Estadio BBVA Bancomer, Guadaloupe", (short)0, "Grupo F", null, null, "1ª RODADA", "Europa B", "Tunisia" },
                    { 13, "https://s.sde.globo.com/media/organizations/2019/09/15/B%C3%A9lgica.svg", "https://s.sde.globo.com/media/organizations/2019/09/08/Egito.svg", new DateTime(2026, 6, 15, 16, 0, 0, 0, DateTimeKind.Utc), "Lumen Field, Seattle", (short)0, "Grupo G", null, null, "1ª RODADA", "Belgica", "Egito" },
                    { 14, "https://s.sde.globo.com/media/organizations/2019/09/01/Ir%C3%A3.svg", "https://s.sde.globo.com/media/organizations/2019/09/01/Nova_Zelandia.svg", new DateTime(2026, 6, 15, 22, 0, 0, 0, DateTimeKind.Utc), "SoFi Stadium, Inglewood", (short)0, "Grupo G", null, null, "1ª RODADA", "Ira", "Nova Zelândia" },
                    { 15, "https://s.sde.globo.com/media/organizations/2019/09/03/Espanha.svg", "https://s.sde.globo.com/media/organizations/2019/09/08/Cabo_Verde.svg", new DateTime(2026, 6, 15, 13, 0, 0, 0, DateTimeKind.Utc), "Mercedes-Benz Stadium, Atlanta", (short)0, "Grupo H", null, null, "1ª RODADA", "Espanha", "Cabo Verde" },
                    { 16, "https://s.sde.globo.com/media/organizations/2019/09/05/Ar%C3%A1bia_Saudita.svg", "https://s.sde.globo.com/media/organizations/2019/07/16/Uruguai.svg", new DateTime(2026, 6, 15, 19, 0, 0, 0, DateTimeKind.Utc), "Hard Rock Stadium, Miami Gardens", (short)0, "Grupo H", null, null, "1ª RODADA", "ArabiaSaudita", "Uruguai" },
                    { 17, "https://s.sde.globo.com/media/organizations/2019/09/03/Fran%C3%A7a.svg", "https://s.sde.globo.com/media/organizations/2019/09/03/Senegal.svg", new DateTime(2026, 6, 16, 16, 0, 0, 0, DateTimeKind.Utc), "MetLife Stadium, East Rutherford", (short)0, "Grupo I", null, null, "1ª RODADA", "Franca", "Senegal" },
                    { 18, "https://s.sde.globo.com/media/organizations/2025/12/03/Bandeira_FIFA.svg", "https://s.sde.globo.com/media/organizations/2019/09/15/Noruega.svg", new DateTime(2026, 6, 16, 19, 0, 0, 0, DateTimeKind.Utc), "Gillette Stadium, Foxborough", (short)0, "Grupo I", null, null, "1ª RODADA", "Intercontinental 2", "Noruega" },
                    { 19, "https://s.sde.globo.com/media/organizations/2019/07/15/Argentina.svg", "https://s.sde.globo.com/media/organizations/2019/09/08/Argélia.svg", new DateTime(2026, 6, 16, 22, 0, 0, 0, DateTimeKind.Utc), "Arrowhead Stadium, Kansas City", (short)0, "Grupo J", null, null, "1ª RODADA", "Argentina", "Argélia" },
                    { 20, "https://s.sde.globo.com/media/organizations/2019/09/17/Áustria.svg", "https://s.sde.globo.com/media/organizations/2019/09/03/Jordania.svg", new DateTime(2026, 6, 17, 1, 0, 0, 0, DateTimeKind.Utc), "Levi's Stadium, Santa Clara", (short)0, "Grupo J", null, null, "1ª RODADA", "Áustria", "Jordânia" },
                    { 21, "https://s.sde.globo.com/media/organizations/2019/09/17/Portugal.svg", "https://s.sde.globo.com/media/organizations/2025/12/03/Bandeira_FIFA.svg", new DateTime(2026, 6, 17, 14, 0, 0, 0, DateTimeKind.Utc), "NRG Stadium, Houston", (short)0, "Grupo K", null, null, "1ª RODADA", "Portugal", "Intercontinental 1" },
                    { 22, "https://s.sde.globo.com/media/organizations/2019/09/08/Uzbequistão.svg", "https://s.sde.globo.com/media/organizations/2019/07/14/Colombia.svg", new DateTime(2026, 6, 17, 23, 0, 0, 0, DateTimeKind.Utc), "Estadio Azteca, Cidade do México", (short)0, "Grupo K", null, null, "1ª RODADA", "Uzbequistão", "Colômbia" },
                    { 23, "https://s.sde.globo.com/media/organizations/2019/09/13/Inglaterra.svg", "https://s.sde.globo.com/media/organizations/2019/09/16/Cro%C3%A1cia.svg", new DateTime(2026, 6, 17, 17, 0, 0, 0, DateTimeKind.Utc), "AT&T Stadium, Arlington", (short)0, "Grupo L", null, null, "1ª RODADA", "Inglaterra", "Croacia" },
                    { 24, "https://s.sde.globo.com/media/organizations/2019/09/08/Gana.svg", "https://s.sde.globo.com/media/organizations/2019/07/16/Panamá.svg", new DateTime(2026, 6, 17, 20, 0, 0, 0, DateTimeKind.Utc), "BMO Field, Toronto", (short)0, "Grupo L", null, null, "1ª RODADA", "Gana", "Panamá" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Jogos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Jogos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Jogos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Jogos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Jogos",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Jogos",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Jogos",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Jogos",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Jogos",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Jogos",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Jogos",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Jogos",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Jogos",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Jogos",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Jogos",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Jogos",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Jogos",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Jogos",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Jogos",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Jogos",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Jogos",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Jogos",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Jogos",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.UpdateData(
                table: "Jogos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BandeiraSelecaoA", "BandeiraSelecaoB", "DataHora", "Estadio", "SelecaoA", "SelecaoB" },
                values: new object[] { "https://s.sde.globo.com/media/organizations/2019/07/15/Argentina.svg", "https://s.sde.globo.com/media/organizations/2019/09/02/Australia.svg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Argentina", "Australia" });
        }
    }
}
