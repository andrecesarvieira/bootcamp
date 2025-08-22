using Microsoft.AspNetCore.Mvc;
using MinimalApi.Dominio.Validacoes;
using MinimalApi.Dtos;
using MinimalApi.Entities;
using MinimalApi.Infrastructure.Interfaces;
using MinimalApi.ViewModels;

namespace MinimalApi.Endpoints
{
    public static class UsuariosEndpoints
    {
        public static void MapUsuarioEndpoints(this IEndpointRouteBuilder app)
        {
            //Rota incluir usuario
            app.MapPost("/admin/incluirUsuario", ([FromBody] UsuarioDto usuarioDTO, IUsuarioServico usuarioServico) =>
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

            }).WithTags("Administrador").RequireAuthorization();

            //Rota atualizar usuario
            app.MapPut("/admin/atualizarUsuario/{email}", ([FromRoute] string email, UsuarioDto usuarioDTO, IUsuarioServico usuarioServico) =>
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

            }).WithTags("Administrador").RequireAuthorization();

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

            }).WithTags("Administrador").RequireAuthorization();

            //Rota obter usuario por email
            app.MapGet("/admin/obterUsuarioPorEmail/{email}", ([FromRoute] string email, IUsuarioServico usuarioServico) =>
            {
                Usuario? usuario = usuarioServico.ObterUsuarioPorEmail(email);

                return usuario != null ? Results.Ok(usuario) : Results.NotFound();

            }).WithTags("Administrador").RequireAuthorization();

            //Rota buscar usuario por Id
            app.MapGet("/admin/obterUsuarioPorId/{id}", ([FromRoute] int id, IUsuarioServico usuarioServico) =>
            {
                Usuario? usuario = usuarioServico.ObterUsuarioPorId(id);

                return usuario != null ? Results.Ok(usuario) : Results.NotFound();

            }).WithTags("Administrador").RequireAuthorization();

            //Rota buscar todos os usuarios
            app.MapGet("/admin/obterTodosUsuarios", ([FromQuery] int pagina, IUsuarioServico usuarioServico) =>
            {
                List<Usuario> usuarios = usuarioServico.ObterTodosUsuarios(pagina);

                return Results.Ok(usuarios);

            }).WithTags("Administrador").RequireAuthorization();            
        }
    }
}