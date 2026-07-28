# Technical Decisions

## Architecture

### Layered Architecture

Foi adotada uma arquitetura em camadas, separando responsabilidades entre Controllers, Services, Validators, Data, DTOs e Entities.

Essa abordagem mantém o projeto organizado e de fácil manutenção sem introduzir a complexidade de arquiteturas mais robustas, como Clean Architecture ou CQRS, que seriam desproporcionais para o escopo deste desafio.

---

## Frontend

### Componentização e estado

A aplicação frontend utiliza componentes React separados por domínio, hooks para centralização de lógica de estado e services para comunicação com a API.

As validações dos formulários são realizadas utilizando React Hook Form integrado ao Zod.

---

## Persistence

### PostgreSQL

O projeto utiliza PostgreSQL como banco de dados relacional.

A escolha foi motivada pela integração com o Entity Framework Core e pelo amplo uso da tecnologia em aplicações corporativas.

### Entity Framework Core

O mapeamento objeto-relacional foi realizado utilizando Fluent API (`IEntityTypeConfiguration<T>`), mantendo as entidades livres de detalhes de persistência.

### Cascade Delete

A remoção de uma pessoa remove automaticamente todas as suas transações através do relacionamento configurado no banco de dados.

Essa abordagem mantém a integridade referencial e implementa diretamente uma das regras do desafio.

---

## Validation

As validações foram divididas em dois níveis.

### FluentValidation

Responsável pela validação estrutural dos dados de entrada.

Exemplos:

- campos obrigatórios;
- tamanhos mínimos e máximos;
- valores positivos;
- enums válidos.

### Services

Responsáveis pelas regras de negócio que dependem do domínio ou do banco de dados.

Exemplos:

- verificar se a pessoa existe;
- impedir cadastro de receitas para menores de idade.

---

## Business Rules

### Rules

Regras reutilizáveis que não dependem de infraestrutura foram isoladas na pasta `Rules`, permitindo reutilização e testes unitários independentes.

---

## Error Handling

Foi implementado um middleware global de tratamento de exceções para centralizar as respostas de erro da API e evitar repetição de código nos controllers.

---

## Domain Modeling

### Identificadores

As entidades utilizam identificadores inteiros auto incrementais.

Para o escopo deste projeto, UUID não oferece benefícios que justifiquem sua adoção.

### Valores monetários

Valores financeiros utilizam `decimal` no backend, evitando problemas de precisão comuns ao tipo `double`.

No frontend, os campos de entrada monetária utilizam texto durante a digitação para permitir o formato brasileiro (0,00). Após validação e conversão, os valores são enviados para a API como números.

### Tipo de transação

Foi utilizado um enum para representar os tipos de transação, eliminando o uso de strings ou valores mágicos na regra de negócio.

---

## Docker

O ambiente de desenvolvimento utiliza Docker Compose contendo:

- PostgreSQL;
- Backend;
- Frontend.

Também foram configurados:

- volume persistente para o banco;
- health check do PostgreSQL;
- aplicação automática das migrations durante a inicialização da API em ambiente de desenvolvimento.
