# Controle de Gastos Residenciais

Sistema web para controle de gastos residenciais com cadastro de pessoas, cadastro de transações financeiras e consulta de totais por pessoa.

---

## Tecnologias

**Backend**

- .NET 10 com C#
- ASP.NET Core Web API
- Entity Framework Core com SQLite
- Swagger / OpenAPI
- Arquitetura: DDD + SOLID

**Frontend**

- React 19 com TypeScript
- Vite
- React Router DOM
- Axios

---

## Funcionalidades

- **Cadastro de Pessoas** — criar, listar e remover pessoas. Ao remover uma pessoa, todas as suas transações são apagadas automaticamente
- **Cadastro de Transações** — criar e listar transações do tipo despesa ou receita vinculadas a uma pessoa
- **Regra de negócio** — pessoas menores de 18 anos só podem cadastrar transações do tipo despesa
- **Consulta de Totais** — exibe total de receitas, despesas e saldo de cada pessoa, com consolidado geral ao final

---

## Como executar

### Pré-requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/)
- [Node.js 20+](https://nodejs.org/)

### Backend

> ⚠️ O backend deve rodar obrigatoriamente pelo perfil **http** na porta `5176`.

**Opção 1 — Visual Studio**

Abra o arquivo `ControleGastos.slnx`, selecione o perfil **`http`** no botão de execução e pressione **F5**.

**Opção 2 — VS Code ou qualquer terminal**

Abra a pasta raiz do projeto no VS Code, acesse o terminal integrado e execute:

```bash
cd backend/ControleGastos.API
dotnet run --launch-profile http
```

A API ficará disponível em `http://localhost:5176`.
O Swagger abrirá automaticamente em `http://localhost:5176`.

> O banco de dados SQLite é criado e migrado automaticamente ao iniciar a aplicação.

### Frontend

Abra a pasta `frontend` no VS Code e execute no terminal integrado:

```bash
npm install
npm run dev
```

O frontend ficará disponível em `http://localhost:5173`.

> ⚠️ O backend precisa estar rodando antes de iniciar o frontend.

---

## Estrutura do Projeto

```bash

ControleGastos/
├── backend/
│ ├── ControleGastos.API/ # Controllers e configuração da aplicação
│ ├── ControleGastos.Application/ # DTOs, interfaces e serviços de aplicação
│ ├── ControleGastos.Domain/ # Entidades, enums e regras de negócio
│ └── ControleGastos.Infrastructure/ # Repositórios e contexto do banco de dados
└── frontend/
└── src/
├── components/ # Componentes reutilizáveis (Navbar)
├── pages/ # Telas da aplicação
├── services/ # Comunicação com a API
└── types/ # Tipagem TypeScript dos dados

```

---

## Endpoints da API

| Método | Rota                  | Descrição                            |
| ------ | --------------------- | ------------------------------------ |
| GET    | `/api/pessoas`        | Lista todas as pessoas               |
| POST   | `/api/pessoas`        | Cadastra uma nova pessoa             |
| DELETE | `/api/pessoas/{id}`   | Remove uma pessoa e suas transações  |
| GET    | `/api/pessoas/totais` | Retorna o relatório financeiro geral |
| GET    | `/api/transacoes`     | Lista todas as transações            |
| POST   | `/api/transacoes`     | Cadastra uma nova transação          |
