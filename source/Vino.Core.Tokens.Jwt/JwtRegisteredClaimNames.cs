using System;
using System.Collections.Generic;
using System.Text;

namespace Vino.Core.Tokens.Jwt
{
    public struct JwtRegisteredClaimNames
    {
        /// <summary>
        /// http://tools.ietf.org/html/draft-ietf-oauth-json-web-token-20#section-4
        /// </summary>
        public const string Actort = "actort";

        /// <summary>
        /// http://openid.net/specs/openid-connect-core-1_0.html#IDToken
        /// </summary>
        public const string Acr = "acr";

        /// <summary>
        /// http://openid.net/specs/openid-connect-core-1_0.html#IDToken
        /// </summary>
        public const string Amr = "amr";

        /// <summary>
        /// http://tools.ietf.org/html/draft-ietf-oauth-json-web-token-20#section-4
        /// </summary>
        public const string Aud = "aud";

        /// <summary>
        /// http://openid.net/specs/openid-connect-core-1_0.html#IDToken
        /// </summary>
        public const string AuthTime = "auth_time";

        /// <summary>
        /// http://openid.net/specs/openid-connect-core-1_0.html#IDToken
        /// </summary>
        public const string Azp = "azp";

        /// <summary>
        /// http://tools.ietf.org/html/draft-ietf-oauth-json-web-token-20#section-4
        /// </summary>
        public const string Birthdate = "birthdate";

        /// <summary>
        /// http://tools.ietf.org/html/draft-ietf-oauth-json-web-token-20#section-4
        /// </summary>
        public const string CHash = "c_hash";

        /// <summary>
        /// http://tools.ietf.org/html/draft-ietf-oauth-json-web-token-20#section-4
        /// </summary>
        public const string Email = "email";

        /// <summary>
        /// http://tools.ietf.org/html/draft-ietf-oauth-json-web-token-20#section-4
        /// </summary>
        public const string Exp = "exp";

        /// <summary>
        /// http://tools.ietf.org/html/draft-ietf-oauth-json-web-token-20#section-4
        /// </summary>
        public const string Gender = "gender";

        /// <summary>
        /// http://tools.ietf.org/html/draft-ietf-oauth-json-web-token-20#section-4
        /// </summary>
        public const string FamilyName = "family_name";

        /// <summary>
        /// http://tools.ietf.org/html/draft-ietf-oauth-json-web-token-20#section-4
        /// </summary>
        public const string GivenName = "given_name";

        /// <summary>
        /// http://tools.ietf.org/html/draft-ietf-oauth-json-web-token-20#section-4
        /// </summary>
        public const string Iat = "iat";

        /// <summary>
        /// http://tools.ietf.org/html/draft-ietf-oauth-json-web-token-20#section-4
        /// </summary>
        public const string Iss = "iss";

        /// <summary>
        /// http://tools.ietf.org/html/draft-ietf-oauth-json-web-token-20#section-4
        /// </summary>
        public const string Jti = "jti";

        /// <summary>
        /// http://tools.ietf.org/html/draft-ietf-oauth-json-web-token-20#section-4
        /// </summary>
        public const string NameID = "nameid";

        /// <summary>
        /// http://tools.ietf.org/html/draft-ietf-oauth-json-web-token-20#section-4
        /// </summary>
        public const string Nonce = "nonce";

        /// <summary>
        /// http://tools.ietf.org/html/draft-ietf-oauth-json-web-token-20#section-4
        /// </summary>
        public const string Nbf = "nbf";

        /// <summary>
        /// http://tools.ietf.org/html/draft-ietf-oauth-json-web-token-20#section-4
        /// </summary>
        public const string Prn = "prn";

        /// <summary>
        /// http://tools.ietf.org/html/draft-ietf-oauth-json-web-token-20#section-4
        /// </summary>
        public const string Sub = "sub";

        /// <summary>
        /// http://tools.ietf.org/html/draft-ietf-oauth-json-web-token-20#section-4
        /// </summary>
        public const string Typ = "typ";

        /// <summary>
        /// http://tools.ietf.org/html/draft-ietf-oauth-json-web-token-20#section-4
        /// </summary>
        public const string UniqueName = "unique_name";

        /// <summary>
        /// http://tools.ietf.org/html/draft-ietf-oauth-json-web-token-20#section-4
        /// </summary>
        public const string Website = "website";
        /// <summary>
        /// 用户类型 0:前台 1:后台
        /// </summary>
        public const string CustomUserType = "user_type";
    }
}
