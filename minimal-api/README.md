 # Minimal API com Controle de Usuários e Roles

Este projeto é uma Minimal API desenvolvida em .NET 9, com autenticação JWT, controle de usuários, perfis (roles) e validações. Ideal para estudos e como base para aplicações REST seguras e modernas.

## Funcionalidades
- Autenticação via JWT
- Controle de acesso por Roles (Administrador, Usuário, etc.)
- Endpoints protegidos por autorização
- Validações de dados
- Estrutura modular e fácil de entender

## Estrutura do Projeto
```
├── Auth/                # Lógica de autenticação e geração de tokens JWT
├── Dtos/                # Data Transfer Objects
├── Endpoints/           # Definição dos endpoints da API
├── Entities/            # Entidades do domínio
├── Extensions/          # Extensões para configuração (Swagger, JWT, etc.)
├── Infrastructure/      # Infraestrutura e contexto de dados
├── Migrations/          # Migrations do Entity Framework
├── Services/            # Serviços de negócio
├── Validations/         # Validações customizadas
├── ViewModels/          # Modelos de visualização e resposta
├── Program.cs           # Configuração principal da aplicação
├── appsettings.json     # Configurações gerais
```

## Como Executar
1. **Pré-requisitos:**
	- .NET 9 SDK
	- SQL Server (ou ajuste o provider no `DbContext`)

2. **Restaurar pacotes:**
	```sh
	dotnet restore
	```

3. **Aplicar migrations:**
	```sh
	dotnet ef database update
	```

4. **Executar a aplicação:**
	```sh
	dotnet run
	```

5. **Acessar o Swagger:**
	Abra `http://localhost:5000/swagger` no navegador para testar os endpoints.

## Controle de Roles
- O token JWT inclui a role do usuário.
- Use `[Authorize(Roles = "Administrador")]` ou `.RequireAuthorization("Admin")` nos endpoints para restringir o acesso.
- As roles são definidas no cadastro do usuário e propagadas no token.

## Exemplo de Endpoint Protegido
```csharp
app.MapGet("/admin", () => "Área restrita").RequireAuthorization("Admin");
```

## Contribuição
Pull requests são bem-vindos! Sinta-se à vontade para sugerir melhorias ou reportar problemas.

## Licença
MIT
