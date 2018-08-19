using System;
using System.Collections.Generic;
using System.Text;

namespace Ku.Core.CMS.Domain
{
    public static class CacheKeyDefinition
    {
        public static readonly string VerifyCode = "verify.code:{0}";
        public static readonly string PageLock = "page.lock:{0}";

        public static readonly string UserAuthCode = "ku.cache.user.authcode:{0}";
        public static readonly string UserAuthCodeEncrypt = "ku.cache.user.authcode.encrypt:{0}";

        public static readonly string EventSubscribers = "ku.cache.event.subscribers:{0}";

        public static readonly string ProductSkuTemp = "ku.cache.product.sku.temp:{0}";

        #region WebApi

        public static readonly string ApiUserToken = "api.user.token:{0}.{1}";
        public static readonly string ApiExpiredToken = "api.expired.token:{0}";
        
        #endregion

        public static readonly string DataCenter_AppFeedback_Unsolved = "dc.app.feedback.unsolved:{0}";

        /// <summary>
        /// 短信验证码
        /// </summary>
        public const string SmsVerifyCode = "sms.verify.code:{0}:{1}";

        /// <summary>
        /// 短信验证码(注册)
        /// </summary>
        public const string SmsVerifyCode_Register = "sms.verify.code:register:{0}";

        /// <summary>
        /// 栏目数据缓存
        /// </summary>
        public const string ContentColumnList = "content.column.list";

        /// <summary>
        /// 配置缓存
        /// </summary>
        public const string Config = "config:{0}";
    }
}
