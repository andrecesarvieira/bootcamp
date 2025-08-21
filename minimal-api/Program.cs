using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MinimalApi.Dominio.DTOs;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.ModelViews;
using MinimalApi.Dominio.Servicos;
using MinimalApi.Dominio.Token;
using MinimalApi.Dominio.Validacoes;
using MinimalApi.Infraestrutura.Db;
using MinimalApi.Infraestrutura.Interfaces;

var builder = WebApplication.CreateBuilder(args);

//Dependências
builder.Services.AddScoped<IUsuarioServico, UsuarioServico>();
builder.Services.AddScoped<IVeiculoServico, VeiculoServico>();

//MySql
builder.Services.AddDbContext<DbContexto>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("MySql"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MySql"))));

//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Token
//Token Jwt
byte[] key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt"]!);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(op =>
    {
        op.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

builder.Services.AddAuthorization();
#endregion

var app = builder.Build();

#region Home
//Rota principal
app.MapGet("/", (HttpContext context) =>
{
    Home home = new();
    string linkSwagger = home.SwaggerUrl;
    string msg = home.Mensagem;

    return Results.Text($"<h2>{msg}</h2><a href='{linkSwagger}'>Swagger</a>", "text/html; charset=utf-8");
    
}).WithTags("Home");
#endregion

#region Login
//Rota login usuarios
app.MapPost("/login", ([FromBody] LoginDTO loginDTO, IUsuarioServico usuarioServico) =>
{
    TokenJwt token = new();
    var usuarioLogin = usuarioServico.Login(loginDTO);

    if (usuarioLogin != null)
    {
        string tokenGerado = token.GerarTokenJwt(key, usuarioLogin);
        return Results.Ok(new UsuarioLogado
        {
            Email = usuarioLogin.Email,
            Perfil = usuarioLogin.Perfil,
            Token = tokenGerado
        });
    }
    else
    {
        return Results.Unauthorized();
    }

}).WithTags("Login");
#endregion

#region Administrador
//Rota incluir usuario
app.MapPost("/admin/incluirUsuario", ([FromBody] UsuarioDTO usuarioDTO, IUsuarioServico usuarioServico) =>
{
    //Validar dados preenchidos
    ErrosDeValidacao resultado = new UsuarioValidacoes().ValidaDTO(usuarioDTO);
    if (resultado.Mensagens.Count != 0) return Results.BadRequest(resultado);

    //Verificar se já existe um usuário com o mesmo email
    if (usuarioServico.ObterUsuarioPorEmail(usuarioDTO.Email) != null)
    {
        return Results.BadRequest("Email já cadastrado.");
    }

    //Cadastrar novo usuário
    Usuario novoUsuario = new()
    {
        Email = usuarioDTO.Email,
        Senha = usuarioDTO.Senha,
        Perfil = usuarioDTO.Perfil.ToString()
    };
    usuarioServico.IncluirUsuario(novoUsuario);

    return Results.Created($"/admin/usuario/{novoUsuario.Id}", novoUsuario);

}).RequireAuthorization().WithTags("Administrador");

//Rota atualizar usuario
app.MapPut("/admin/atualizarUsuario/{email}", ([FromRoute] string email, UsuarioDTO usuarioDTO, IUsuarioServico usuarioServico) =>
{
    Usuario? usuario = usuarioServico.ObterUsuarioPorEmail(email);
    if (usuario == null)
    {
        return Results.NotFound("Email não encontrado");
    }

    ErrosDeValidacao resultado = new UsuarioValidacoes().ValidaDTO(usuarioDTO);
    if (resultado.Mensagens.Count != 0) return Results.BadRequest(resultado);

    usuario.Email = usuarioDTO.Email;
    usuario.Senha = usuarioDTO.Senha;
    usuario.Perfil = usuarioDTO.Perfil.ToString();

    usuarioServico.AtualizarUsuario(usuario);

    return Results.Ok(usuario);

}).RequireAuthorization().WithTags("Administrador");

//Rota excluir usuario
app.MapDelete("/admin/excluirUsuario/{email}", ([FromRoute] string email, IUsuarioServico usuarioServico) =>
{
    Usuario? usuario = usuarioServico.ObterUsuarioPorEmail(email);
    if (usuario == null)
    {
        return Results.NotFound("Email não encontrado");
    }

    usuarioServico.ExcluirUsuario(usuario.Id);

    return Results.NoContent();

}).RequireAuthorization().WithTags("Administrador");

//Rota buscar todos os usuarios
app.MapGet("/admin/obterTodosUsuarios", ([FromQuery] int pagina, IUsuarioServico usuarioServico) =>
{
    List<Usuario> usuarios = usuarioServico.ObterTodosUsuarios(pagina);

    return Results.Ok(usuarios);

}).RequireAuthorization().WithTags("Administrador");

//Rota obter usuario por email
app.MapGet("/admin/obterUsuarioPorEmail/{email}", ([FromRoute] string email, IUsuarioServico usuarioServico) =>
{
    Usuario? usuario = usuarioServico.ObterUsuarioPorEmail(email);

    return usuario != null ? Results.Ok(usuario) : Results.NotFound();

}).RequireAuthorization().WithTags("Administrador");

//Rota buscar usuario por Id
app.MapGet("/admin/obterUsuarioPorId/{id}", ([FromRoute] int id, IUsuarioServico usuarioServico) =>
{
    Usuario? usuario = usuarioServico.ObterUsuarioPorId(id);

    return usuario != null ? Results.Ok(usuario) : Results.NotFound();
    
}).RequireAuthorization().WithTags("Administrador");
#endregion

#region Veiculos
//Rota incluir veiculos
app.MapPost("/veiculos/incluir", ([FromBody] VeiculoDTO veiculoDTO, IVeiculoServico veiculoServico) =>
{
    var resultado = new VeiculoValidacoes().ValidaDTO(veiculoDTO);

    if (resultado.Mensagens.Count != 0) return Results.BadRequest(resultado);

    Veiculo veiculo = new()
    {
        Nome = veiculoDTO.Nome,
        Marca = veiculoDTO.Marca,
        Ano = veiculoDTO.Ano
    };
    veiculoServico.IncluirVeiculo(veiculo);

    return Results.Created($"/veiculo/{veiculo.Id}", veiculo);
    
}).WithTags("Veiculos");

//Rota atualizar veiculo
app.MapPut("/veiculos/atualizar/{id}", ([FromRoute] int id, VeiculoDTO veiculoDTO,IVeiculoServico veiculoServico) =>
{
    Veiculo? veiculo = veiculoServico.ObterVeiculoPorId(id);

    if (veiculo == null)
    {
        return Results.NotFound();
    }

    var resultado = new VeiculoValidacoes().ValidaDTO(veiculoDTO);

    if (resultado.Mensagens.Count != 0) return Results.BadRequest(resultado);

    veiculo.Nome = veiculoDTO.Nome;
    veiculo.Marca = veiculoDTO.Marca;
    veiculo.Ano = veiculoDTO.Ano;

    veiculoServico.AtualizarVeiculo(veiculo);

    return Results.Ok(veiculo);

}).WithTags("Veiculos");

//Rota excluir veiculo
app.MapDelete("/veiculos/excluir/{id}", ([FromRoute] int id, IVeiculoServico veiculoServico) =>
{
    if (veiculoServico.ObterVeiculoPorId(id) == null)
    {
        return Results.NotFound();
    }

    veiculoServico.ExcluirVeiculo(id);

    return Results.NoContent();

}).WithTags("Veiculos");

//Rota buscar todos os veiculos
app.MapGet("/veiculos/obterTodos", ([FromQuery] int pagina, IVeiculoServico veiculoServico) =>
{
    List<Veiculo> veiculos = veiculoServico.ObterTodosVeiculos(pagina);

    return Results.Ok(veiculos);

}).WithTags("Veiculos");

//Rota buscar veiculos por Id
app.MapGet("/veiculos/obterPorId/{id}", ([FromRoute] int id, IVeiculoServico veiculoServico) =>
{
    Veiculo? veiculo = veiculoServico.ObterVeiculoPorId(id);

    return veiculo != null ? Results.Ok(veiculo) : Results.NotFound();
    
}).WithTags("Veiculos");
#endregion

//SwaggerUI
app.UseSwagger();
app.UseSwaggerUI();

//Jwt
app.UseAuthentication();
app.UseAuthorization();

app.Run();