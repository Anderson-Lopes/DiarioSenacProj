static int LerInteiro(string mensagem)
{
    while (true)
    {
        Console.Write(mensagem);
        string? entrada = Console.ReadLine();

        if (int.TryParse(entrada, out int valor))
        {
            return valor;
        }

        Console.WriteLine("Valor inválido. Digite um número inteiro.");
    }
}

RegistroDAO dao = new RegistroDAO();
Usuario usuarioPadrao = dao.ObterOuCriarUsuarioPadrao();

int opcao;

do
{
    Console.Clear();

    Console.WriteLine("===================================");
    Console.WriteLine("      MEU DIÁRIO SENAC");
    Console.WriteLine("===================================");
    Console.WriteLine("1 - Novo Registro");
    Console.WriteLine("2 - Listar Registros");
    Console.WriteLine("3 - Buscar por ID");
    Console.WriteLine("4 - Atualizar Registro");
    Console.WriteLine("5 - Excluir Registro");
    Console.WriteLine("0 - Sair");
    Console.WriteLine();

    opcao = LerInteiro("Escolha uma opção: ");

    Console.Clear();

    switch (opcao)
    {
        case 1:

            Registro registro = new Registro();

            Console.Write("Título: ");
            registro.Titulo = Console.ReadLine()!;

            Console.Write("Conteúdo: ");
            registro.Conteudo = Console.ReadLine()!;

            registro.DataRegistro = DateTime.Now;
            registro.Usuario = usuarioPadrao;
            registro.UsuarioId = usuarioPadrao.Id;

            dao.Inserir(registro);

            Console.WriteLine();
            Console.WriteLine("Registro salvo com sucesso!");

            break;

        case 2:

            List<Registro> lista = dao.ListarTodos();

            foreach (var r in lista)
            {
                Console.WriteLine("-----------------------------------");
                Console.WriteLine($"ID: {r.Id}");
                Console.WriteLine($"Título: {r.Titulo}");
                Console.WriteLine($"Data: {r.DataRegistro:dd/MM/yyyy}");
                Console.WriteLine($"Conteúdo: {r.Conteudo}");
                Console.WriteLine();
            }

            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhum registro encontrado.");
            }

            break;

        case 3:

            int id = LerInteiro("Informe o ID: ");

            Registro? encontrado = dao.BuscarPorId(id);

            Console.WriteLine();

            if (encontrado != null)
            {
                Console.WriteLine($"ID: {encontrado.Id}");
                Console.WriteLine($"Título: {encontrado.Titulo}");
                Console.WriteLine($"Data: {encontrado.DataRegistro:dd/MM/yyyy}");
                Console.WriteLine($"Conteúdo:");
                Console.WriteLine(encontrado.Conteudo);
            }
            else
            {
                Console.WriteLine("Registro não encontrado.");
            }

            break;

        case 4:

            int idAtualizacao = LerInteiro("Informe o ID do registro: ");
            Registro? registroAtualizado = dao.BuscarPorId(idAtualizacao);

            if (registroAtualizado != null)
            {
                Console.Write("Novo título: ");
                registroAtualizado.Titulo = Console.ReadLine()!;

                Console.Write("Novo conteúdo: ");
                registroAtualizado.Conteudo = Console.ReadLine()!;

                dao.Atualizar(registroAtualizado);
                Console.WriteLine("Registro atualizado com sucesso!");
            }
            else
            {
                Console.WriteLine("Registro não encontrado.");
            }

            break;

        case 5:

            int idExclusao = LerInteiro("Informe o ID do registro: ");
            Registro? registroExcluido = dao.BuscarPorId(idExclusao);

            if (registroExcluido != null)
            {
                dao.Excluir(idExclusao);
                Console.WriteLine("Registro excluído com sucesso!");
            }
            else
            {
                Console.WriteLine("Registro não encontrado.");
            }

            break;

        case 0:

            Console.WriteLine("Encerrando o sistema...");
            break;

        default:

            Console.WriteLine("Opção inválida.");
            break;
    }

    if (opcao != 0)
    {
        Console.WriteLine();
        Console.WriteLine("Pressione qualquer tecla para continuar...");
        Console.ReadKey();
    }

} while (opcao != 0);