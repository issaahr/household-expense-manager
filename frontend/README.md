# Frontend

Aplicação web desenvolvida em React utilizando TypeScript, Vite e Material UI.

<p align="center">
  <img src="https://img.shields.io/badge/React-19-61DAFB?logo=react&logoColor=black" />
  <img src="https://img.shields.io/badge/TypeScript-5.9-3178C6?logo=typescript&logoColor=white" />
  <img src="https://img.shields.io/badge/Vite-8-646CFF?logo=vite&logoColor=white" />
  <img src="https://img.shields.io/badge/Material%20UI-9-007FFF?logo=mui&logoColor=white" />
  <img src="https://img.shields.io/badge/Vitest-4-6E9F18?logo=vitest&logoColor=white" />
</p>

## Estrutura

```text
frontend
├── src
│   ├── components
│   ├── hooks
│   ├── layout
│   ├── pages
│   ├── schemas
│   ├── services
│   ├── tests
│   ├── types
│   └── utils
└── public
```

## Organização

O projeto segue uma organização baseada em componentes e separação de responsabilidades.

- Components contém componentes reutilizáveis e componentes específicos de domínio.
- Hooks centralizam lógica de estado e operações relacionadas aos recursos.
- Services são responsáveis pela comunicação com a API.
- Schemas contém validações utilizando Zod.
- Types definem contratos TypeScript utilizados pela aplicação.
- Pages representam as telas principais da aplicação.
- Layout contém estruturas compartilhadas de composição da interface.
- Utils contém funções auxiliares reutilizáveis.

## Executando sem Docker

Instale as dependências:

```bash
npm install
```

Crie o arquivo `.env`:

```env
VITE_API_URL=http://localhost:8080
```

Execute:

```bash
npm run dev
```

## Executando com Docker

Pela raiz do projeto:

```bash
docker compose up frontend
```

## Qualidade de código

Lint:

```bash
npm run lint
```

Testes:

```bash
npm run test:run
```

O projeto utiliza Husky e lint-staged para validações automáticas durante o desenvolvimento.

## Build

```bash
npm run build
```
