using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Server.Models;

namespace Server.Controllers;

[ApiController]
[Route("token")]
public class TokenController : ControllerBase
{
    [HttpPost]
    public void Post([FromBody] string tokenOperacao)
    {
        Token token = Token.Preencher(tokenOperacao);

        Certificado certificado = new Certificado(token.Carro.Modelo);

        RSA chavePublica = certificado.ObterChavePublica();

        var validationParameters = new TokenValidationParameters
        {
            RequireExpirationTime = false,
            RequireSignedTokens = true,
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateLifetime = false,
            IssuerSigningKey = new RsaSecurityKey(chavePublica)
        };

        SecurityToken validacao;

        new JwtSecurityTokenHandler().ValidateToken(tokenOperacao, validationParameters, out validacao);

    }
}
