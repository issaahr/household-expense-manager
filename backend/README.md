# Backend — Household Expense Manager API

API REST construída em ASP.NET Core 10 com Entity Framework Core e PostgreSQL.

## Estrutura de pastas

```text
backend/
├── Dockerfile
├── README.md
├── src/
│   ├── Controllers/                # Endpoints da API
│   ├── Data/
│   │   ├── AppDbContext.cs         # Contexto do EF Core
│   │   ├── Configurations/         # Mapeamento entidade → tabela (Fluent API)
│   │   └── DevelopmentDatabaseSeeder.cs
│   ├── DTOs/                       # Objetos de transferência (request/response)
│   ├── Entities/                   # Classes de domínio
│   ├── Enums/                      # Enums do domínio
│   ├── Migrations/                 # Migrations geradas pelo EF Core
│   ├── Properties/
│   ├── Services/                   # Regras de negócio e orquestração de dados
│   ├── Validators/                 # Validação de entrada (ex: FluentValidation)
│   ├── appsettings.json
│   ├── appsettings.Development.json
│   ├── HouseholdExpenseManager.Api.csproj
│   └── Program.cs
└── tests/
    └── HouseholdExpenseManager.Api.Tests/
        └── HouseholdExpenseManager.Api.Tests.csproj
```

### Padrão de entidades

As entidades em `Entities/` são POCOs "burros" — sem validação, sem métodos de negócio. Toda regra de negócio (ex: "menor de idade só pode registrar despesas") fica isolada em `Services/`, o que facilita testes unitários sem depender do `DbContext`.

O mapeamento objeto-relacional (tamanho de coluna, precisão de decimal, relacionamentos, cascade delete) fica separado em `Data/Configurations/`, uma classe `IEntityTypeConfiguration<T>` por entidade, em vez de Data Annotations nas próprias classes.

## Rodando localmente (sem Docker)

Esse fluxo é útil para gerar/aplicar migrations ou rodar a API fora do container, com hot reload mais rápido.

### 1. Configurar a connection string via User Secrets

A connection string de desenvolvimento **não fica em nenhum arquivo versionado**. Configure via User Secrets (já inicializado no projeto, `UserSecretsId` presente no `.csproj`):

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=expense-manager;Username=expense-manager;Password=<sua_senha>"
```

Use as mesmas credenciais definidas no `.env` da raiz do projeto.

### 2. Subir apenas o banco de dados

Na raiz do projeto:

```bash
docker compose up postgres -d
```

### 3. Rodar a API

```bash
dotnet run
```

A API sobe em `http://localhost:5080` (ver `Properties/launchSettings.json`).

## Migrations

Gerar uma nova migration após alterar entidades ou configurações:

```bash
dotnet ef migrations add NomeDaMigration
```

Aplicar migrations pendentes manualmente:

```bash
dotnet ef database update
```

> Em ambiente Docker, as migrations pendentes são aplicadas automaticamente na inicialização do backend (ver `Program.cs`), não sendo necessário rodar `dotnet ef database update` manualmente nesse cenário.

## Testes

```bash
dotnet test
```

## Documentação da API

Com a aplicação rodando, o Swagger fica disponível em `/swagger`.
