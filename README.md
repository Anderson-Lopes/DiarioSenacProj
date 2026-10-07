# MeuDiarioSENAC

Sistema em .NET para gestão de registros do diário, composto por uma solução com API Web ASP.NET Core e uma aplicação de console. O projeto persiste dados em MySQL usando Entity Framework Core e organiza a lógica em camadas de modelo, dados, negócios, serviços e apresentação.

## Funcionalidades

As funcionalidades abaixo estão confirmadas no código:

- Cadastro de registros com título, data e conteúdo.
- Listagem de todos os registros.
- Busca de um registro por identificador.
- Atualização de um registro existente.
- Exclusão de um registro existente.
- Criação automática de um usuário padrão quando não há usuário associado ao registro.
- Validação de regras de negócio antes de inserir ou atualizar um registro:
  - título obrigatório;
  - título com no máximo 50 caracteres;
  - data não pode ser futura;
  - conteúdo com no máximo 3000 caracteres.
- Exposição de endpoints REST para manipular registros.
- Interface textual em console para uso local.

## Tecnologias

As tecnologias abaixo foram confirmadas pelos arquivos de projeto e configuração:

- .NET SDK 9.0
- ASP.NET Core Web API
- C#
- Entity Framework Core 9.0.0
- Pomelo.EntityFrameworkCore.MySql 9.0.0
- MySql.Data 26.7.0
- MySQL como banco de dados relacional
- Microsoft.EntityFrameworkCore.Design 9.0.0

Observações:

- As versões foram confirmadas pelos arquivos `*.csproj` e `project.assets.json`.
- Não há indicação de frontend web, SPA, mobile, Node.js, Python, Java, Go ou outra stack principal no repositório.
- Não há arquivo `global.json` ou `package.json` na raiz.

## Arquitetura

O repositório é uma solução .NET com múltiplos projetos, mas não configura um monorepo de vários serviços independentes; ele organiza o sistema em camadas dentro de uma mesma solução.

Estrutura de diretórios relevante:

```text
MeuDiarioSENAC/
├─ MeuDiarioSENAC.sln
├─ MeuDiarioSENAC.Model/
│  ├─ Registro.cs
│  ├─ Usuario.cs
│  └─ MeuDiarioSENAC.Model.csproj
├─ MeuDiarioSENAC.Data/
│  ├─ MeuDiarioSENACContext.cs
│  ├─ RegistroDAO.cs
│  ├─ Migrations/
│  └─ MeuDiarioSENAC.Data.csproj
├─ MeuDiarioSENAC.Busines/
│  └─ RegistroBusines.cs
├─ MeuDiarioSENAC.Service/
│  ├─ Interfaces/
│  ├─ Services/
│  └─ MeuDiarioSENAC.Service.csproj
├─ MeuDiarioSENAC.WebAPI/
│  ├─ Program.cs
│  ├─ appsettings.json
│  ├─ appsettings.Development.json
│  ├─ Properties/launchSettings.json
│  └─ MeuDiarioSENAC.WebAPI.csproj
├─ MeuDiarioSENAC.ConsoleApp/
│  ├─ Program.cs
│  ├─ Menu.cs
│  └─ MeuDiarioSENAC.ConsoleApp.csproj
└─ .vscode/
```

Responsabilidades por camada:

- `MeuDiarioSENAC.Model`: entidades `Registro` e `Usuario`.
- `MeuDiarioSENAC.Data`: acesso ao banco via `DbContext` e `RegistroDAO`; também contém as migrações do EF Core.
- `MeuDiarioSENAC.Busines`: regras de validação do domínio (`RegistroBusiness`).
- `MeuDiarioSENAC.Service`: orquestração de regras de negócio e acesso aos dados.
- `MeuDiarioSENAC.WebAPI`: API HTTP minimal API.
- `MeuDiarioSENAC.ConsoleApp`: aplicação terminal para interação direta.

Fluxo principal:

1. A API recebe requisições HTTP em `/registros`.
2. O `RegistroService` chama `RegistroDAO` para consultar ou persistir registros.
3. `RegistroBusiness` valida os dados antes de inserir/atualizar.
4. O EF Core usa `MeuDiarioSENACContext` para persistir em MySQL.
5. O `MeuDiarioSENACContext` chama `Database.Migrate()` automaticamente no construtor, aplicando as migrações pendentes.

## Pré-requisitos

Antes de iniciar o projeto, confirme os itens abaixo:

- .NET SDK 9.0 instalado.
- MySQL acessível localmente ou em rede.
- Git para clonar o repositório.
- Editor de código ou IDE compatível com .NET (Visual Studio, VS Code, Rider ou similar).

Observações:

- Não foi encontrado arquivo `Dockerfile`, `docker-compose.yml`, `docker-compose.yaml`, `.env.example` ou configuração de infraestrutura em container.
- A conexão padrão do banco é definida no código em `MeuDiarioSENACContext` e usa um valor placeholder na senha, então a credencial real precisa ser confirmada no ambiente local.
- O projeto não define autenticação, filas, cache, Redis, Kafka ou serviços externos em arquivos de configuração.

## Instalação

No terminal, execute os comandos a seguir a partir da raiz do repositório:

```bash
git clone <URL-do-repositorio>
cd MeuDiarioSENAC
dotnet restore MeuDiarioSENAC.sln
dotnet build MeuDiarioSENAC.sln
```

No Windows PowerShell, a sequência equivalente é:

```powershell
git clone <URL-do-repositorio>
Set-Location MeuDiarioSENAC
dotnet restore MeuDiarioSENAC.sln
dotnet build MeuDiarioSENAC.sln
```

## Configuração do ambiente

Não há arquivo `.env`, `.env.example` ou `appsettings.Production.json` no repositório. A variável de ambiente configurada no código é a seguinte:

| Variável | Obrigatória | Valor padrão | Descrição |
|---|---|---|---|
| `MEUDIARIOSENAC_CONNECTION_STRING` | Não | `server=localhost;database=MeuDiarioSENAC;uid=root;password=SenhaForte123;` | String de conexão do MySQL usada pelo `MeuDiarioSENACContext` quando a variável de ambiente não está definida. O valor real do banco e da senha devem ser confirmados no ambiente local. |

Exemplo de configuração para Linux/macOS:

```bash
export MEUDIARIOSENAC_CONNECTION_STRING="server=localhost;database=MeuDiarioSENAC;uid=root;password=SenhaForte123;"
```

Exemplo de configuração para PowerShell:

```powershell
$env:MEUDIARIOSENAC_CONNECTION_STRING = "server=localhost;database=MeuDiarioSENAC;uid=root;password=SenhaForte123;"
```

Observações:

- A senha real aparece mascarada no código (`******`), então este valor é apenas ilustrativo e não deve ser copiado para produção.
- A aplicação usa o valor padrão do código quando a variável não estiver definida.

## Inicialização em desenvolvimento

A solução possui dois pontos de entrada relevantes: a API Web e a aplicação de console.

### 1) Preparar o banco MySQL

O projeto exige uma instância MySQL disponível antes da execução da aplicação. Não há arquivo `docker-compose.yml` ou `Dockerfile` para provisionar o banco.

Verifique se o serviço MySQL está acessível e, se necessário, crie o banco e as credenciais no ambiente local. A aplicação tenta aplicar as migrações automaticamente via `Database.Migrate()`.

### 2) Restaurar dependências e compilar

```bash
cd MeuDiarioSENAC
dotnet restore MeuDiarioSENAC.sln
dotnet build MeuDiarioSENAC.sln
```

### 3) Definir a string de conexão (opcional)

```bash
export MEUDIARIOSENAC_CONNECTION_STRING="server=localhost;database=MeuDiarioSENAC;uid=root;password=SenhaForte123;"
```

PowerShell:

```powershell
Set-Location MeuDiarioSENAC
$env:MEUDIARIOSENAC_CONNECTION_STRING = "server=localhost;database=MeuDiarioSENAC;uid=root;password=SenhaForte123;"
```

### 4) Iniciar a API Web

O profile de execução em `launchSettings.json` informa os endpoints da API:

- `http://localhost:5287`
- `https://localhost:7219`

Inicie a API com:

```bash
cd MeuDiarioSENAC
dotnet run --project MeuDiarioSENAC.WebAPI
```

Ou, se quiser iniciar diretamente pelo projeto e definir a configuração de execução em um ambiente Windows PowerShell:

```powershell
Set-Location MeuDiarioSENAC
dotnet run --project .\MeuDiarioSENAC.WebAPI\MeuDiarioSENAC.WebAPI.csproj
```

A aplicação estará disponível em:

```text
http://localhost:5287
```

### 5) Iniciar a aplicação de console

```bash
cd MeuDiarioSENAC
dotnet run --project MeuDiarioSENAC.ConsoleApp
```

PowerShell:

```powershell
Set-Location MeuDiarioSENAC
dotnet run --project .\MeuDiarioSENAC.ConsoleApp\MeuDiarioSENAC.ConsoleApp.csproj
```

A aplicação exibe um menu interativo para criar, listar, buscar, atualizar e excluir registros.

### 6) Encerrar os serviços

Pressione `Ctrl+C` na janela do terminal que executa a aplicação. Para a API ou a console, o encerramento é feito pelo próprio console do processo em execução.

### 7) Migrações e dados iniciais

O código aplica migrações automaticamente no `MeuDiarioSENACContext`:

```csharp
public MeuDiarioSENACContext()
{
    Database.Migrate();
}
```

Isso significa que, ao iniciar a aplicação, o EF Core tenta aplicar as migrações pendentes no banco. Não há script de seed explícito em `Migrations` ou em um diretório de `seed` no repositório.

## Execução em produção

Não há configuração de produção confirmada no repositório. Não foram encontrados:

- `Dockerfile`;
- `docker-compose.yml`;
- `appsettings.Production.json`;
- pipeline de deploy;
- scripts de publicação em `Makefile`, `scripts/` ou similar.

Portanto, a implantação em produção é `A confirmar` e deve ser definida no ambiente de destino, conforme as políticas e a infraestrutura externa do projeto.

## Comandos disponíveis

Os comandos abaixo foram confirmados por arquivos do projeto e podem ser executados a partir da raiz do repositório.

| Comando | Descrição |
|---|---|
| `dotnet restore MeuDiarioSENAC.sln` | Restaura as dependências da solução. |
| `dotnet build MeuDiarioSENAC.sln` | Compila a solução inteira. |
| `dotnet run --project MeuDiarioSENAC.WebAPI` | Inicia a API Web em ambiente de desenvolvimento. |
| `dotnet run --project MeuDiarioSENAC.ConsoleApp` | Inicia a aplicação de console. |

Observações:

- Não há projeto de testes configurado no repositório.
- Não há scripts de lint, format, type-check, migration ou production definidos em um `package.json`, `Makefile` ou `Taskfile.yml`.

## Testes

Não há projeto de testes, arquivos de testes automatizados, scripts de cobertura ou configuração de `xUnit`, `NUnit`, `MSTest`, `Playwright`, `Jest` ou ferramentas equivalentes no repositório.

A verificação mais direta disponível é a compilação da solução:

```bash
dotnet build MeuDiarioSENAC.sln
```

Se houver testes no futuro, eles devem ser adicionados em um projeto separado e executados com `dotnet test`.

## Banco de dados

### Tecnologia

O banco utilizado é MySQL, conforme a dependência `Pomelo.EntityFrameworkCore.MySql` e a configuração do contexto.

### Inicialização

O banco não é provisionado pelo repositório por meio de Docker, Compose ou scripts SQL de bootstrapping. O código usa a string de conexão padrão:

```text
server=localhost;database=MeuDiarioSENAC;uid=root;password=...;
```

A senha real foi mascarada no código (`******`). Portanto, a configuração exata do ambiente real precisa ser validada localmente.

### Migrações

As migrações do Entity Framework Core estão em:

```text
MeuDiarioSENAC.Data/Migrations/
```

Arquivos identificados:

- `20260819000310_CriacaoDatabase.cs`
- `20260819003119_AdicaoIdRegistro.cs`
- `20260819003240_AdicaoColunaRegistro.cs`
- `20260826233044_RelacionamentoUsurioRegistros.cs`

A aplicação aplica as migrações automaticamente durante a inicialização, por meio do construtor do `MeuDiarioSENACContext`.

### Seeds e reinicialização

Não foram encontrados scripts de seed, scripts SQL de criação de dados iniciais, ou comando automatizado de reset do banco. Em outras palavras:

- `seed` de dados iniciais: não confirmado;
- scripts de reset: não confirmados;
- procedimento de reinicialização segura: `A confirmar`.

## API

A API está implementada em `MeuDiarioSENAC.WebAPI/Program.cs`. Não há autenticação configurada, middleware de autorização, documentação OpenAPI/Swagger ou esquema de segurança declarado.

### Base URL

Os endpoints da API são expostos conforme `launchSettings.json`:

- `http://localhost:5287`
- `https://localhost:7219`

### Endpoints confirmados

```http
GET /
```

Resposta:

```text
Hello World
```

```http
GET /motivacional
```

Resposta:

```text
Hello World! Mundo cruel onde vivemos. E cada integrante deste curso abusa consideravelmente da paciência alheia.
```

```http
GET /registros
```

Lista todos os registros.

```http
POST /registros
```

Cria um novo registro. O corpo é um objeto `Registro` com os campos `titulo`, `conteudo`, `dataRegistro` e `usuarioId` (opcional).

```http
GET /registros/{id}
```

Busca um registro pelo identificador.

```http
PUT /registros/{id}
```

Atualiza um registro existente pela rota `{id}`.

```http
DELETE /registros/{id}
```

Exclui um registro existente.

### Autenticação

Não há autenticação implementada. O `Program.cs` não registra serviços de autenticação nem exige tokens ou cookies.

### Documentação da API

Não há documentação OpenAPI, Swagger, ReDoc, Postman Collection nem arquivo de especificação API no repositório. A documentação disponível está somente no código da aplicação.

## Docker

Não há qualquer artefato de containerização no repositório:

- `Dockerfile`: não encontrado;
- `docker-compose.yml`: não encontrado;
- `docker-compose.yaml`: não encontrado;
- `.dockerignore`: não encontrado.

Logo, o projeto não possui uma infraestrutura oficial em Docker definida no código atual.

## Solução de problemas

### 1) Erro de conexão com o MySQL

Sintoma: a aplicação falha ao iniciar ou a operação de banco gera exceção.

Solução:

- confirme se o MySQL está em execução;
- valide a variável `MEUDIARIOSENAC_CONNECTION_STRING`;
- confira se o banco `MeuDiarioSENAC` existe ou pode ser criado pelo EF Core;
- o código usa `Database.Migrate()`, mas o serviço MySQL precisa estar acessível para isso funcionar.

### 2) Porta já em uso

Sintoma: `dotnet run` falha ao iniciar a API por conflito de porta.

Solução:

- verifique se outra aplicação está usando as portas `5287` ou `7219`;
- altere os valores em `MeuDiarioSENAC.WebAPI/Properties/launchSettings.json` ou use `ASPNETCORE_URLS` durante a execução.

### 3) SDK .NET incompatível

Sintoma: erro ao compilar ou executar pela ausência de .NET 9.

Solução:

```bash
dotnet --list-sdks
```

Se o .NET 9 não estiver instalado, instale-o antes de executar a solução.

### 4) Migrações pendentes não aplicadas

Sintoma: o projeto inicia e o banco não contém as tabelas esperadas.

Solução:

- confirme se o MySQL está acessível;
- confirme a string de conexão;
- a classe `MeuDiarioSENACContext` tenta aplicar migrações automaticamente no construtor;
- se a falha persistir, verifique manualmente a configuração do banco e a conexão do ambiente.

### 5) Cliente de console não funciona corretamente

Sintoma: a aplicação de console não está criando ou listando registros.

Solução:

- confirme que o banco está funcionando;
- valide a presença do usuário padrão e do contexto EF Core;
- verifique se a variável de ambiente da conexão foi definida corretamente.

## Contribuição

O fluxo de desenvolvimento do projeto é direto e baseado em solução .NET:

1. Crie ou atualize a funcionalidade em uma das camadas: `Model`, `Data`, `Business`, `Service` ou `WebAPI`.
2. Mantenha o padrão de separação por responsabilidade já adotado no código.
3. Compile a solução antes de enviar alterações:

```bash
dotnet build MeuDiarioSENAC.sln
```

4. Quando alterar modelos ou schema do banco, valide o impacto das migrações EF Core.
5. Evite alterar o comportamento da API sem confirmar o efeito em `Program.cs` e nas classes de serviço.

Recomendações:

- mantenha as entidades e regras de negócio na camada correta;
- preserve a convenção de nomes utilizada no projeto;
- teste manualmente os fluxos de cadastro, listagem, busca, atualização e exclusão;
- acompanhe as migrações em `MeuDiarioSENAC.Data/Migrations` se houver alteração do banco.

## Licença

Nenhuma licença foi especificada no repositório. Não foi encontrado arquivo `LICENSE` ou `LICENSE.md` na raiz ou em diretórios do projeto.

Portanto, a licença do projeto é: `Não especificada`.
