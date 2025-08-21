namespace MinimalApi.Dominio.DTOs
{
    public class LoginDTO(string username, string password)
    {
        public string Username { get; set; } = username;
        public string Password { get; set; } = password;
    }
}