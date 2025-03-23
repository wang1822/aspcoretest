using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace AspCoreStudy.Services
{
    /// <summary>
    /// 提供生成和验证 JWT Token 的方法。
    /// </summary>
    public class TokenService(string secretKey)
    {
        private readonly string _secretKey = secretKey;

        /// <summary>
        /// 为指定用户生成包含给定权限的JWT令牌。
        /// </summary>
        /// <param name="username">生成令牌的用户名。</param>
        /// <param name="permissions">要包含在令牌中的权限列表。</param>
        /// <returns>作为字符串的JWT令牌。</returns>
        public string GenerateToken(string username, List<string> permissions)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username)
            };

            claims.AddRange(permissions.Select(permission => new Claim("permissions", permission)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = "your-issuer", // 签发者
                Audience = "your-audience", // 接收者
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}