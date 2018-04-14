using System;
using System.Collections.Generic;
using System.Text;

namespace Vino.Core.Infrastructure.Exceptions
{
    public class VinoException : System.Exception
    {
        public new string Message { set; get; }
        public int Code { set; get; }
        public new object Data { set; get; }

        public VinoException()
            : base()
        {

        }

        public VinoException(string message)
            : base(message)
        {
            this.Code = 99;
            this.Message = message;
        }

        public VinoException(int code, string message)
            : base(message)
        {
            this.Code = code;
            this.Message = message;
        }

        public VinoException(int code, string message, object data)
            : base(message)
        {
            this.Code = code;
            this.Message = message;
            this.Data = data;
        }
    }

    public class VinoArgNullException : VinoException
    {
        public VinoArgNullException()
            : base(902, "参数出错")
        {

        }

        public VinoArgNullException(string msg)
            : base(902, msg)
        {
        }

        public VinoArgNullException(string msg, object data)
            : base(902, msg, data)
        {
        }
    }

    public class VinoDataNotFoundException : VinoException
    {
        public VinoDataNotFoundException()
            : base(903, "无法取得相关数据！")
        {
        }

        public VinoDataNotFoundException(string msg)
            : base(903, msg)
        {
        }
    }

    public class VinoAccessDeniedException : VinoException
    {
        public VinoAccessDeniedException()
            : base(904, "无权操作！")
        {
        }
    }

    public class VinoNeedVerifyCodeException : VinoException
    {
        public VinoNeedVerifyCodeException()
            : base(2001, "需要验证码")
        {
        }
    }

    public class VinoVerifyCodeInvalidException : VinoException
    {
        public VinoVerifyCodeInvalidException()
            : base(2002, "验证码出错")
        {
        }

        public VinoVerifyCodeInvalidException(string msg)
            : base(2002, msg)
        {
        }
    }
}
