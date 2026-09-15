using MeuDiarioSENAC.Model;
using MeuDiarioSENAC.Service.Interfaces;

namespace MeuDiarioSENAC.ConsoleApp;

public class Menu
{
    private readonly IRegistroService registroService;
    private readonly Usuario usuarioPadrao;

    public Menu(IRegistroService registroService)
    {
        this.registroService = registroService;
        usuarioPadrao = registroService.ObterOuCriarUsuarioPadrao();
    }

    public void Executar()
    {
        int opcao;

        do
        {
            ExibirMenuPrincipal();
            opcao = LerInteiro("Escolha uma opção: ");
            Console.Clear();
            ExecutarOpcao(opcao);

            if (opcao != 0)
            {
                AguardarContinuacao();
            }
        }
        while (opcao != 0);
    }

    private void ExibirMenuPrincipal()
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("================================================================================");
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("                   MEU DIÁRIO SENAC");
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("================================================================================");
        Console.ResetColor();
        Console.WriteLine("                 1 - Novo Registro");
        Console.WriteLine("                 2 - Listar Registros");
        Console.WriteLine("                 3 - Buscar por ID");
        Console.WriteLine("                 4 - Atualizar Registro");
        Console.WriteLine("                 5 - Excluir Registro");
        Console.WriteLine("                 0 - Sair");
        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.Yellow;
    }

    private void ExecutarOpcao(int opcao)
    {
        switch (opcao)
        {
            case 1:
                CriarRegistro();
                break;
            case 2:
                ListarRegistros();
                break;
            case 3:
                BuscarRegistro();
                break;
            case 4:
                AtualizarRegistro();
                break;
            case 5:
                ExcluirRegistro();
                break;
            case 0:
                EncerrarAplicacao();
                break;
            default:
                Console.WriteLine("Opção inválida.");
                break;
        }
    }

    private void CriarRegistro()
    {
        Registro registro = new Registro
        {
            Titulo = LerTexto("Título: "),
            Conteudo = LerTexto("Conteúdo: "),
            DataRegistro = DateTime.Now,
            Usuario = usuarioPadrao,
            UsuarioId = usuarioPadrao.Id
        };

        if (!registroService.Inserir(registro))
        {
            Console.WriteLine();
            Console.WriteLine("Registro inválido. O título é obrigatório, além de data e conteúdo válidos.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine("Registro salvo com sucesso!");
    }

    private void ListarRegistros()
    {
        List<Registro> lista = registroService.ListarTodos();

        foreach (Registro registro in lista)
        {
            Console.WriteLine("-----------------------------------");
            Console.WriteLine($"ID: {registro.Id}");
            Console.WriteLine($"Título: {registro.Titulo}");
            Console.WriteLine($"Data: {registro.DataRegistro:dd/MM/yyyy}");
            Console.WriteLine($"Conteúdo: {registro.Conteudo}");
            Console.WriteLine();
        }

        if (lista.Count == 0)
        {
            Console.WriteLine("Nenhum registro encontrado.");
        }
    }

    private void BuscarRegistro()
    {
        int id = LerInteiro("Informe o ID: ");
        Registro? registro = registroService.BuscarPorId(id);

        Console.WriteLine();

        if (registro is null)
        {
            Console.WriteLine("Registro não encontrado.");
            return;
        }

        Console.WriteLine($"ID: {registro.Id}");
        Console.WriteLine($"Título: {registro.Titulo}");
        Console.WriteLine($"Data: {registro.DataRegistro:dd/MM/yyyy}");
        Console.WriteLine("Conteúdo:");
        Console.WriteLine(registro.Conteudo);
    }

    private void AtualizarRegistro()
    {
        int id = LerInteiro("Informe o ID do registro: ");
        Registro? registro = registroService.BuscarPorId(id);

        if (registro is null)
        {
            Console.WriteLine("Registro não encontrado.");
            return;
        }

        registro.Titulo = LerTexto("Novo título: ");
        registro.Conteudo = LerTexto("Novo conteúdo: ");

        if (!registroService.Atualizar(registro))
        {
            Console.WriteLine();
            Console.WriteLine("Registro inválido. O título é obrigatório, além de data e conteúdo válidos.");
            return;
        }

        Console.WriteLine("Registro atualizado com sucesso!");
    }

    private void ExcluirRegistro()
    {
        int id = LerInteiro("Informe o ID do registro: ");
        Registro? registro = registroService.BuscarPorId(id);

        if (registro is null)
        {
            Console.WriteLine("Registro não encontrado.");
            return;
        }

        registroService.Excluir(id);
        Console.WriteLine("Registro excluído com sucesso!");
    }

    private static int LerInteiro(string mensagem)
    {
        while (true)
        {
            Console.Write(mensagem);
            string? entrada = Console.ReadLine();

            if (int.TryParse(entrada, out int valor))
            {
                Console.ResetColor();
                return valor;
            }

            Console.WriteLine("Valor inválido. Digite um número inteiro.");
        }
    }

    private static string LerTexto(string mensagem)
    {
        Console.Write(mensagem);
        return Console.ReadLine()!;
    }

    private static void AguardarContinuacao()
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Pressione qualquer tecla para continuar...");
        Console.ReadKey();
        Console.ResetColor();
    }

    private static void EncerrarAplicacao()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Encerrando o sistema...");
        Console.ResetColor();
    }
}