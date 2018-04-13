using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Vino.Core.Tokens.Jwt
{
    public interface IJwtProvider
    {
        string CreateToken(IEnumerable<Claim> claims);

        string CreateToken(IEnumerable<Claim> claims, DateTime expires);

        ClaimsPrincipal ValidateToken(string validateJwtToken, TokenValidationParameters validationParameters);
    }
}
