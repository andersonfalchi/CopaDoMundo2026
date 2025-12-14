using CopaDoMundo.Models.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace CopaMundo2026.Models;

public class Usuario
{
    public int Id { get; set; }

    public string NomeUsuario { get; set; } = string.Empty;

    public string SenhaHash { get; set; } = string.Empty;

    public DateTime DataCriacao { get; set; } = DateTime.Now;

    public DateTime UltimoAcesso { get; set; } = DateTime.Now;

    public bool Pago { get; set; } = false;

    [NotMapped]
    public string PagoStatus => Pago ? "Sim" : "Não";

    public virtual List<Palpite> Palpites { get; set; } = new List<Palpite>();
}
