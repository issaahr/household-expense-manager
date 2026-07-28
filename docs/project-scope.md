# Project Scope

Este documento descreve algumas decisões adotadas para manter a implementação compatível com o escopo proposto pelo desafio técnico.

## Objetivo

A implementação busca atender integralmente aos requisitos funcionais descritos no desafio, priorizando simplicidade, legibilidade e organização do código.

---

## Autenticação

Não foi implementado sistema de autenticação ou autorização por não fazer parte do escopo solicitado.

Toda a aplicação considera um único contexto de utilização.

---

## Ownership dos recursos

Como consequência da ausência de autenticação, pessoas e transações não possuem proprietário.

Todos os recursos pertencem ao mesmo contexto da aplicação.

---

## Operações disponíveis

Nesse momento, foram implementadas apenas as operações solicitadas no desafio.

---

## Persistência

Os dados permanecem persistidos após o encerramento da aplicação através do PostgreSQL.

---

## Documentação

A documentação dos endpoints é disponibilizada automaticamente através do Swagger.

Além disso, o projeto utiliza comentários XML nos principais componentes públicos para complementar a documentação gerada.

---

## Testes

Foram implementados testes unitários no backend e frontend para validar regras de negócio, validações e comportamento dos principais componentes da aplicação.

O foco dos testes foi garantir o comportamento esperado das regras descritas no desafio.

---

## Recursos adicionais

Foram adicionados alguns recursos que não alteram o comportamento solicitado, mas tornam a aplicação mais próxima de um ambiente real de desenvolvimento, como:

- validação utilizando FluentValidation;
- validação de formulários no frontend utilizando React Hook Form e Zod;
- componentes de interface utilizando Material UI;
- middleware global para tratamento de exceções;
- documentação via Swagger/OpenAPI;
- Docker Compose para execução completa da aplicação;
- aplicação automática das migrations em ambiente de desenvolvimento.
