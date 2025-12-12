using CopaDoMundo2026.Api.Exceptions;
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

    public async Task<Usuario> RegistrarAsync(RegistroDTO registro)
    {
        try
        {
            var existe = await _db.Usuarios
                .AnyAsync(u => u.NomeUsuario.ToLower() == registro.NomeUsuario.ToLower());

            if (existe)
            {
                throw new BusinessException(
                    "⚽ Este usuário já está em campo! Escolha outro.",
                    "USUARIO_JA_EXISTE"
                );
            }

            var usuario = new Usuario
            {
                NomeUsuario = registro.NomeUsuario,
                SenhaHash = HashSenha(registro.Senha),
                DataCriacao = DateTime.UtcNow
            };

            _db.Usuarios.Add(usuario);
            await _db.SaveChangesAsync();

            return usuario;
        }
        catch (DbUpdateException ex)
        {
            throw new DatabaseException(
                "Erro ao salvar usuário no banco de dados. Tente novamente."
            );
        }
    }

    public async Task<Usuario> LoginAsync(LoginDTO login)
    {
        try
        {
            var usuario = await _db.Usuarios
                .FirstOrDefaultAsync(u => u.NomeUsuario.ToLower() == login.Usuario.ToLower());

            if (usuario == null)
            {
                throw new BusinessException(
                    "❌ Usuário não encontrado!",
                    "USUARIO_NAO_ENCONTRADO"
                );
            }

            if (usuario.SenhaHash != HashSenha(login.Senha))
            {
                throw new BusinessException(
                    "❌ Senha incorreta! Tente novamente.",
                    "SENHA_INCORRETA"
                );
            }

            usuario.UltimoAcesso = DateTime.UtcNow;
            await _db.SaveChangesAsync();

            return usuario;
        }
        catch (BusinessException)
        {
            // Re-lança BusinessException sem modificar
            throw;
        }
        catch (DbUpdateException ex)
        {
            throw new DatabaseException(
                "Erro ao acessar banco de dados durante login."
            );
        }
    }

    public async Task<Usuario> BuscarPorIdAsync(int id)
    {
        var usuario = await _db.Usuarios.FindAsync(id);

        if (usuario == null)
        {
            throw new NotFoundException("Usuário", id);
        }

        return usuario;
    }

    public async Task<Usuario> BuscarPorNomeUsuarioAsync(string nomeUsuario)
    {
        var usuario = await _db.Usuarios
            .FirstOrDefaultAsync(u => u.NomeUsuario.ToLower() == nomeUsuario.ToLower());

        if (usuario == null)
        {
            throw new NotFoundException($"Usuário '{nomeUsuario}' não encontrado");
        }

        return usuario;
    }

    private string HashSenha(string senha)
    {
        using var sha256 = System.Security.Cryptography.SHA256.Create();
        var bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
        return Convert.ToBase64String(bytes);
    }
}
