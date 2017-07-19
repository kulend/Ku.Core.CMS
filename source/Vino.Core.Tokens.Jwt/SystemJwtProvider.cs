using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Vino.Core.Tokens.Jwt
{
    public class SystemJwtProvider : IJwtProvider
    {
        public string CreateToken(string key, string issuer, string audience, IEnumerable<Claim> claims, DateTime expires)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var header = new JwtHeader(signingCredentials);

            var payload = new JwtPayload(issuer, audience, claims, DateTime.Now, expires);

            var secToken = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(secToken);
        }

        public ClaimsPrincipal ValidateToken(string key, string validateJwtToken, TokenValidationParameters validationParameters)
        {
            var handler = new JwtSecurityTokenHandler();
            if (validationParameters.IssuerSigningKey == null)
            {
                validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            }

            return handler.ValidateToken(validateJwtToken, validationParameters, out SecurityToken validatedToken);
        }
    }
}
