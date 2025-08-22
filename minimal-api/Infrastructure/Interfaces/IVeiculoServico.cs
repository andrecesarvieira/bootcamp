using MinimalApi.Entities;

namespace MinimalApi.Infrastructure.Interfaces
{
    public interface IVeiculoServico
    {
        //Metodo para buscar uma lista com todos os veículos
        List<Veiculo> ObterTodosVeiculos(int pagina = 1, string? nome = null, string? marca = null);

        //Método para buscar por veículo
        Veiculo? ObterVeiculoPorId(int id);

        //Método para incluir o veículo
        bool IncluirVeiculo(Veiculo veiculo);

        //Método para atualizar o veículo
        bool AtualizarVeiculo(Veiculo veiculo);

        //Método para excluir o veículo
        bool ExcluirVeiculo(int id);

    }
}