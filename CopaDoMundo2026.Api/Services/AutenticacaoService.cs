using CopaMundo2026.Context;
using CopaMundo2026.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CopaMundo2026.Services;

public class AutenticacaoService
{
    private readonly AppDbContext _db;

    public AutenticacaoService(AppDbContext db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
    }

    public async Task<(bool sucesso, string mensagem)> RegistrarAsync(RegistroDTO registro)
    {
        var existe = await _db.Usuarios.AnyAsync(u => u.NomeUsuario.ToLower() == registro.NomeUsuario.ToLower());
        if (existe) return (false, "⚽ Este usuário já está em campo! Escolha outro.");

        var usuario = new Usuario
        {
            NomeUsuario = registro.NomeUsuario,
            SenhaHash = HashSenha(registro.Senha),
            DataCriacao = DateTime.UtcNow
        };

        _db.Usuarios.Add(usuario);
        await _db.SaveChangesAsync();

        return (true, "🎉 Cadastro realizado com sucesso! Bem-vindo ao time!");
    }

    public async Task<(bool sucesso, string mensagem, Usuario? usuario)> LoginAsync(LoginDTO login)
    {
        var usuario = await _db.Usuarios.FirstOrDefaultAsync(u => u.NomeUsuario.ToLower() == login.Usuario.ToLower());
        if (usuario == null)
            return (false, "❌ Usuário não encontrado!", null);

        if (usuario.SenhaHash != HashSenha(login.Senha)) 
            return (false, "❌ Senha incorreta! Tente novamente.", null);

        usuario.UltimoAcesso = DateTime.UtcNow;
        await _db.SaveChangesAsync();

        return (true, $"⚽ Gooool! Bem-vindo de volta, {usuario.NomeUsuario}!", usuario);
    }

    private string HashSenha(string senha)
    {
        using var sha256 = System.Security.Cryptography.SHA256.Create();
        var bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
        return Convert.ToBase64String(bytes);
    }
}
