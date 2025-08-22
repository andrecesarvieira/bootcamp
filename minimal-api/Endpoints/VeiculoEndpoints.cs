using Microsoft.AspNetCore.Mvc;
using MinimalApi.Dominio.Validacoes;
using MinimalApi.Dtos;
using MinimalApi.Entities;
using MinimalApi.Infrastructure.Interfaces;

namespace MinimalApi.Endpoints
{
    public static class VeiculoEndpoints
    {
        public static void MapVeiculoEndpoints(this IEndpointRouteBuilder app)
        {
            //Rota incluir veiculos
            app.MapPost("/veiculos/incluir", ([FromBody] VeiculoDto veiculoDTO, IVeiculoServico veiculoServico) =>
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
                
            }).WithTags("Veiculos").RequireAuthorization();

            //Rota atualizar veiculo
            app.MapPut("/veiculos/atualizar/{id}", ([FromRoute] int id, VeiculoDto veiculoDTO,IVeiculoServico veiculoServico) =>
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

            }).WithTags("Veiculos").RequireAuthorization();

            //Rota excluir veiculo
            app.MapDelete("/veiculos/excluir/{id}", ([FromRoute] int id, IVeiculoServico veiculoServico) =>
            {
                if (veiculoServico.ObterVeiculoPorId(id) == null)
                {
                    return Results.NotFound();
                }

                veiculoServico.ExcluirVeiculo(id);

                return Results.NoContent();

            }).WithTags("Veiculos").RequireAuthorization();

            //Rota buscar veiculos por Id
            app.MapGet("/veiculos/obterPorId/{id}", ([FromRoute] int id, IVeiculoServico veiculoServico) =>
            {
                Veiculo? veiculo = veiculoServico.ObterVeiculoPorId(id);

                return veiculo != null ? Results.Ok(veiculo) : Results.NotFound();
                
            }).WithTags("Veiculos").RequireAuthorization();

            //Rota buscar todos os veiculos
            app.MapGet("/veiculos/obterTodos", ([FromQuery] int pagina, IVeiculoServico veiculoServico) =>
            {
                List<Veiculo> veiculos = veiculoServico.ObterTodosVeiculos(pagina);

                return Results.Ok(veiculos);

            }).WithTags("Veiculos").RequireAuthorization();            
        }
    }
}