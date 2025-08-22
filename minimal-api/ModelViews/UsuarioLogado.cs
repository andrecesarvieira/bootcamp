namespace MinimalApi.Dominio.ModelViews
{
    public class UsuarioLogado
    {
        public string Email { get; set; } = string.Empty;
        public string Perfil { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}