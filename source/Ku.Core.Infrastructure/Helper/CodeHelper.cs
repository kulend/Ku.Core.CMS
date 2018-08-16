using System;
using System.Collections.Generic;
using System.Text;

namespace Ku.Core.Infrastructure.Helper
{
    public enum CodeLetterType
    {
        All,
        Number,
        Letter,

    }
    public static class CodeHelper
    {
        /// <summary>
        /// 该方法用于生成指定位数的随机数
        /// </summary>
        /// <param name="length">位数</param>
        /// <returns>返回一个随机数字符串</returns>
        public static string Create(int length, CodeLetterType type = CodeLetterType.All)
        {
            //可以显示的字符集合
            string vcahr = "";
            switch (type)
            {
                case CodeLetterType.All:
                    vcahr = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,l,m,n,p,q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,I,J,K,L,M,N,P,P,Q,R,S,T,U,V,W,X,Y,Z";
                    break;
                case CodeLetterType.Number:
                    vcahr = "0,1,2,3,4,5,6,7,8,9";
                    break;
                case CodeLetterType.Letter:
                    vcahr = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,p,q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,I,J,K,L,M,N,P,P,Q,R,S,T,U,V,W,X,Y,Z";
                    break;
                default:
                    break;
            }
            string[] VcArray = vcahr.Split(new Char[] { ',' });//拆分成数组
            string code = "";//产生的随机数
            Random rand = new Random(unchecked((int)DateTime.Now.Ticks));//初始化随机类
            for (int i = 1; i < length + 1; i++)
            {
                int t = rand.Next(VcArray.Length);//获取随机数
                code += VcArray[t];//随机数的位数加一
            }
            return code;
        }
    }
}
