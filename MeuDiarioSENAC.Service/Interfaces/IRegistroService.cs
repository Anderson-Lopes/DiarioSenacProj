using MeuDiarioSENAC.Model;

namespace MeuDiarioSENAC.Service.Interfaces;

public interface IRegistroService
{
    Usuario ObterOuCriarUsuarioPadrao();

    bool Inserir(Registro registro);

    List<Registro> ListarTodos();

    Registro? BuscarPorId(int id);

    bool Atualizar(Registro registro);

    bool Excluir(int id);
}