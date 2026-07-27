# Backend

API REST desenvolvida em ASP.NET Core 10 utilizando Entity Framework Core e PostgreSQL.

## Estrutura

```text
backend
├── src
│   ├── Controllers
│   ├── Data
│   ├── DTOs
│   ├── Entities
│   ├── Enums
│   ├── Exceptions
│   ├── Middlewares
│   ├── Migrations
│   ├── Rules
│   ├── Services
│   └── Validators
└── tests
```

## Organização

O projeto segue uma arquitetura em camadas.

- Controllers recebem as requisições HTTP.
- Services concentram as regras de negócio.
- Validators validam os DTOs de entrada.
- Data contém o contexto do Entity Framework e os mapeamentos.
- DTOs representam contratos de entrada e saída.
- Rules encapsulam regras reutilizáveis que não dependem de infraestrutura.
- Exceptions define exceções específicas da aplicação.

## Executando sem Docker

### Configurar User Secrets

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=expense-manager;Username=expense-manager;Password=<senha>"
```

### Subir apenas o banco

```bash
docker compose up postgres -d
```

### Executar a API

```bash
dotnet run
```

## Migrations

Criar migration:

```bash
dotnet ef migrations add NomeDaMigration
```

Aplicar migrations:

```bash
dotnet ef database update
```

## Testes

```bash
dotnet test
```

## Documentação

Swagger:

Com a aplicação rodando, o Swagger fica disponível em `/swagger`.
