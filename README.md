# Household Expense Manager

Aplicação full stack para gerenciamento de gastos residenciais, desenvolvida como desafio técnico.

## Stack

<p align="center">
  <img src="https://img.shields.io/badge/.NET%2010-512BD4?logo=dotnet&logoColor=white" />
  <img src="https://img.shields.io/badge/C%23-239120?logo=csharp&logoColor=white" />
  <img src="https://img.shields.io/badge/PostgreSQL-4169E1?logo=postgresql&logoColor=white" />
  <img src="https://img.shields.io/badge/React-61DAFB?logo=react&logoColor=black" />
  <img src="https://img.shields.io/badge/TypeScript-3178C6?logo=typescript&logoColor=white" />
  <img src="https://img.shields.io/badge/Docker-2496ED?logo=docker&logoColor=white" />
</p>

## Sobre

Sistema de controle de gastos residenciais desenvolvido com frontend em React e backend em ASP.NET Core.

A aplicação permite o gerenciamento de pessoas, transações financeiras e consultas de informações consolidadas.

## Estrutura

```text
.
├── backend/      API ASP.NET Core
├── frontend/     Aplicação React
├── docs/         Documentação do projeto
├── docker-compose.yml
└── .env.example
```

## Executando com Docker

Crie o arquivo de ambiente:

```bash
cp .env.example .env
```

Suba a aplicação:

```bash
docker compose up --build
```

Serviços disponíveis:

| Serviço | Endereço |
| --- | --- |
| Frontend | http://localhost:5173 |
| API | http://localhost:8080 |
| Swagger | http://localhost:8080/swagger |
| Health Check | http://localhost:8080/health |

## Executando manualmente

Consulte os guias específicos:

- [Backend](./backend/README.md)
- [Frontend](./frontend/README.md)

## Documentação

- [Decisões técnicas](./docs/technical-decisions.md)
- [Escopo do projeto](./docs/project-scope.md)

## Licença

Este projeto está disponível sob a licença MIT.
