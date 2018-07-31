namespace Ku.Core.Infrastructure.Exceptions
{
    public class KuException : System.Exception
    {
        public new string Message { set; get; }
        public int Code { set; get; }
        public new object Data { set; get; }

        public KuException()
            : base()
        {

        }

        public KuException(string message)
            : base(message)
        {
            this.Code = 99;
            this.Message = message;
        }

        public KuException(int code, string message)
            : base(message)
        {
            this.Code = code;
            this.Message = message;
        }

        public KuException(int code, string message, object data)
            : base(message)
        {
            this.Code = code;
            this.Message = message;
            this.Data = data;
        }
    }

    public class KuArgNullException : KuException
    {
        public KuArgNullException()
            : base(902, "参数出错")
        {

        }

        public KuArgNullException(string msg)
            : base(902, msg)
        {
        }

        public KuArgNullException(string msg, object data)
            : base(902, msg, data)
        {
        }
    }

    public class KuDataNotFoundException : KuException
    {
        public KuDataNotFoundException()
            : base(903, "无法取得相关数据！")
        {
        }

        public KuDataNotFoundException(string msg)
            : base(903, msg)
        {
        }
    }

    public class KuAccessDeniedException : KuException
    {
        public KuAccessDeniedException()
            : base(904, "无权操作！")
        {
        }
    }

    public class KuPageLockException : KuException
    {
        public KuPageLockException()
            : base(905, "页面已锁定！")
        {
        }
    }

    public class KuApiOverloadException : KuException
    {
        public KuApiOverloadException()
            : base(906, "访问过于频繁，请稍后重试！")
        {
        }
    }

    public class KuNeedVerifyCodeException : KuException
    {
        public KuNeedVerifyCodeException()
            : base(2001, "需要验证码")
        {
        }
    }

    public class KuVerifyCodeInvalidException : KuException
    {
        public KuVerifyCodeInvalidException()
            : base(2002, "验证码出错")
        {
        }

        public KuVerifyCodeInvalidException(string msg)
            : base(2002, msg)
        {
        }
    }
}
