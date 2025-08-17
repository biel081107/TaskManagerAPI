# 📌 API REST para Gerenciamento de Tarefas

## 🇧🇷 Versão em Português

### 📖 Descrição
API REST simples e extensível para gerenciamento de tarefas, construída com ASP.NET Core e Entity Framework Core sobre SQLite. O objetivo do projeto é fornecer uma base prática para criar, consultar, atualizar e remover tarefas (CRUD), com foco em boas práticas de arquitetura, persistência e facilidade de deploy (Docker). Ideal como template para aplicações pequenas, provas de conceito ou para estudo de desenvolvimento web em .NET.

Principais funcionalidades:
- Gerenciamento completo de tarefas (criar, listar, atualizar, excluir).
- Marcação de tarefas como concluídas, pendentes e em progresso.
- Validações de entrada e tratamento de erros consistente na API.
- Persistência local com SQLite e suporte a migrations via EF Core.
- Estrutura preparada para adicionar testes.
- Sistema de autenticação e autorização (JWT): cada usuário tem suas próprias tarefas e só pode criar, visualizar, editar ou excluir as suas.

Como este projeto ajuda:
- Serve como exemplo prático de uma API REST bem organizada em ASP.NET Core.
- Facilita aprendizado de padrões comuns (DTOs, serviços, repositórios e migrations).
- Demonstra controle de acesso por usuário e separação de dados entre contas.
- Pode ser estendido para integração com front-end, autenticação por outros provedores ou armazenamento em outros bancos.
- Configuração simples para rodar localmente e em ambiente containerizado.

### 🚀 Tecnologias Utilizadas
- ASP.NET Core
- Entity Framework Core
- SQLite
- Docker
- JWT
- RESTFull API

### ⚙️ Como Rodar o Projeto

1. **Clone o repositório**
   git clone https://github.com/seu-usuario/nome-do-repo.git

2. **Acesse a pasta do projeto**
    cd nome-do-repo

3. **Restaure os pacotes**
    dotnet restore

4. **Aplicar as migrações e criar o banco de dados**
    dotnet ef migrations add InitialCreate
    dotnet ef database update

5. **Rodar o projeto**
    dotnet run

6. **Acessar Via Swagger(Se quiser)**
    /{url}/swagger

**📌 Endpoints Principais**

**🔑 Autenticação**
    POST /api/autenticacao/login → Realiza login e retorna token JWT
    POST /api/autenticacao/register → Realiza o cadastro do usuario

👤 Usuários

GET /api/usuarios/perfil → retorna um perfil com as informações do usuario

✅ Tarefas

GET /api/tarefas → Lista todas as tarefas do usuario logado

POST /api/tarefas → Cria uma nova tarefa para o usuario

PUT /api/tarefas/{id} → Atualiza uma tarefa

DELETE /api/tarefas/{id} → Remove uma tarefa


**🔒 Autenticação JWT**

Alguns endpoints exigem autenticação via Bearer Token.
Exemplo de uso no header:

Authorization: Bearer {seu_token_jwt}

**🛠️ Autor**

Desenvolvido por Gabriel Olímpio
📧 Email: contatoolimpiodev@gmail.com
🌐 GitHub: https://github.com/biel081107


