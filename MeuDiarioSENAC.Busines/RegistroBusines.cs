using MeuDiarioSENAC.Model;

namespace MeuDiarioSENAC.Business;

public class RegistroBusiness
{
    public bool TituloInformado(Registro registro)
    {
        return !string.IsNullOrWhiteSpace(registro.Titulo);
    }

    public bool TituloTemNoMaximo50Caracteres(Registro registro)
    {
        return registro.Titulo.Length <= 50;
    }

    public bool DataValida(Registro registro)
    {
        return registro.DataRegistro.Date <= DateTime.Now.Date;
    }

    public bool ConteudoTemNoMaximo3000Caracteres(Registro registro)
    {
        return registro.Conteudo.Length <= 3000;
    }

    public bool RegistroValido(Registro registro)
    {
        return TituloInformado(registro)
            && TituloTemNoMaximo50Caracteres(registro)
            && DataValida(registro)
            && ConteudoTemNoMaximo3000Caracteres(registro);
    }
}