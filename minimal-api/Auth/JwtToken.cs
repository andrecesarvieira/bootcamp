using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MinimalApi.Entities;

namespace MinimalApi.Auth
{
    public class TokenJwt
    {
        public string GerarTokenJwt(byte[] key, Usuario usuario)
        {
            if (key == null || key.Length == 0) return string.Empty;

            List<Claim> claims =
            [
                new Claim("Email", usuario.Email),
                new Claim("Perfil", usuario.Perfil),
                new Claim(ClaimTypes.Role, usuario.Perfil)
            ];

            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(Encoding.UTF8.GetString(key)));
            SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new
            (
                claims: claims,
                signingCredentials: credentials,
                expires: DateTime.Now.AddDays(1)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}