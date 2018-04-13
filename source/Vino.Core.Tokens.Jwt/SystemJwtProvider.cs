using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Vino.Core.Tokens.Jwt
{
    public class SystemJwtProvider : IJwtProvider
    {
        private JwtConfig _jwtConfig;

        public SystemJwtProvider(IOptions<JwtConfig> jwtConfig)
        {
            _jwtConfig = jwtConfig.Value;
            if (string.IsNullOrEmpty(_jwtConfig.Key))
            {
                throw new Exception("appsettings.json中jwt相关配置出错！");
            }
        }

        public string CreateToken(IEnumerable<Claim> claims)
        {
            return CreateToken(claims, DateTime.Now.AddMinutes(_jwtConfig.ExpiredMinutes));
        }

        public string CreateToken(IEnumerable<Claim> claims, DateTime expires)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Key));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var header = new JwtHeader(signingCredentials);

            var payload = new JwtPayload(_jwtConfig.Issuer, _jwtConfig.Audience, claims, DateTime.Now, expires);

            var secToken = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(secToken);
        }

        public ClaimsPrincipal ValidateToken(string validateJwtToken, TokenValidationParameters validationParameters)
        {
            var handler = new JwtSecurityTokenHandler();
            if (validationParameters.IssuerSigningKey == null)
            {
                validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Key));
            }

            return handler.ValidateToken(validateJwtToken, validationParameters, out SecurityToken validatedToken);
        }
    }
}
