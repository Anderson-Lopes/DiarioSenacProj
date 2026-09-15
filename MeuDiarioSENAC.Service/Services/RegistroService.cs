using MeuDiarioSENAC.Business;
using MeuDiarioSENAC.Data;
using MeuDiarioSENAC.Model;
using MeuDiarioSENAC.Service.Interfaces;

namespace MeuDiarioSENAC.Service.Services;

public class RegistroService : IRegistroService
{
    private readonly RegistroDAO registroDao;
    private readonly RegistroBusiness registroBusiness;

    public RegistroService()
        : this(new RegistroDAO(), new RegistroBusiness())
    {
    }

    public RegistroService(RegistroDAO registroDao, RegistroBusiness registroBusiness)
    {
        this.registroDao = registroDao;
        this.registroBusiness = registroBusiness;
    }

    public Usuario ObterOuCriarUsuarioPadrao()
    {
        return registroDao.ObterOuCriarUsuarioPadrao();
    }

    public bool Inserir(Registro registro)
    {
        if (!registroBusiness.RegistroValido(registro))
        {
            return false;
        }

        registroDao.Inserir(registro);
        return true;
    }

    public List<Registro> ListarTodos()
    {
        return registroDao.ListarTodos();
    }

    public Registro? BuscarPorId(int id)
    {
        return registroDao.BuscarPorId(id);
    }

    public bool Atualizar(Registro registro)
    {
        if (!registroBusiness.RegistroValido(registro))
        {
            return false;
        }

        registroDao.Atualizar(registro);
        return true;
    }

    public bool Excluir(int id)
    {
        if (registroDao.BuscarPorId(id) is null)
        {
            return false;
        }

        registroDao.Excluir(id);
        return true;
    }
}