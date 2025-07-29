# 📟 JwInventory - Gerenciador de Inventário com Autenticação JWT

Seja bem-vindo ao **JwInventory**, uma aplicação robusta e escalável para gerenciamento de inventário que traz segurança, modularidade e organização com base nos princípios da **Clean Architecture**.

Desenvolvido com foco em boas práticas e extensibilidade, o sistema já oferece uma base sólida com **autenticação segura via JWT** e controle por **roles de usuário**.

---

## 🚀 Funcionalidades Principais

* 🔐 **Autenticação segura com JWT**
* 👤 **Registro e login de usuários**
* 📋 **Gerenciamento de usuários com papel (Role)**
* ⚙️ **API RESTful estruturada com ASP.NET Core**
* 🧱 **Arquitetura limpa (Clean Architecture)**
* 🧪 **Integração com Swagger para testes de endpoints**
* 💄 **Persistência com Entity Framework Core + SQL Server**

---

## 🏢 Tecnologias e Conceitos Utilizados

| Camada         | Tecnologias / Padrões                                     |
| -------------- | --------------------------------------------------------- |
| API            | ASP.NET Core 8, Swagger (Swashbuckle)                     |
| Domínio        | Entidades ricas, Enum de Role (`UserRole`)                |
| Aplicação      | DTOs, Interfaces, Serviços de Autenticação                |
| Infraestrutura | EF Core, SQL Server, Repositórios, Injeção de dependência |
| Segurança      | JWT, Claims, Policies (em progresso)                      |
| Autenticação   | Middleware JWT + Claims de Role                           |

---

## 📂 Estrutura do Projeto

```
JwInventory
🔘 JwInventory.API                // Ponto de entrada da API
️ Controllers                // Endpoints públicos e protegidos
🔘 JwInventory.Application       // DTOs, interfaces, contratos
🔘 JwInventory.Domain            // Entidades e enums do domínio
🔘 JwInventory.Infrastructure    // Repositórios, serviços, JWT, EF Core
```

---

## 🧲 Como executar o projeto localmente

> Pré-requisitos: [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0), SQL Server, Visual Studio ou VS Code

1. **Clone o repositório**

```bash
git clone https://github.com/seu-usuario/JwInventory.git
cd JwInventory
```

2. **Configure o banco de dados**

* Edite o `appsettings.json` da API:

```json
"ConnectionString": {
  "DefaultConnection": "Server=localhost;Database=JwInventoryDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

3. **Crie o banco**

```bash
dotnet ef database update --project JwInventory.Infrastructure --startup-project JwInventory.API
```

4. **Execute a aplicação**

```bash
dotnet run --project JwInventory.API
```

5. **Acesse o Swagger**

```
https://localhost:7110/swagger
```

---

## 🔐 Testando a Autenticação

1. Acesse `POST /api/Auth/register` e registre um usuário.
2. Acesse `POST /api/Auth/login` com o mesmo usuário.
3. Copie o token JWT retornado e clique em **Authorize (🔒)** no Swagger.
4. Insira:

```
Bearer seu_token_aqui
```

Agora você poderá acessar endpoints protegidos!

---

## 📌 Próximos Passos

*  Implementar roles e policies de acesso (`[Authorize(Roles = "Admin")]`)
* CRUD completo para produtos e inventário
* Interface Blazor
* Deploy com Docker + Cloud Run (GCP)

---

## 💡 Motivação do Projeto

Este projeto nasceu com o objetivo de aplicar conceitos sólidos de arquitetura e segurança em um cenário real de backend. Ele serve como uma vitrine técnica para demonstrar:

* Organização de código limpo e desacoplado
* Controle de autenticação e autorização realista
* Uso moderno de ferramentas .NET no mundo profissional

---

## 👨‍💼 Autor

**Guilherme dos Santos Costa**
📚 Estudante de Análise e Desenvolvimento de Sistemas
🚀 Apaixonado por tecnologia e as soluções que ela pode proporcionar
🔗 [LinkedIn](www.linkedin.com/in/guilhermecosta-tech) • [GitHub]([https://github.com/seu-usuario](https://github.com/GuilhermeCosta-Tech))
