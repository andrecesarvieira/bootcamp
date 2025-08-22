namespace MinimalApi.Dominio.DTOs
{
    public class UsuarioDTO
    {
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public PerfilEnum Perfil { get; set; }
    }
    public enum PerfilEnum
    {
        Admin,
        Editor,
        Usuario
    }
}