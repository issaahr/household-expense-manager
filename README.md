# Household Expense Manager

Aplicação full stack para gerenciamento de gastos residenciais, desenvolvida como desafio técnico.

## Stack

- ASP.NET Core 10
- Entity Framework Core
- PostgreSQL
- React
- TypeScript
- Docker Compose
- Swagger/OpenAPI
- xUnit
- Vitest

## Estrutura

```text
.
├── backend/      API ASP.NET Core
├── frontend/     Aplicação React
├── docs/         Documentação técnica
├── docker-compose.yml
└── .env.example
```

## Executando com Docker

1. Crie o arquivo `.env`:

```bash
cp .env.example .env
```

2. Inicie os containers:

```bash
docker compose up --build
```

Após a inicialização:

| Serviço | Endereço |
|---------|----------|
| Frontend | http://localhost:5173 |
| API | http://localhost:8080 |
| Swagger | http://localhost:8080/swagger |
| Health Check | http://localhost:8080/health |

Durante a inicialização da API, as migrations pendentes são aplicadas automaticamente em ambiente de desenvolvimento.

## Desenvolvimento

Para executar somente o backend sem Docker, consulte:

- `backend/README.md`

## Documentação

As principais decisões arquiteturais estão documentadas em [technical decisions](docs/technical-decisions.md), enquanto as decisões relacionadas ao escopo da implementação encontram-se em [project scope](docs/project-scope.md).

Documentação da API:

- Swagger (`/swagger`)
