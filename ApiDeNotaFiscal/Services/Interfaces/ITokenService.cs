using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ApiDeNotaFiscal.Services.Interfaces
{
    public interface ITokenService
    {
        //Gerar o token JWT
        JwtSecurityToken GenerateAcessToken(IEnumerable<Claim> claims, IConfiguration _config);
        string GenerateRefreshToken();

        //Extrair as claims, para gerar o novo token de acesso
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token, IConfiguration _config);
    }
}
