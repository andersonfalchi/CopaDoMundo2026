using System.ComponentModel.DataAnnotations;

namespace CopaMundo2026.Models;

public class RegistroDTO
{
    [Required(ErrorMessage = "O nome de usuário é obrigatório")]
    [MinLength(3, ErrorMessage = "O nome de usuário deve ter no mínimo 3 caracteres")]
    [MaxLength(20, ErrorMessage = "O nome de usuário deve ter no máximo 20 caracteres")]
    public string NomeUsuario { get; set; } = string.Empty;

    [Required(ErrorMessage = "A senha é obrigatória")]
    [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres")]
    public string Senha { get; set; } = string.Empty;

    [Required(ErrorMessage = "Confirme sua senha")]
    [Compare("Senha", ErrorMessage = "As senhas não coincidem")]
    public string ConfirmaSenha { get; set; } = string.Empty;
}
