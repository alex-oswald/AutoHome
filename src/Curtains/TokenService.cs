using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Curtains;

public interface ITokenService
{
    string BuildToken(string key, string issuer);
}

public class TokenService : ITokenService
{
    private readonly TimeSpan _expiryDuration = new(7, 0, 0);

    public string BuildToken(string key, string issuer)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        var tokenDescriptor = new JwtSecurityToken(issuer, issuer, null,
            expires: DateTime.Now.Add(_expiryDuration),
            signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
}
