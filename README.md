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

> ⚠️ **Atenção:** o projeto possui dois servidores independentes — backend e frontend.
> Ambos precisam estar rodando ao mesmo tempo para o sistema funcionar.

### Clonando o repositório

```bash
git clone https://github.com/martoxm/ControleGastos.git
cd ControleGastos
```

### Pré-requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/) — necessário para compilar e rodar o backend
- [Node.js 20+](https://nodejs.org/) — necessário para rodar o frontend React. O `npm` já vem incluído na instalação do Node.js

> 💡 Para verificar se já possui o Node.js instalado, execute `node -v` no terminal.
> Para verificar o .NET, execute `dotnet --version`.

---

### 1. Backend

O backend pode ser iniciado pelo **Visual Studio** ou pelo **terminal**.

**Visual Studio 2026**

Abra o arquivo `ControleGastos.slnx`, selecione o perfil **`http`** no botão de execução e pressione **F5**.

**Terminal (VS Code, PowerShell ou CMD)**

```bash
cd backend/ControleGastos.API
dotnet run --launch-profile http
```

✅ API disponível em `http://localhost:5176`
✅ Swagger disponível em `http://localhost:5176`
✅ O banco de dados SQLite é criado automaticamente na primeira execução

---

### 2. Frontend

> ⚠️ **O frontend não pode ser iniciado pelo Visual Studio.**
> Use obrigatoriamente um terminal — recomendamos o terminal integrado do **VS Code**.

Abra a pasta `frontend` no VS Code, acesse o terminal integrado (`Ctrl + '`) e execute:

```bash
npm install
npm run dev
```

✅ Frontend disponível em `http://localhost:5173`

---

### Atalho — Windows

Prefere subir tudo de uma vez? Execute o arquivo **`iniciar.bat`** na raiz do projeto.
Ele abre o backend e o frontend automaticamente em janelas separadas.

---

## Estrutura do Projeto

```bash
ControleGastos/
├── backend/
│ ├── ControleGastos.API/ # Controllers, middlewares e configuração da aplicação
│ ├── ControleGastos.Application/ # Use Cases organizados por entidade e operação
│ ├── ControleGastos.Domain/ # Entidades, enums, interfaces e exceções de domínio
│ └── ControleGastos.Infrastructure/ # Repositórios, DbContext e Migrations
└── frontend/
└── src/
├── components/ # Componentes reutilizáveis (Navbar)
├── pages/ # Telas da aplicação (Pessoas, Transações, Totais)
├── services/ # Comunicação com a API via Axios
└── types/ # Interfaces TypeScript (Request e Response por entidade)
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
