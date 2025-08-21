using MinimalApi.Dominio.DTOs;
using MinimalApi.Dominio.Entidades;

namespace MinimalApi.Infraestrutura.Interfaces
{
    public interface IUsuarioServico
    {
        //Método para efetuar o login
        Usuario? Login(LoginDTO loginDTO);

        //Método para incluir um novo usuário
        bool IncluirUsuario(Usuario usuario);

        //Método para atualizar usuario
        bool AtualizarUsuario(Usuario usuario);

        //Método para excluir usuario
        bool ExcluirUsuario(int id);

        //Metodo para buscar uma lista com todos os usuários
        List<Usuario> ObterTodosUsuarios(int pagina = 1, string? email = null, string? perfil = null);

        //Método para buscar usuário pelo email
        Usuario? ObterUsuarioPorEmail(string email);

        //Método para buscar usuário pelo id
        Usuario? ObterUsuarioPorId(int id);
    }
}