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
	Abra `http://localhost:5033/swagger` no navegador para testar os endpoints.

## Testes de Request Automatizados

Os testes de request da API são organizados na pasta `Tests/`, separados por contexto em subpastas:

```
Tests/
  Auth/
	 auth.http        # Testes de autenticação/login
	 token.http       # Geração e uso automático do token JWT
  Usuarios/
	 usuarios.http    # Testes de endpoints de usuário (listar, buscar, cadastrar, excluir)
  Veiculos/
	 veiculos.http    # Testes de endpoints de veículos
```

Utilize a extensão [REST Client](https://marketplace.visualstudio.com/items?itemName=humao.rest-client) no VS Code para executar os arquivos `.http` e automatizar seus testes de API.

### Como usar os arquivos `.http`
1. Instale a extensão REST Client no VS Code.
2. Abra qualquer arquivo `.http` na pasta `Tests/`.
3. Clique em "Send Request" acima da requisição desejada para executá-la e ver a resposta.
4. Use variáveis como `@baseUrl` e `@token` para facilitar a reutilização de valores.
5. O arquivo `token.http` permite capturar o token JWT automaticamente após o login e reutilizá-lo nos demais testes.

### Exemplo de request para cadastro de usuário
```http
@baseUrl = http://localhost:5033
@token = <seu_token_aqui>

POST {{baseUrl}}/admin/incluirUsuario
Content-Type: application/json
Authorization: Bearer {{token}}

{
  "email": "novo@teste.com",
  "senha": "123",
  "perfil": "Usuario"
}
```

### Dicas
- Sempre confira se o endpoint, método HTTP e payload estão corretos (compare com o Swagger se necessário).
- Para endpoints protegidos, lembre-se de enviar o token JWT no header Authorization.
- Use o Swagger para explorar e validar os endpoints manualmente.

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
