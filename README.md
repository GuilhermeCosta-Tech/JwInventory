# 📟 JwInventory V1 - Gerenciador de Inventário com Autenticação por Papéis (Roles)

Olá, seja muito bem-vindo(a) à primeira versão do meu primeiro projeto pessoal consistente! *JwInventory*, uma API de backend segura para gerenciamento de inventário, construída com *ASP.NET Core 8* e os princípios da *Clean Architecture*.

Este projeto vai além do conceito de um simples CRUD, me desafiando durante minha jornada de aprendizado e implementando um sistema de autenticação e autorização completo com *JSON Web Tokens (JWT)*, e um controle de acesso granular baseado em três níveis de papéis de usuário: *Administrador, Gerente e Colaborador*.

---

## 👨🏻‍💻 Funcionalidades Implementadas

*   🔐 *Sistema de Autenticação e Autorização Completo*:
    *   Registro de usuários com senhas seguras (hash).
    *   Login com geração de token JWT (com tempo limitado).
    *   Validação de token em endpoints protegidos.
*   👤 *Controle de Acesso Baseado em Papéis (Roles)*:
    *   *Administrador*: Acesso total ao sistema, incluindo gerenciamento de produtos e endpoints administrativos.
    *   *Gerente*: Permissão para criar e atualizar produtos.
    *   *Colaborador*: Acesso de apenas leitura aos recursos permitidos.
*   📦 *CRUD de Produtos com Permissões*:
    *   Listagem e visualização de produtos (público).
    *   Criação e atualização de produtos (restrito a Gerentes e Admins).
    *   Exclusão de produtos (restrito a Admins).
*   ⚙ *Endpoints Administrativos*:
    *   Área segura (/api/admin) com endpoints que só podem ser acessados por administradores.
    *   Endpoint de diagnóstico (/api/admin/me) para verificar os dados do usuário autenticado.
*   🧱 *Base Sólida e Escalável*:
    *   *Clean Architecture* para separação de responsabilidades.
    *   *Injeção de Dependência* em todo o projeto.
    *   *Entity Framework Core* para persistência de dados com SQL Server.
    *   *Swagger* para documentação e teste interativo da API.

---

## 🛠 Tecnologias e Arquitetura

| Categoria      | Tecnologias / Padrões                                           |
| -------------- | --------------------------------------------------------------- |
| *API*        | ASP.NET Core 8, REST, Swagger (Swashbuckle), Mapeamento com AutoMapper |
| *Domínio*      | Entidades Ricas, Hierarquia de Herança (conceito de TPH para Usuários aplicado)      |
| *Aplicação*    | DTOs, Interfaces de Serviços, CQRS (implícito)                  |
| *Infraestrutura* | Entity Framework Core 8, SQL Server, Padrão Repositório         |
| *Segurança*    | ASP.NET Core Identity, JWT, Claims (Role-based)                 |

---

## 📂 Estrutura do Projeto

A solução segue os princípios da Clean Architecture, garantindo baixo acoplamento e alta coesão.


  
│ JwInventory.sln

├── 📁 JwInventory.Domain              # Contém as entidades e regras de negócio centrais. 

│
└── Entities (PessoaComAcesso, Product, AdminUser , etc.)

│
├── 📁 JwInventory.Application         # Define a lógica da aplicação e os casos de uso.
│  

│
└── DTOs (Data Transfer Objects)  

│
└── Interfaces (IAuthService, IProductRepository, etc.)

│
├── 📁 JwInventory.Infrastructure      # Implementa os detalhes técnicos (acesso a dados, serviços externos). 

├── Data (DbContext, Migrations)

│
├── Repositories (User Repository, ProductRepository) 

│
├── Services (AuthService)

│
└── Security (JwtTokenGenerator)

│
└── 📁 JwInventory.API                  # Ponto de entrada da API, controllers e configuração.
    └── Controllers (AuthController, ProductController, AdminController)

---

## 🗺 Guia da API (Endpoints)

### Autenticação (/api/auth)

| Método HTTP | Endpoint             | Descrição                                                              | Acesso Necessário |
| :---------- | :------------------- | :--------------------------------------------------------------------- | :---------------- |
| POST      | /register          | Registra um novo usuário (pode especificar a role).                    | Público           |
| POST      | /login             | Autentica um usuário e retorna um token JWT.                           | Público           |


<img width="1475" height="162" alt="image" src="https://github.com/user-attachments/assets/88bd4666-567b-48e4-8ed1-b1ba888bdcf1" />




### Produtos (/api/product)

| Método HTTP | Endpoint             | Descrição                                                              | Acesso Necessário |
| :---------- | :------------------- | :--------------------------------------------------------------------- | :---------------- |
| GET       | /                  | Lista todos os produtos.                                               | Público           |
| GET       | /{id}              | Busca um produto por seu ID.                                           | Público           |
| POST      | /                  | Cria um novo produto.                                                  | Admin, Gerente    |
| PUT       | /{id}              | Atualiza um produto existente.                                         | Admin, Gerente    |
| DELETE    | /{id}              | Exclui um produto.                                                     | Admin             |


<img width="1465" height="324" alt="image" src="https://github.com/user-attachments/assets/cf19a5d3-80c7-4d5a-a77f-5fc51217c97e" />


### Administração (/api/admin)

| Método HTTP | Endpoint             | Descrição                                                              | Acesso Necessário |
| :---------- | :------------------- | :--------------------------------------------------------------------- | :---------------- |
| GET       | /me                | Retorna os detalhes (claims) do usuário autenticado.                   | Admin             |
| GET       | /secret            | Endpoint de exemplo para testar o acesso de Admin.                     | Admin             |


<img width="1508" height="254" alt="image" src="https://github.com/user-attachments/assets/65db752d-a330-4bfd-9fbf-56fc764f91fc" />


---

## Como Executar Localmente

> *Pré-requisitos*: [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) e uma instância do SQL Server (pode ser a LocalDB instalada com o Visual Studio).

1.  *Clone o repositório*
    sh
    git clone https://github.com/GuilhermeCosta-Tech/JwInventory.git
    cd JwInventory
    

2.  *Configure a Conexão com o Banco de Dados*
    *   Verifique se a string de conexão no arquivo JwInventory.API/appsettings.Development.json está correta para sua instância do SQL Server. O padrão usa a LocalDB do Visual Studio.
    json
    "ConnectionStrings": {
      "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=JwInventoryDb;Trusted_Connection=True;"
    }
    

3.  *Crie e Atualize o Banco de Dados*
    *   Abra o *Package Manager Console* no Visual Studio, selecione JwInventory.Infrastructure como projeto padrão e execute:
    powershell
    EntityFrameworkCore\Update-Database
    
    *   Isso criará o banco JwInventoryDb e aplicará todas as migrações.

4.  *Execute a Aplicação*
    *   Você pode executar o projeto JwInventory.API diretamente pelo Visual Studio (pressionando F5) ou via linha de comando:
    sh
    dotnet run --project JwInventory.API
    

5.  *Acesse a Documentação Interativa*
    *   Abra seu navegador e acesse: https://localhost:7110/swagger para visualizar todos os endpoints disponíveis

---

## 🧪 Como Testar a API

1.  *Registre um Administrador*
    *   Use o endpoint POST /api/auth/register com o seguinte corpo:
    json
    {
      "name": "Admin User",

      "email": "admin@email.com",
    
      "password": "password123@",
    
      "role": "Admin"
    }


    <img width="1200" height="700" alt="image" src="https://github.com/user-attachments/assets/0afb25fc-7a68-41ba-a5f9-15b349255d6f" />


3.  *Faça Login e Obtenha o Token*
    *   Use POST /api/auth/login com as credenciais do admin.
    *   Copie o token gerado na resposta.
      
  


      <img width="1376" height="81" alt="image" src="https://github.com/user-attachments/assets/26c92355-8398-49b6-a7fe-48523bafc135" />


4.  *Autorize suas Requisições*
    *   No topo da página do Swagger, clique no botão *Authorize (🔒)*.


    *   <img width="468" height="222" alt="image" src="https://github.com/user-attachments/assets/7afc4c89-0f4d-4bad-ac7d-ee9aa62c4851" />


    *   Na janela, digite `Bearer ` (com um espaço no final) e cole o seu token. Ex: Bearer eyJhbGciOi...
      

      <img width="468" height="222" alt="image" src="https://github.com/user-attachments/assets/7e9b43c9-ded9-4fd8-adf7-bb1d2c79f957" />


    *   Clique em Authorize e logo você verá a confirmação da autenticação.


      <img width="468" height="222" alt="image" src="https://github.com/user-attachments/assets/5e82d9dc-d0ae-4d8c-8feb-2c596963d0b5" />

5.  *Teste um Endpoint Protegido*
    *   Agora, execute GET /api/admin/me. Você deverá receber uma resposta 200 OK com os detalhes do seu usuário.
  
      <img width="468" height="222" alt="image" src="https://github.com/user-attachments/assets/8525ffa0-82d7-47b9-a3e8-fa0829faf7ea" />


6.  *Teste a Restrição de Acesso*
    *   Registre um novo usuário com a role "Colaborador".
    *   Faça login com ele e autorize com o novo token.
    *   Tente acessar GET /api/admin/me novamente. Você receberá um erro *403 Forbidden*, provando que a segurança por papéis está funcionando!
  
      <img width="468" height="222" alt="image" src="https://github.com/user-attachments/assets/04162ced-c8ea-41b0-879f-443a3646b93c" />


---

## 📌 Próximos Passos

*   [ ] *Testes Unitários e de Integração*: Adicionar testes para os serviços e controllers.
*   [ ] *Logging e Monitoramento*: Implementar um sistema de logs robusto.
*   [ ] *Refinamento de Validação*: Usar FluentValidation para DTOs.
*   [ ] *Frontend*: Construir uma interface de usuário (Blazor) para consumir a API.
*   [ ] *CI/CD e Deploy*: Configurar um pipeline de integração contínua e fazer o deploy em um provedor de nuvem (GCP).

---

## 👨‍💼 Autor

*Guilherme dos Santos Costa*
<br>
📚 Estudante de Análise e Desenvolvimento de Sistemas
<br>
🚀 Apaixonado por tecnologia e as soluções que ela pode proporcionar

🔗 [![LinkedIn](https://img.shields.io/badge/LinkedIn-0A66C2?logo=linkedin&logoColor=white&style=flat-square)](www.linkedin.com/in/guilhermecosta-tech) | |  [![Email](https://img.shields.io/badge/Email-D14836?logo=gmail&logoColor=white)](mailto:guilhermecosta.tech@gmail.com)
