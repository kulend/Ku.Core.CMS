using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Vino.Core.Tokens.Jwt
{
    public interface IJwtProvider
    {
        string CreateToken(string key, string issuer, string audience, IEnumerable<Claim> claims, DateTime expires);

        ClaimsPrincipal ValidateToken(string key, string validateJwtToken, TokenValidationParameters validationParameters);
    }
}
