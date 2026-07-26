# Household Expense Manager

Boilerplate full stack para um sistema de controle de gastos residenciais.

## Tecnologias

- ASP.NET Core 8, C# and Entity Framework Core
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

## Documentação

Veja [technical decisions](docs/technical-decisions.md).
