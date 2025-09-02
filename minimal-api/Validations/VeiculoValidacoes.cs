using MinimalApi.Dtos;
using MinimalApi.ViewModels;

namespace MinimalApi.Validations
{
    public class VeiculoValidacoes
    {
        public ErrosDeValidacao ValidaDTO(VeiculoDto veiculoDTO)
        {
            ErrosDeValidacao validacao = new()
            {
                Mensagens = []
            };

            if (string.IsNullOrEmpty(veiculoDTO.Nome))
            {
                validacao.Mensagens.Add("Nome não pode ser vazio");
            }
            if (string.IsNullOrEmpty(veiculoDTO.Marca))
            {
                validacao.Mensagens.Add("Marca não pode ser vazia");
            }
            if (veiculoDTO.Ano == 0)
            {
                validacao.Mensagens.Add("Ano não pode ser zero");
            }
            return validacao;
        } 
    }
}