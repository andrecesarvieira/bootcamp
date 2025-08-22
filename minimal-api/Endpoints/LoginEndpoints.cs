using Microsoft.AspNetCore.Mvc;
using MinimalApi.Auth;
using MinimalApi.ViewModels;
using MinimalApi.Dtos;
using MinimalApi.Infrastructure.Interfaces;

namespace MinimalApi.Endpoints
{
	public static class LoginEndpoints
	{
		public static void MapLoginEndpoints(this IEndpointRouteBuilder app, byte[] jwtKey)
		{
			app.MapPost("/login", ([FromBody] LoginDto loginDTO, IUsuarioServico usuarioServico) =>
			{
				TokenJwt token = new();
				var usuarioLogin = usuarioServico.Login(loginDTO);

				if (usuarioLogin != null)
				{
					string tokenGerado = token.GerarTokenJwt(jwtKey, usuarioLogin);
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
			}).WithTags("Login").AllowAnonymous();
		}
	}
}
