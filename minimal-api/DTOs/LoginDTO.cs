namespace MinimalApi.Dtos
{
    public class LoginDto(string username, string password)
    {
        public string Username { get; set; } = username;
        public string Password { get; set; } = password;
    }
}