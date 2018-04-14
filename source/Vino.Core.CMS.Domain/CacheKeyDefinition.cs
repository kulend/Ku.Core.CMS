using System;
using System.Collections.Generic;
using System.Text;

namespace Vino.Core.CMS.Domain
{
    public static class CacheKeyDefinition
    {
        public static readonly string VerifyCode = "verify.code:{0}";

        public static readonly string UserAuthCode = "vino.cache.user.authcode:{0}";
        public static readonly string UserAuthCodeEncrypt = "vino.cache.user.authcode.encrypt:{0}";

        public static readonly string EventSubscribers = "vino.cache.event.subscribers:{0}";

        public static readonly string ProductSkuTemp = "vino.cache.product.sku.temp:{0}";

        public static readonly string ApiMemberToken = "api.member.token:{0}.{1}";
        public static readonly string ApiExpiredToken = "api.expired.token:{0}";
    }
}
