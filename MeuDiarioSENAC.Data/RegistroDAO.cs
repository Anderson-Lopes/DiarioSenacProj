using MeuDiarioSENAC.Model;

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
        conexao.Registros.Add(registro);
        conexao.SaveChanges();
    }

    public List<Registro> ListarTodos()
    {
        return conexao.Registros.ToList();
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