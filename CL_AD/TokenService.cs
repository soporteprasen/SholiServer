using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using CL_EN;

namespace CL_AD
{
    public class TokenService
    {
        private readonly string _secretKey;

        public TokenService(string secretKey)
        {
            _secretKey = secretKey;
        }

        public string GenerateToken(ENusuarios usuario, int minutes = 15)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, usuario.UsuarioNombre),
            new Claim("id_usuario", usuario.IdUsuario.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "tuapp",
                audience: "tuapp",
                claims: claims,
                expires: DateTime.UtcNow.AddYears(minutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
