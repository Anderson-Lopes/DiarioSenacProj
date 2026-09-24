using MeuDiarioSENAC.Model;
using Microsoft.EntityFrameworkCore;

namespace MeuDiarioSENAC.Data;

public class RegistroDAO
{
    private readonly MeuDiarioSENACContext conexao = new();

    public Usuario ObterOuCriarUsuarioPadrao()
    {
        var usuario = conexao.Usuarios.FirstOrDefault();

        if (usuario == null)
        {
            usuario = new Usuario { Nome = "Usuário Padrão" };
            conexao.Usuarios.Add(usuario);
            conexao.SaveChanges();
        }

        return usuario;
    }

    public void Inserir(Registro registro)
    {
        if (registro.UsuarioId == 0)
        {
            Usuario usuario = ObterOuCriarUsuarioPadrao();
            registro.UsuarioId = usuario.Id;
            registro.Usuario = usuario;
        }

        conexao.Registros.Add(registro);
        conexao.SaveChanges();
    }

    public List<Registro> ListarTodos()
    {
        return conexao.Registros
        .AsNoTracking()
        .Include(r => r.Usuario)
        .ToList();
    }

    public Registro? BuscarPorId(int id)
    {
        return conexao.Registros.Find(id);
    }

    public void Atualizar(Registro registro)
    {
        conexao.Registros.Update(registro);
        conexao.SaveChanges();
    }
    public void Excluir(int id)
    {
        var registro = conexao.Registros.Find(id);
        if (registro != null)
        {
            conexao.Registros.Remove(registro);
            conexao.SaveChanges();
        }
    }
}