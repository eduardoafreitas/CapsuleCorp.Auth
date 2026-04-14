# CapsuleCorp.Auth.API 💊

[![.NET 9](https://img.shields.io/badge/.NET-9.0-blueviolet)](https://dotnet.microsoft.com/download/dotnet/9.0)
[![EF Core](https://img.shields.io/badge/EF%20Core-SQLite-blue)](https://learn.microsoft.com/ef/core/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

Sistema de autenticação robusto desenvolvido em .NET 9, focado em performance, segurança e uma arquitetura limpa. Este projeto faz parte do ecossistema tecnológico da Capsule Corp, bem como meus estudos.

## 🚀 Status do Projeto
O projeto encontra-se na fase inicial de implementação do módulo de autenticação. Atualmente, suporta o registo de utilizadores com persistência em base de dados e segurança via hashing.

## 🛠 Tecnologias Utilizadas

- **Runtime:** .NET 9.0 (ASP.NET Core)
- **Base de Dados:** SQLite (leve e eficiente para desenvolvimento)
- **ORM:** Entity Framework Core 9
- **Segurança:** BCrypt.Net-Next (Hashing de passwords)
- **Documentação:** Swagger (Swashbuckle)
- **Ferramentas:** Docker, DBeaver

## 🏗 Arquitetura
O projeto segue os princípios da **Clean Architecture** (em evolução), visando a separação de responsabilidades. No momento, a estrutura foca em:
- **Controllers:** Exposição dos endpoints REST.
- **Models:** Entidades e DTOs (Data Transfer Objects).
- **Data:** Contexto do EF Core e Migrations.
- **Services (Próximo Passo):** Lógica de negócio isolada.

## 📋 Funcionalidades Implementadas
- [x] Configuração base do ASP.NET Core 9.
- [x] Contexto da base de dados com SQLite.
- [x] Entidade `User` com campos: `Id`, `Name`, `Email`, `PasswordHash`, `CreateDate` e `LastUpdateDate`.
- [x] Endpoint de Registo (`POST /api/Auth/register`).
- [x] Criptografia de passwords com BCrypt.

## ⚙️ Como executar

### Pré-requisitos
- .NET 9 SDK instalado.
- Visual Studio 2022 ou VS Code.

### Passo a passo
1. Clone o repositório:
   ```bash
   git clone [https://github.com/eduardoafreitas/CapsuleCorp.Auth.API.git](https://github.com/teu-utilizador/CapsuleCorp.Auth.API.git)
