using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MinimalApi.Dominio.Entidades;

namespace MinimalApi.Dominio.Token
{
    public class TokenJwt
    {
        public string GerarTokenJwt(byte[] key, Usuario usuario)
        {
            if (key == null || key.Length == 0) return string.Empty;

            List<Claim> claims = new()
            {
                new Claim("Email", usuario.Email),
                new Claim("Perfil", usuario.Perfil),
                new Claim("Expira", DateTime.Now.AddDays(1).ToString("yyyy-MM-ddTHH:mm:ss"))
            };

            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(Encoding.UTF8.GetString(key)));
            SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new
            (
                claims: claims,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}