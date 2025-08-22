using MinimalApi.Dominio.DTOs;
using MinimalApi.Dominio.ModelViews;

namespace MinimalApi.Dominio.Validacoes
{
    public class UsuarioValidacoes
    {
        public ErrosDeValidacao ValidaDTO(UsuarioDTO usuarioDTO)
        {
            ErrosDeValidacao validacao = new()
            {
                Mensagens = []
            };

            if (string.IsNullOrEmpty(usuarioDTO.Email))
            {
                validacao.Mensagens.Add("Email não pode ser vazio");
            }
            if (string.IsNullOrEmpty(usuarioDTO.Senha))
            {
                validacao.Mensagens.Add("Senha não pode ser vazia");
            }
            if (usuarioDTO.Perfil == 0)
            {
                validacao.Mensagens.Add("Perfil não pode ser zero");
            }
            return validacao;
        }         
    }
}