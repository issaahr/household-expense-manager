# Household Expense Manager

Boilerplate full stack para um sistema de controle de gastos residenciais.

## Tecnologias

- ASP.NET Core 10, C# and Entity Framework Core
- PostgreSQL
- React, TypeScript and Vite
- xUnit and Vitest
- Docker Compose
- Swagger/OpenAPI

## Organização de diretórios

```text
.
├── backend/       # API ASP.NET Core
├── frontend/      # Aplicação React
├── docs/          # Documentação do projeto
├── docker-compose.yml
└── .env.example
```

## Iniciar com Docker Compose

1. Copie e configure as variáveis de ambiente:

```bash
   cp .env.example .env
```

2. Inicie a aplicação:

```bash
   docker compose up --build
```

O frontend estará disponível em `http://localhost:5173`, a API em `http://localhost:8080`, e o Swagger em `http://localhost:8080/swagger`.

> As migrations do banco de dados são aplicadas automaticamente ao subir o backend em ambiente de desenvolvimento (ver `backend/Program.cs`).

## Desenvolvimento local do backend (sem Docker)

Para rodar a API diretamente na máquina (fora do container), sem docker, veja [backend/README.md](backend/README.md).

## Documentação

Veja [technical decisions](docs/technical-decisions.md).
